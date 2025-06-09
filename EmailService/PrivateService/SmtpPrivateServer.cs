using System.Net.Sockets;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;

namespace EmailService.PrivateService
{
    public class SmtpPrivateServer
    {
        public SmtpPrivateServer()
        {
        }

        public async Task StartAsync()
        {
            int Port = AddressHelper.GetSMTPPrivatePort();
            var listener = new TcpListener(AddressHelper.GetSMTPIpAddress(), Port);
            listener.Start();
            Console.WriteLine($"SMTP Private Server started on port {Port}.");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                var stream = client.GetStream();

                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                try
                {
                    // Зареждаме потока в "умниия масив" ;)
                    InPackage.LoadFromStream(stream);

                    await BaseDispatchHandler.HandleRequestAsync(InPackage, OutPackage, new DispatchMapper());

                    // изпращаме отговор
                    byte[] responseData = OutPackage.ToByte();
                    await stream.WriteAsync(responseData, 0, responseData.Length);

                    await stream.FlushAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogError(LogMessages.InteralError);
                }
            }
        }
    }
}
