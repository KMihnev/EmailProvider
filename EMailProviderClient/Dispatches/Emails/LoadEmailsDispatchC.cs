//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EmailProvider.Models.Serializables;

namespace EMailProviderClient.Dispatches.Emails
{
    //------------------------------------------------------
    //	LoadEmailsDispatchC
    //------------------------------------------------------
    public class LoadEmailsDispatchC
    {
        public static async Task<bool> LoadIncomingEmails(List<EmailListModel> outMessageList, SearchData searchData)
        {
            searchData.UserId = UserController.GetCurrentUserID();

            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadIncomingEmails);
                InPackage.Serialize(searchData);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<EmailListModel> messageList = null;
                OutPackage.Deserialize(out messageList);

                outMessageList.Clear();
                outMessageList.AddRange(messageList);
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }

            return true;
        }

        public static async Task<bool> LoadOutgoingEmails(List<EmailListModel> outMessageList, SearchData searchData)
        {
            searchData.UserId = UserController.GetCurrentUserID();

            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadOutgoingEmails);
                InPackage.Serialize(searchData);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<EmailListModel> messageList = null;
                OutPackage.Deserialize(out messageList);

                outMessageList.Clear();
                outMessageList.AddRange(messageList);
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }

            return true;
        }

        public static async Task<bool> LoadDrafts(List<EmailListModel> outMessageList, SearchData searchData)
        {
            searchData.UserId = UserController.GetCurrentUserID();
            outMessageList.Clear();
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadDrafts);
                InPackage.Serialize(searchData);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<EmailListModel> messageList = null;
                OutPackage.Deserialize(out messageList);

                outMessageList.AddRange(messageList);
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }

            return true;
        }

        public static async Task<bool> LoadEmailsByFolder(List<EmailListModel> outMessageList, SearchData searchData, int FolderId)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadEmailsByFolder);
                InPackage.Serialize(searchData);
                InPackage.Serialize(FolderId);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<EmailListModel> messageList = null;
                OutPackage.Deserialize(out messageList);

                outMessageList.Clear();
                outMessageList.AddRange(messageList);
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }

            return true;
        }
    }
}
