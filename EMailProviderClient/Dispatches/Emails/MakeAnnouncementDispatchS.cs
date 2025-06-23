//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Dispatches.Base;

namespace EMailProviderClient.Dispatches.Emails
{
    //------------------------------------------------------
    //	MakeAnnouncementDispatchC
    //------------------------------------------------------
    public class MakeAnnouncementDispatchC
    {
        public static async Task<bool> MakeAnnouncement(EmailViewModel messageSerializable)
        {
            try
            {
                messageSerializable.IsAccouncement = true;
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.MakeAnnouncement);
                InPackage.Serialize(messageSerializable);

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
