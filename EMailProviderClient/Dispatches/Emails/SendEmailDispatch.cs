using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EmailProvider.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches.Emails
{
    public class SendEmailDispatch
    {
        public static async Task<bool> Register()
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.Register);
                //InPackage.Serialize(user);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                UserSerializable newUser = null;
                OutPackage.Deserialize(out newUser);

                if (newUser == null)
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                UserController.SetCurrentUser(newUser);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }
        }
    }
}
