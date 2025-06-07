using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EmailServiceIntermediate.Models.Serializables;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.Models.Serializables;
using EmailProvider.Enums;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

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
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"SMTP Server started on port {_port}.");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            var session = new SmtpSession(stream);
            await session.RunAsync();
        }
    }
}
