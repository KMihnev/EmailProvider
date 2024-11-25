using EmailProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Dispatches
{
    public class DispatchImplementation
    {
        public static async Task ExecuteRequestAsync(DispatchEnums dispatchEnum , SmartStreamArray array, string serverIp, int serverPort)
        {
            try
            {
                using TcpClient client = new TcpClient(serverIp, serverPort);
                using NetworkStream stream = client.GetStream();

                //Първо записваме кода на диспача
                byte[] codeBytes = BitConverter.GetBytes(((short)dispatchEnum));
                await stream.WriteAsync(codeBytes, 0, codeBytes.Length);

                byte[] serializedData = array.ToByte();
                await stream.WriteAsync(serializedData, 0, serializedData.Length);

                Console.WriteLine("Request sent to server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
