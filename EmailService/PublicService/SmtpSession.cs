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
        private static readonly X509Certificate2 CachedCert = LoadCertificate();

        private StreamReader reader;
        private StreamWriter writer;
        private Stream baseStream;

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

                if (!string.IsNullOrEmpty(response))
                    await writer.WriteLineAsync(response);

                if (response.StartsWith("221")) break;
            }
        }

        public void BeginData() => readingData = true;
        public void SetSender(string value) => sender = value;
        public void AddRecipient(string value) => recipients.Add(value);
        public List<string> GetRecipients() => recipients;

        public async Task WriteLineAsync(string line)
        {
            await writer.WriteLineAsync(line);
        }

        public async Task UpgradeToTlsAsync()
        {
            var sslStream = new SslStream(baseStream, false);
            try
            {
                var cert = CachedCert;
                if (cert == null)
                    throw new InvalidOperationException("No valid certificate found for STARTTLS");

                await sslStream.AuthenticateAsServerAsync(cert, clientCertificateRequired: false, enabledSslProtocols: System.Security.Authentication.SslProtocols.Tls12, checkCertificateRevocation: false);
                Console.WriteLine("TLS handshake completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("TLS handshake failed: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                throw;
            }
            reader = new StreamReader(sslStream, Encoding.ASCII);
            writer = new StreamWriter(sslStream, Encoding.ASCII) { AutoFlush = true };

            Console.WriteLine("TLS handshake successful. Session stream upgraded.");
        }

        private static X509Certificate2 LoadCertificate()
        {
            var cert = new X509Certificate2(
                SettingsProvider.GetSMTPServicePublicCertPFXPath(),
                SettingsProvider.GetSMTPServicePublicCertPassword(),
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.PersistKeySet |
                X509KeyStorageFlags.Exportable
            );

            if (!cert.HasPrivateKey)
                throw new InvalidOperationException("Cached cert does not have a private key.");

            return cert;
        }
    }
}
