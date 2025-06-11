using EmailServiceIntermediate.Settings;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EmailService.Parsers
{
    public static class DkimSigner
    {
        public static string SignEmail(string rawMessage)
        {
            string domain = SettingsProvider.GetEmailDomain();
            string selector = SettingsProvider.GetDKIMRecordSelector();
            string privateKeyPath = SettingsProvider.GetDKIMKeyPath();

            var keyText = File.ReadAllText(privateKeyPath);
            using var rsa = RSA.Create();
            rsa.ImportFromPem(keyText.ToCharArray());

            var headersToSign = new[] { "from", "to", "subject", "date" };

            var headerDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using var reader = new StringReader(rawMessage);
            string? line;
            while ((line = reader.ReadLine()) != null && line != "")
            {
                foreach (var h in headersToSign)
                {
                    if (line.StartsWith(h + ":", StringComparison.OrdinalIgnoreCase))
                    {
                        headerDict[h] = line.Trim();
                        break;
                    }
                }
            }

            string bodyHash = ComputeBodyHash(rawMessage);
            string dkimHeaderBare = $"v=1; a=rsa-sha256; c=simple/simple; d={domain}; s={selector}; " +
                                    $"h={string.Join(":", headersToSign)}; bh={bodyHash}; b=";

            string dkimHeaderForSigning = "DKIM-Signature: " + dkimHeaderBare;

            var signedHeadersBuilder = new StringBuilder();
            signedHeadersBuilder.AppendLine(dkimHeaderForSigning);
            foreach (var h in headersToSign)
            {
                if (headerDict.TryGetValue(h, out var headerLine))
                {
                    signedHeadersBuilder.AppendLine(headerLine);
                }
            }

            var signingBytes = Encoding.ASCII.GetBytes(signedHeadersBuilder.ToString());
            var signatureBytes = rsa.SignData(signingBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var signatureBase64 = Convert.ToBase64String(signatureBytes);

            return WrapDkimHeader(dkimHeaderBare + signatureBase64);
        }



        private static string ComputeBodyHash(string fullMessage)
        {
            var parts = fullMessage.Split(new[] { "\r\n\r\n" }, 2, StringSplitOptions.None);
            var body = parts.Length > 1 ? parts[1] : "";
            var bytes = Encoding.UTF8.GetBytes(body.TrimEnd() + "\r\n");
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static string WrapDkimHeader(string header)
        {
            const int maxLength = 75;
            var sb = new StringBuilder("DKIM-Signature: ");
            int index = 0;

            while (index < header.Length)
            {
                int take = Math.Min(maxLength, header.Length - index);
                sb.Append(header.Substring(index, take));
                index += take;

                if (index < header.Length)
                {
                    sb.Append("\r\n\t");
                }
            }

            return sb.ToString();
        }
    }
}
