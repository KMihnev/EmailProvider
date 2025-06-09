using EmailService.Parsers;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Models.Serializables;
using System.Net.Sockets;

namespace EmailService.PublicService
{
    public class SmtpMailSender
    {
        private string SmtpHost = AddressHelper.GetSMTPIpAddress().ToString();
        private int SmtpPort = AddressHelper.GetSMTPPublicPort();

        public async Task<string> SendAsync(EmailViewModel email)
        {
            var mimeMessage = EmailComposer.Compose(email);

            using var client = new TcpClient();
            await client.ConnectAsync(SmtpHost, SmtpPort);

            using var stream = client.GetStream();
            using var reader = new StreamReader(stream);
            using var writer = new StreamWriter(stream) { AutoFlush = true };

            await ReadResponseAsync(reader);
            await writer.WriteLineAsync("HELO email-service.local");
            await ReadResponseAsync(reader);

            await writer.WriteLineAsync($"MAIL FROM:<{email.FromEmail}>");
            await ReadResponseAsync(reader);

            foreach (var recipient in email.Recipients)
            {
                await writer.WriteLineAsync($"RCPT TO:<{recipient.Email}>");
                await ReadResponseAsync(reader);
            }

            await writer.WriteLineAsync("DATA");
            await ReadResponseAsync(reader);

            await writer.WriteLineAsync(mimeMessage + "\r\n.");
            await ReadResponseAsync(reader);

            await writer.WriteLineAsync("QUIT");
            await ReadResponseAsync(reader);

            return mimeMessage;
        }

        private async Task ReadResponseAsync(StreamReader reader)
        {
            var response = await reader.ReadLineAsync();
            if (response == null || !response.StartsWith("2") && !response.StartsWith("3"))
                throw new InvalidOperationException($"SMTP error: {response}");
        }
    }
}
