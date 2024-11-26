using EmailProvider.Dispatches;
using EmailProvider.Logging;
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
                using var stream = client.GetStream();

                byte[] requestData = InPackage.ToByte();
                await stream.WriteAsync(requestData, 0, requestData.Length);

                OutPackage.LoadFromStream(stream);

                bool bResult = false;
                OutPackage.Deserialize(out bResult);

                if (!bResult)
                {
                    string Error = "";
                    OutPackage.Deserialize(out Error);
                        if (Error?.Length > 0)
                        Logger.LogError(Error);
                    else
                        Logger.LogError(LogMessages.InteralError);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            return true;
        }
    }
}
