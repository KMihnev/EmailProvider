using EMailProviderClient.Dispatches.Base;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches.Emails
{
    internal class GetEmailDispatchC
    {
        public static async Task<(bool, MessageSerializable)> LoadEmail(int nMessageId)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.GetEmail);
                InPackage.Serialize(nMessageId);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return (false, null);
                }

                MessageSerializable outMessage;
                OutPackage.Deserialize(out outMessage);

                return (true, outMessage);
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return (false, null);
            }
        }
    }
}
