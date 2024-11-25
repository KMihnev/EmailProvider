using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using EmailProviderServer.TCP_Server.Dispatches;
using EmailProviderServer.DBContext;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProvider.Dispatches;

namespace EmailProviderServer.TCP_Server
{
    public class TcpServerService : BackgroundService
    {
        private readonly TcpListener _listener;
        private readonly ApplicationDbContext _context;

        public TcpServerService(TcpListener listener, ApplicationDbContext context)
        {
            _listener = listener;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            Console.WriteLine("TCP Server started.");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync(stoppingToken);
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
                var stream = client.GetStream();
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                try
                {
                    byte[] lengthPrefix = new byte[4];
                    await stream.ReadAsync(lengthPrefix, 0, lengthPrefix.Length, cancellationToken);
                    int messageLength = BitConverter.ToInt32(lengthPrefix, 0);

                    byte[] requestData = new byte[messageLength];
                    int totalBytesRead = 0;
                    while (totalBytesRead < messageLength)
                    {
                        int bytesRead = await stream.ReadAsync(requestData, totalBytesRead, messageLength - totalBytesRead, cancellationToken);
                        if (bytesRead == 0) throw new IOException("Client disconnected prematurely.");
                        totalBytesRead += bytesRead;
                    }

                    InPackage.ToArray(requestData);

                    InPackage.Deserialize(out int dispatchCode);

                    DispatchMapper dispatchMapper = new DispatchMapper(_context);
                    BaseDispatchHandler dispatchHandler = dispatchMapper.MapDispatch(dispatchCode);

                    if (dispatchHandler == null)
                    {
                        SetResponseFailed(OutPackage);
                    }
                    else
                    {
                        bool success = await dispatchHandler.Execute(InPackage, OutPackage);
                        if (!success)
                        {
                            SetResponseFailed(OutPackage);
                        }
                    }

                    byte[] responseData = OutPackage.ToByte();
                    byte[] responseLengthPrefix = BitConverter.GetBytes(responseData.Length);
                    await stream.WriteAsync(responseLengthPrefix, 0, responseLengthPrefix.Length, cancellationToken);
                    await stream.WriteAsync(responseData, 0, responseData.Length, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling client: {ex.Message}");
                    SetResponseFailed(OutPackage);
                }
            }
        }

        private void SetResponseFailed(SmartStreamArray ResponsePackage)
        {
            ResponsePackage.Serialize(false);
        }
    }
}
