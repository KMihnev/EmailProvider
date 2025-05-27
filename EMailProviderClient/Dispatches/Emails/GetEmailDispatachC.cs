using EMailProviderClient.Dispatches.Base;
//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;

namespace EMailProviderClient.Dispatches.Emails
{
    //------------------------------------------------------
    //	GetEmailDispatchC
    //------------------------------------------------------
    public class GetEmailDispatchC
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
