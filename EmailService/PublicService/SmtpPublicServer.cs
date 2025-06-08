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

namespace EmailService.PublicService
{
    public class SmtpPublicServer
    {

        public SmtpPublicServer()
        {
        }

        public async Task StartAsync()
        {
            int Port = AddressHelper.GetSMTPPublicPort();
            var listener = new TcpListener(AddressHelper.GetSMTPIpAddress(), Port);
            listener.Start();
            Console.WriteLine($"SMTP Public Server started on port {Port}.");

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
