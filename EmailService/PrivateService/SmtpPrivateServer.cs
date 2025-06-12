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
            using (var stream = client.GetStream())
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                try
                {
                    Console.WriteLine("Loading stream...");
                    InPackage.LoadFromStream(stream);
                    Console.WriteLine("Loaded stream");


                    Console.WriteLine("Handling dispatch...");
                    await BaseDispatchHandler.HandleRequestAsync(InPackage, OutPackage, new DispatchMapper());
                    Console.WriteLine("Handled dispatch");

                    Console.WriteLine("Writing response...");
                    byte[] responseData = OutPackage.ToByte();

                    var writeTask = stream.WriteAsync(responseData, 0, responseData.Length);
                    if (await Task.WhenAny(writeTask, Task.Delay(5000)) != writeTask)
                    {
                        Logger.LogError("Write timed out.");
                        return;
                    }

                    await stream.FlushAsync();
                    Console.WriteLine("Flushed response");
                }
                catch (Exception ex)
                {
                    Logger.LogError("Exception in HandleClientAsync: " + ex.Message);
                }
            }
        }
    }
}
