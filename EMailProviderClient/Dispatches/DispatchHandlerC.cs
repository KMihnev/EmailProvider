using EmailProvider.Dispatches;
using EmailProvider.Logging;
using EmailProvider.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class DispatchHandlerC
    {
        private static TcpClient _client;
        private NetworkStream _stream;
        public Response Response { get; private set; }

        public static async Task InitClient()
        {
            if (_client == null || !_client.Connected)
            {
                _client = new TcpClient();
                await _client.ConnectAsync("192.168.1.27", 8009);
            }
        }

        public DispatchHandlerC()
        {
            _stream = _client.GetStream();
        }

        public async Task<bool> HandleResponse()
        {
            byte[] responseBytes = new byte[1024];
            int bytesRead = await Stream.ReadAsync(responseBytes, 0, responseBytes.Length);
            string jsonResponse = Encoding.UTF8.GetString(responseBytes, 0, bytesRead);

            Response = JsonSerializer.Deserialize<Response>(jsonResponse);

            if(Response?.bSuccess != null && !Response.bSuccess)
            {
                Logger.LogError(Response.msgError);
                return false;
            }

            return true;
        }

        public async Task SendRequest(MethodRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson + "\n");
            await Stream.WriteAsync(requestBytes, 0, requestBytes.Length);
        }

        public static TcpClient Client { get { return _client; } }

        private NetworkStream Stream { get { return _stream;} }
    }
}
