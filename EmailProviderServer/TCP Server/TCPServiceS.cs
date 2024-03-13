//Includes

using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using System.Text.Json;
using EmailProviderServer.TCP_Server.Dispatches;
using EmailProviderServer.DBContext;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProvider.Dispatches;
using System.Text.Json.Serialization;

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
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                try
                {
                    var requestJson = await reader.ReadLineAsync();
                    var request = JsonSerializer.Deserialize<MethodRequest>(requestJson);

                    DispatchMapper dispatchMapper = new DispatchMapper(_context);
                    IDispatchHandler dispatchHandler = dispatchMapper.MapDispatch(request);

                    bool result = await dispatchHandler.Execute(request.Parameters);

                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                    };

                    var response = JsonSerializer.Serialize(dispatchHandler.response, options ) + "\n";
                    await writer.WriteLineAsync(response);
                }
                catch (Exception ex)
                {
                    client.Close();
                    client.GetStream().Close();
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    var errorResponse = JsonSerializer.Serialize(new { Success = false, Error = ex.Message });
                    await writer.WriteLineAsync(errorResponse);
                }
            }
        }
    }
}
