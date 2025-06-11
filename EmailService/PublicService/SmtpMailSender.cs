using DnsClient;
using EmailService.Parsers;
using EmailServiceIntermediate.Models.Serializables;
using EmailServiceIntermediate.Settings;
using System.Net.Security;
using System.Net.Sockets;
using System.Net;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Text;

public class SmtpMailSender
{
    public async Task<string> SendAsync(EmailViewModel email)
    {
        var mimeMessage = EmailComposer.Compose(email);

        string dkimHeader = DkimSigner.SignEmail(mimeMessage);

        string signedMimeMessage = dkimHeader + "\r\n" + mimeMessage;

        var normalizedMessage = signedMimeMessage
            .Replace("\r\n", "\n")
            .Replace("\r", "")
            .Replace("\n", "\r\n");

        var dotStuffedMessage = string.Join("\r\n", normalizedMessage
            .Split(new[] { "\r\n" }, StringSplitOptions.None)
            .Select(line => line.StartsWith(".") ? "." + line : line));

        foreach (var recipient in email.Recipients)
        {
            var domain = recipient.Email.Split('@')[1];
            var (ip, mxHost) = await ResolveMxAsync(domain);

            using var client = new TcpClient();
            await client.ConnectAsync(ip.ToString(), 25);

            using var stream = client.GetStream();
            var reader = new StreamReader(stream, Encoding.ASCII);
            var writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };

            await ReadResponseAsync(reader);

            await writer.WriteLineAsync($"EHLO {SettingsProvider.GetEmailDomain()}");
            var supportsStartTls = await ReadEhloAndDetectStartTlsAsync(reader);

            if (supportsStartTls)
            {
                await writer.WriteLineAsync("STARTTLS");
                await ReadResponseAsync(reader);

                var sslStream = new SslStream(stream, false);
                await sslStream.AuthenticateAsClientAsync(mxHost, null, SslProtocols.Tls12 | SslProtocols.Tls13, false);

                reader = new StreamReader(sslStream, Encoding.ASCII);
                writer = new StreamWriter(sslStream, Encoding.ASCII) { AutoFlush = true };

                await writer.WriteLineAsync($"EHLO {SettingsProvider.GetEmailDomain()}");
                await ReadEhloAndDetectStartTlsAsync(reader);
            }

            await writer.WriteLineAsync($"MAIL FROM:<{email.FromEmail}>");
            await ReadResponseAsync(reader);

            await writer.WriteLineAsync($"RCPT TO:<{recipient.Email}>");
            await ReadResponseAsync(reader);

            await writer.WriteLineAsync("DATA");
            await ReadResponseAsync(reader);

            await writer.WriteAsync(dotStuffedMessage);
            await writer.WriteAsync("\r\n.\r\n");
            await ReadResponseAsync(reader);

            await writer.WriteLineAsync("QUIT");
            await ReadResponseAsync(reader);
        }

        return signedMimeMessage;
    }

    private async Task<(IPAddress ip, string hostname)> ResolveMxAsync(string domain)
    {
        var lookup = new LookupClient();
        var result = await lookup.QueryAsync(domain, QueryType.MX);
        var mx = result.Answers.MxRecords().OrderBy(x => x.Preference).FirstOrDefault()
            ?? throw new Exception("No MX record found.");

        var mxHost = mx.Exchange.Value.TrimEnd('.');
        var ipResult = await lookup.QueryAsync(mxHost, QueryType.A);
        var ip = ipResult.Answers.ARecords().FirstOrDefault()?.Address
            ?? throw new Exception("No A record found for MX host.");

        return (ip, mxHost);
    }

    private async Task<bool> ReadEhloAndDetectStartTlsAsync(StreamReader reader)
    {
        bool supportsStartTls = false;
        string? line;
        do
        {
            line = await reader.ReadLineAsync();
            if (line == null)
                throw new InvalidOperationException("SMTP connection closed unexpectedly.");

            Console.WriteLine("EHLO: " + line);
            if (line.Contains("STARTTLS", StringComparison.OrdinalIgnoreCase))
                supportsStartTls = true;

        } while (Regex.IsMatch(line, @"^\d{3}-"));

        return supportsStartTls;
    }

    private async Task ReadResponseAsync(StreamReader reader)
    {
        string? line;
        do
        {
            line = await reader.ReadLineAsync();
            if (line == null)
                throw new InvalidOperationException("SMTP connection closed unexpectedly.");

            Console.WriteLine("SMTP: " + line);
        } while (Regex.IsMatch(line, @"^\d{3}-"));

        if (!line.StartsWith("2") && !line.StartsWith("3"))
            throw new InvalidOperationException($"SMTP error: {line}");
    }
}
