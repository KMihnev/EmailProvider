using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class SmtpSession
    {
        private StreamReader reader;
        private StreamWriter writer;
        private Stream baseStream;

        private string sender;
        private string recipient;
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

                line = line.Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (readingData)
                {
                    if (line == ".")
                    {
                        readingData = false;
                        await writer.WriteLineAsync("250 OK: Message received");
                        var handler = new EmailMessageHandler();
                        await handler.HandleAsync(sender, recipient, dataBuffer.ToString());
                    }
                    else
                    {
                        dataBuffer.AppendLine(line);
                    }
                    continue;
                }

                var processor = new SmtpCommandProcessor(this);
                var response = await processor.ProcessAsync(line);
                await writer.WriteLineAsync(response);
            }
        }

        public void BeginData() => readingData = true;
        public void SetSender(string value) => sender = value;
        public void SetRecipient(string value) => recipient = value;
    }
}
