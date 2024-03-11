using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EmailProvider.Reponse;
using EMailProviderClient.UserControl;
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

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                await dispatchHandlerC.SendRequest(request);

                if(!await dispatchHandlerC.HandleResponse())
                    return false;

                User newUser = new User();
                if (dispatchHandlerC.Response.Data != null)
                    newUser = JsonSerializer.Deserialize<User>(dispatchHandlerC.Response.Data.ToString());

                if (newUser != null)
                    UserController.SetCurrentUser(user);
                else
                    return false;

                return true;

            }
            catch (Exception)
            {
                Logger.Log(LogMessages.DispatchErrorRegister, LogType.LogTypeScreen, LogSeverity.Error);
                return false;
            }
        }

        public static async Task<bool> SetUpProfile(User user)
        {
            await DispatchHandlerC.InitClient();

            try
            {
                // Construct the request
                var request = new MethodRequest
                {
                    eDispatch = DispatchEnums.SetUpProfile,
                    Parameters = JsonDocument.Parse(JsonSerializer.Serialize(new { user })).RootElement
                };

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                await dispatchHandlerC.SendRequest(request);

                if (!await dispatchHandlerC.HandleResponse())
                    return false;

                return true;

            }
            catch (Exception)
            {
                Logger.Log(LogMessages.DispatchErrorSetUpProfile, LogType.LogTypeScreen, LogSeverity.Error);
                return false;
            }
        }
    }
}
