using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EMailProviderClient.UserControl;
using EmailServiceIntermediate.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class UserDispatchesC
    {
        public static async Task<bool> Register(User user)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.Register);
                InPackage.Serialize(user);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return false;
                }

                User newUser = null;
                OutPackage.Deserialize(out newUser);

                if (newUser == null)
                {
                    return false;
                }

                UserController.SetCurrentUser(newUser);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"{LogMessages.DispatchErrorRegister}: {ex.Message}", LogType.LogTypeScreen, LogSeverity.Error);
                return false;
            }
        }

        public static async Task<bool> SetUpProfile(User user)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                InPackage.Serialize((int)DispatchEnums.SetUpProfile);
                InPackage.Serialize(user);

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();
                if(!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return false;
                }

                bool bResponse = false;
                OutPackage.Deserialize(out bResponse);
                if (bResponse)
                {
                    return false;
                }

                User updatedUser = null;
                OutPackage.Deserialize(out updatedUser);

                if (updatedUser != null)
                {
                    UserController.SetCurrentUser(updatedUser);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.Log($"{LogMessages.DispatchErrorSetUpProfile}: {ex.Message}", LogType.LogTypeScreen, LogSeverity.Error);
                return false;
            }

            return true;
        }
    }
}
