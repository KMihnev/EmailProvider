using System.Net.Sockets;
using EmailServiceIntermediate.Dispatches;

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
