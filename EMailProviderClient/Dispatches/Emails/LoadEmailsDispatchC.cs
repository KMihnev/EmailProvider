//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;

namespace EMailProviderClient.Dispatches.Emails
{
    //------------------------------------------------------
    //	LoadEmailsDispatchC
    //------------------------------------------------------
    public class LoadEmailsDispatchC
    {
        public static async Task<bool> LoadIncomingEmails(List<MessageSerializable> outMessageList, SearchData searchData)
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

                List<MessageSerializable> messageList = null;
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

        public static async Task<bool> LoadOutgoingEmails(List<MessageSerializable> outMessageList, SearchData searchData)
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

                List<MessageSerializable> messageList = null;
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

        public static async Task<bool> LoadDrafts(List<MessageSerializable> outMessageList, SearchData searchData)
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

                List<MessageSerializable> messageList = null;
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

        public static async Task<bool> LoadEmailsByFolder(List<MessageSerializable> outMessageList, SearchData searchData, int FolderId)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadIncomingEmails);
                InPackage.Serialize(FolderId);
                InPackage.Serialize(searchData);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<MessageSerializable> messageList = null;
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
