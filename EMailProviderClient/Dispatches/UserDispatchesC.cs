using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class UserDispatchesC
    {
        public static async Task<bool> Register(User user)
        {
            await DispatchHandlerC.InitClient();

            try
            {
                // Construct the request
                var request = new MethodRequest
                {
                    eDispatch = DispatchEnums.Register,
                    Parameters = JsonDocument.Parse(JsonSerializer.Serialize(new { user })).RootElement
                };

                string requestJson = JsonSerializer.Serialize(request);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson + "\n");

                await DispatchHandlerC.Stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                byte[] responseBytes = new byte[1024];
                int bytesRead = await DispatchHandlerC.Stream.ReadAsync(responseBytes, 0, responseBytes.Length);
                string response = Encoding.UTF8.GetString(responseBytes, 0, bytesRead);
            }
            catch (Exception)
            {
                Logger.Log(LogMessages.DispatchErrorRegister, LogType.LogTypeScreen, LogSeverity.Error);
            }

            return true;
        }
    }
}
