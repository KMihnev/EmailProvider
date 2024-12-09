//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.SearchData;
using EmailProvider.Models.DBModels;

namespace EMailProviderClient.Dispatches.Emails
{
    //------------------------------------------------------
    //	LoadEmailsDispatchC
    //------------------------------------------------------
    public class LoadEmailsDispatchC
    {
        public static async Task<bool> LoadEmails(List<ViewMessage> outMessageList, SearchData searchData)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadEmails);
                InPackage.Serialize(searchData);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<ViewMessage> messageList = null;
                OutPackage.Deserialize(out messageList);

                outMessageList.Clear();
                outMessageList.AddRange(messageList);

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
