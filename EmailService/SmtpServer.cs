using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EmailServiceIntermediate.Models.Serializables;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.Models.Serializables;
using EmailProvider.Enums;

namespace EmailService
{
    public class SmtpServer
    {
        private readonly int _port;

        public SmtpServer(int port = 25)
        {
            _port = port;
        }

        public async Task StartAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"SMTP Server started on port {_port}.");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            using var reader = new StreamReader(stream, Encoding.ASCII);
            using var writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };

            await writer.WriteLineAsync("220 tyron.mail SMTP Service Ready");

            string sender = null;
            string recipient = null;
            List<string> data = new();
            string line;
            bool isDataMode = false;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (isDataMode)
                {
                    if (line == ".")
                    {
                        isDataMode = false;
                        await writer.WriteLineAsync("250 OK: Message accepted");

                        await ForwardToMainServerAsync(sender, recipient, string.Join("\n", data));
                        data.Clear();
                    }
                    else
                    {
                        data.Add(line);
                    }
                    continue;
                }

                if (line.StartsWith("HELO") || line.StartsWith("EHLO"))
                    await writer.WriteLineAsync("250 Hello");
                else if (line.StartsWith("MAIL FROM:"))
                    sender = line[10..].Trim('<', '>');
                else if (line.StartsWith("RCPT TO:"))
                    recipient = line[8..].Trim('<', '>');
                else if (line == "DATA")
                {
                    await writer.WriteLineAsync("354 End data with <CR><LF>.<CR><LF>");
                    isDataMode = true;
                }
                else if (line == "QUIT")
                {
                    await writer.WriteLineAsync("221 Bye");
                    break;
                }
                else
                    await writer.WriteLineAsync("500 Unknown command");
            }

            client.Close();
        }

        private async Task ForwardToMainServerAsync(string from, string to, string body)
        {
            var dispatchHandler = new DispatchHandlerService();
            var inPackage = new SmartStreamArray();
            var outPackage = new SmartStreamArray();

            var message = new EmailViewModel
            {
                FromEmail = from,
                Subject = "SMTP Message",
                Body = body,
                Status = EmailStatuses.EmailStatusNew,
                Recipients = new List<MessageRecipientSerializable>
                {
                    new MessageRecipientSerializable { Email = to}
                }
            };

            inPackage.Serialize(DispatchEnums.ReceiveEmails);
            inPackage.Serialize(message);

            bool result = await dispatchHandler.Execute(inPackage, outPackage);

            Console.WriteLine(result ? "Email forwarded successfully." : "Failed to forward email.");
        }
    }
}
