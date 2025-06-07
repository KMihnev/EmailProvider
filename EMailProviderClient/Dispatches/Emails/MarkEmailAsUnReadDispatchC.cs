//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.SearchData;

namespace EMailProviderClient.Dispatches.Emails
{
    //------------------------------------------------------
    //	LoadEmailsDispatchC
    //------------------------------------------------------
    public class MarkEmailAsUnReadDispatchC
    {
        public static async Task<bool> MarkEmailsAsUnRead(List<int> messagesToRead)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.MarkEmailsAsUnread);
                InPackage.Serialize(messagesToRead);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

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
