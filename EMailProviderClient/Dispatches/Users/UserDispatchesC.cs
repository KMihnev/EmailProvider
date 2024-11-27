using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EmailProvider.Models.Serializables;
using EMailProviderClient.Dispatches.Base;
using EMailProviderClient.UserControl;
using EmailServiceIntermediate.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches.Users
{
    public class UserDispatchesC
    {
        public static async Task<bool> Register(EmailServiceIntermediate.Models.User user)
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
                    Logger.LogError(LogMessages.DispatchErrorRegister);
                    return false;
                }

                UserSerializable newUser = null;
                OutPackage.Deserialize(out newUser);

                if (newUser == null)
                {
                    Logger.LogError(string.Format(LogMessages.NullObject, newUser.GetType().Name));
                    return false;
                }

                UserController.SetCurrentUser(newUser);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.ErrorCalling, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public static async Task<bool> SetUpProfile(UserSerializable user)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.SetUpProfile);
                InPackage.Serialize(user);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogError(LogMessages.DispatchErrorRegister);
                    return false;
                }

                UserSerializable updatedUser = null;
                OutPackage.Deserialize(out updatedUser);

                if (updatedUser == null)
                {
                    Logger.LogError(string.Format(LogMessages.NullObject, updatedUser.GetType().Name));
                    return false;
                }

                UserController.SetCurrentUser(updatedUser);
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.ErrorCalling, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

            return true;
        }
    }
}
