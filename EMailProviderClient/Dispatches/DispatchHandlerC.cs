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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class DispatchHandlerC
    {
        private static TcpClient _client;
        private static NetworkStream _stream;
        public Response Response { get; private set; }

        public static async Task InitClient()
        {
            _client = new TcpClient();
            await _client.ConnectAsync("192.168.1.27", 8009);
            _stream = _client.GetStream();
        }

        public DispatchHandlerC()
        {

        }

        public async Task<bool> HandleResponse()
        {
            try
            {
                var buffer = new byte[1024];
                var completeMessage = new StringBuilder();
                int bytesRead;

                do
                {
                    bytesRead = await Stream.ReadAsync(buffer, 0, buffer.Length);
                    string part = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    completeMessage.Append(part);

                } while (!completeMessage.ToString().EndsWith("\n"));

                string jsonResponse = completeMessage.ToString().TrimEnd('\n');

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };

                Response = JsonSerializer.Deserialize<Response>(jsonResponse, options);

                _client.Close();
                _stream.Flush();

                if(Response?.bSuccess != null && !Response.bSuccess)
                {
                    Logger.LogError(Response.msgError);
                    return false;
                }
            }
            catch (Exception)
            {
                _client.Close();
                _stream.Flush();
                throw;
            }

            return true;
        }

        public async Task SendRequest(MethodRequest request)
        {
            await InitClient();
            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson + "\n");
            await Stream.WriteAsync(requestBytes, 0, requestBytes.Length);
        }

        public static TcpClient Client { get { return _client; } }

        private NetworkStream Stream { get { return _stream;} }
    }
}
