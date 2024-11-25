using EmailProvider.Dispatches;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class DispatchHandlerC
    {
        public async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync(AddressHelper.GetIpAddress(), AddressHelper.GetPort());
                var stream = client.GetStream();

                byte[] requestData = InPackage.ToByte();

                byte[] lengthPrefix = BitConverter.GetBytes(requestData.Length);
                await stream.WriteAsync(lengthPrefix, 0, lengthPrefix.Length);
                await stream.WriteAsync(requestData, 0, requestData.Length);

                byte[] responseLengthPrefix = new byte[4];
                await stream.ReadAsync(responseLengthPrefix, 0, responseLengthPrefix.Length);
                int responseLength = BitConverter.ToInt32(responseLengthPrefix, 0);

                byte[] responseData = new byte[responseLength];
                int totalBytesRead = 0;
                while (totalBytesRead < responseLength)
                {
                    int bytesRead = await stream.ReadAsync(responseData, totalBytesRead, responseLength - totalBytesRead);
                    if (bytesRead == 0) throw new IOException("Server disconnected prematurely.");
                    totalBytesRead += bytesRead;
                }

                OutPackage.ToArray(responseData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in client execution: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
