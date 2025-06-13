//Includes
using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using EmailProviderServer.TCP_Server.Dispatches;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace EmailProviderServer.TCP_Server
{
    //------------------------------------------------------
    //	TcpServerService
    //------------------------------------------------------
    public class TcpServerService : BackgroundService
    {
        private readonly X509Certificate2 _serverCertificate;

        private readonly TcpListener _listener;
        private readonly DispatchMapper _dispatchMapper;

        //Constructor
        public TcpServerService(TcpListener listener, DispatchMapper dispatchMapper, X509Certificate2 serverCertificate)
        {
            _listener = listener;
            _dispatchMapper = dispatchMapper;
            _serverCertificate = serverCertificate;
        }

        //Methods
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            _listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            Console.WriteLine("TCP Server started.");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync(stoppingToken);
                    client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    Console.WriteLine("Client connected.");
                    _ = HandleClientAsync(client, stoppingToken);
                }
            }
            finally
            {
                _listener.Stop();
            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
        {
            using (client)
            {
                var sslStream = new SslStream(client.GetStream(), false);
                try
                {
                    await sslStream.AuthenticateAsServerAsync(_serverCertificate, clientCertificateRequired: false,enabledSslProtocols: System.Security.Authentication.SslProtocols.Tls12,checkCertificateRevocation: false);
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
                    // Зареждаме потока в "умниия масив" ;)
                    InPackage.LoadFromStream(sslStream);

                    await BaseDispatchHandler.HandleRequestAsync(InPackage, OutPackage, _dispatchMapper);

                    // изпращаме отговор
                    byte[] responseData = OutPackage.ToByte();
                    await sslStream.WriteAsync(responseData, 0, responseData.Length, cancellationToken);

                    await sslStream.FlushAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogError(LogMessages.InteralError);
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _listener.Stop();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _listener?.Server?.Dispose();
            base.Dispose();
        }
    }
}
