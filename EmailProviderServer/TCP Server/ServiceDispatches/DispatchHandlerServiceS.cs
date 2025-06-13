using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Settings;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;

namespace EmailProviderServer.ServiceDispatches
{
    public class DispatchHandlerServiceS
    {
        public async Task<bool> Execute(SmartStreamArray inPackage, SmartStreamArray outPackage)
        {
            try
            {
                using TcpClient client = new TcpClient();
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

                using var cts = new CancellationTokenSource(5000);
                await client.ConnectAsync(AddressHelper.GetSMTPIpAddress(), AddressHelper.GetSMTPPrivatePort(), cts.Token);

                using var sslStream = new SslStream(client.GetStream(), false, (sender, certificate, chain, errors) => true);
                await sslStream.AuthenticateAsClientAsync(targetHost: SettingsProvider.GetServerCertificateSubject(), clientCertificates: null, enabledSslProtocols: SslProtocols.Tls12, checkCertificateRevocation: false);

                sslStream.Flush();

                SmartStreamArray finalPackage = new();
                finalPackage.Serialize("");

                finalPackage.Append(inPackage);

                byte[] requestData = finalPackage.ToByte();
                await sslStream.WriteAsync(requestData, 0, requestData.Length);

                outPackage.LoadFromStream(sslStream);

                bool result = false;
                outPackage.Deserialize(out result);

                if (!result)
                {
                    string error = "";
                    outPackage.Deserialize(out error);
                    Logger.LogSilent(!string.IsNullOrWhiteSpace(error) ? error : LogMessages.InteralError);
                    return false;
                }

                sslStream.Flush();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogSilent(LogMessages.InteralError);
                return false;
            }
        }
    }
}
