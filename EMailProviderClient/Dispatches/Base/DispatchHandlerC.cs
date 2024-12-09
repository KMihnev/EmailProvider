//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using System.Net.Sockets;

namespace EMailProviderClient.Dispatches.Base
{
    //------------------------------------------------------
    //	DispatchHandlerC
    //------------------------------------------------------
    public class DispatchHandlerC
    {
        public async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                using TcpClient client = new TcpClient();
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

                await client.ConnectAsync(AddressHelper.GetIpAddress(), AddressHelper.GetPort());
                using var stream = client.GetStream();
                stream.Flush();

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
