using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class DispatchHandlerC
    {
        private static TcpClient _client;
        private static NetworkStream _stream;

        public static async Task InitClient()
        {
            if (_client == null || !_client.Connected)
            {
                _client = new TcpClient();
                await _client.ConnectAsync("192.168.1.27", 8009);
                _stream = _client.GetStream();
            }
        }

        public static TcpClient Client { get { return _client; } }

        public static NetworkStream Stream { get { return _stream;} }
    }
}
