using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;

namespace EmailService.PrivateService
{
    public class SmtpPrivateServer
    {
        private readonly X509Certificate2 _serverCertificate;

        public SmtpPrivateServer(X509Certificate2 serverCertificate)
        {
            _serverCertificate = serverCertificate;
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
            using (var sslStream = new SslStream(client.GetStream(), false))
            {
                try
                {
                    await sslStream.AuthenticateAsServerAsync(_serverCertificate, clientCertificateRequired: false, enabledSslProtocols: System.Security.Authentication.SslProtocols.Tls12, checkCertificateRevocation: false);
                    Console.WriteLine("TLS handshake successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("TLS handshake failed: " + ex.Message);
                    return;
                }

                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                try
                {
                    Console.WriteLine("Loading stream...");
                    InPackage.LoadFromStream(sslStream);
                    Console.WriteLine("Loaded stream");


                    Console.WriteLine("Handling dispatch...");
                    await BaseDispatchHandler.HandleRequestAsync(InPackage, OutPackage, new DispatchMapper());
                    Console.WriteLine("Handled dispatch");

                    Console.WriteLine("Writing response...");
                    byte[] responseData = OutPackage.ToByte();

                    var writeTask = sslStream.WriteAsync(responseData, 0, responseData.Length);
                    if (await Task.WhenAny(writeTask, Task.Delay(5000)) != writeTask)
                    {
                        Logger.LogError("Write timed out.");
                        return;
                    }

                    await sslStream.FlushAsync();
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
