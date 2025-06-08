using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using EmailService.Parsers;

namespace EmailService
{
    public class SmtpSession
    {
        private StreamReader reader;
        private StreamWriter writer;
        private Stream baseStream;
        private bool tlsRequested = false;

        private string sender;
        private List<string> recipients = new();
        private StringBuilder dataBuffer = new();
        private bool readingData = false;

        public SmtpSession(Stream stream)
        {
            baseStream = stream;
            reader = new StreamReader(stream, Encoding.ASCII);
            writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
        }

        public async Task RunAsync()
        { 
            await writer.WriteLineAsync("220 tyron.mail SMTP Service Ready");

            while (true)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) break;

                if (readingData) 
                {
                    if (line == ".")
                    {
                        readingData = false;
                        await writer.WriteLineAsync("250 OK: Message received");
                        var handler = new EmailMessageHandler();
                        await handler.HandleAsync(sender, recipients, dataBuffer.ToString());
                        recipients.Clear();
                        dataBuffer.Clear();
                    }
                    else
                    {
                        dataBuffer.Append(line).Append("\r\n");
                    }
                    continue;
                }

                var processor = new SmtpCommandProcessor(this);
                var response = await processor.ProcessAsync(line);
                await writer.WriteLineAsync(response);

                if (tlsRequested)
                {
                    await UpgradeToTlsAsync();
                    tlsRequested = false;
                    continue;
                }

                if (response.StartsWith("221")) break;
            }
        }

        public void BeginData() => readingData = true;
        public void SetSender(string value) => sender = value;
        public void AddRecipient(string value) => recipients.Add(value);
        public List<string> GetRecipients() => recipients;

        public void MarkStartTlsRequested() => tlsRequested = true;

        private async Task UpgradeToTlsAsync()
        {
            using var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindBySubjectName, "localhost", validOnly: false);

            if (certs.Count == 0)
            {
                await writer.WriteLineAsync("454 TLS not available: certificate not found");
                throw new Exception("No TLS certificate found.");
            }

            var cert = certs[0];
            var sslStream = new SslStream(baseStream, false);
            await sslStream.AuthenticateAsServerAsync(cert, false, false);

            reader = new StreamReader(sslStream, Encoding.ASCII);
            writer = new StreamWriter(sslStream, Encoding.ASCII) { AutoFlush = true };
        }
    }
}
