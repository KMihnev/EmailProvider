using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using EmailService.Parsers;
using EmailServiceIntermediate.Settings;

namespace EmailService.PublicService
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
                Task<string> readTask = reader.ReadLineAsync();
                if (await Task.WhenAny(readTask, Task.Delay(15000)) != readTask)
                {
                    Console.WriteLine("Timed out");
                    break;
                }

                var line = readTask.Result;
                if (line == null) break;

                if (readingData)
                {
                    if (line == ".")
                    {
                        readingData = false;
                        await writer.WriteLineAsync("250 OK: Message received");
                        var handler = new EmailMessageHandler();
                        await handler.HandleAsync(sender, recipients, dataBuffer.ToString());

                        // ✅ Reset state after send
                        sender = null;
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
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var certs = store.Certificates.Find(
                X509FindType.FindBySubjectName,
                SettingsProvider.GetSMTPServicePublicCertSubject(),
                validOnly: false
            );

            var cert = certs.Count > 0 ? certs[0] : null;
            if (cert == null)
                throw new InvalidOperationException("No valid certificate found for STARTTLS");

            var sslStream = new SslStream(baseStream, false);
            await sslStream.AuthenticateAsServerAsync(cert, false, false);
            reader = new StreamReader(sslStream, Encoding.ASCII);
            writer = new StreamWriter(sslStream, Encoding.ASCII) { AutoFlush = true };
        }
    }
}
