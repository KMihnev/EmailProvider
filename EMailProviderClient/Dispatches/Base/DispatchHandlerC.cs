using EMailProviderClient.Controllers.UserControl;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using System.Net.Sockets;

namespace EMailProviderClient.Dispatches.Base
{
    public class DispatchHandlerC
    {
        public async Task<bool> Execute(SmartStreamArray inPackage, SmartStreamArray outPackage)
        {
            try
            {
                using TcpClient client = new TcpClient();
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

                using var cts = new CancellationTokenSource(5000);
                await client.ConnectAsync(AddressHelper.GetIpAddress(), AddressHelper.GetPort(), cts.Token);
                using var stream = client.GetStream();
                stream.Flush();

                SmartStreamArray finalPackage = new();
                finalPackage.Serialize(SessionControllerC.Token ?? "");

                finalPackage.Append(inPackage);

                byte[] requestData = finalPackage.ToByte();
                await stream.WriteAsync(requestData, 0, requestData.Length);

                outPackage.LoadFromStream(stream);

                bool result = false;
                outPackage.Deserialize(out result);

                if (!result)
                {
                    string error = "";
                    outPackage.Deserialize(out error);
                    Logger.LogError(!string.IsNullOrWhiteSpace(error) ? error : LogMessages.InteralError);
                    return false;
                }

                stream.Flush();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }
        }
    }
}
