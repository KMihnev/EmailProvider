using EmailProviderServer.ServiceDispatches;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;

namespace EmailProviderServer.TCP_Server.ServiceDispatches
{
    public class SendEmailServiceDispatchS
    {
        public static async Task<bool> SendEmail(EmailServiceModel messageSerializable)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.SendEmailToService);
                InPackage.Serialize(messageSerializable);

                //Изпращаме заявката
                DispatchHandlerServiceS dispatchHandlerServiceS = new DispatchHandlerServiceS();

                if (!await dispatchHandlerServiceS.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCallingSilent();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogErrorCallingSilent();
                return false;
            }
        }
    }
}
