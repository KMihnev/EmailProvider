//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models;

namespace EMailProviderClient.Dispatches.Emails
{
    //------------------------------------------------------
    //	MoveEmailToFolderDispatchC
    //------------------------------------------------------
    public class MoveEmailToFolderDispatchC
    {
        public static async Task<bool> MoveEmailToFolder(List<int> messagesToMove, int FolderId)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.MoveEmailsToFolder);
                InPackage.Serialize(messagesToMove);
                InPackage.Serialize(FolderId);

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

        public static async Task<bool> RemoveFromFolder(List<int> messageIds)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.RemoveEmailsFromFolder);
                InPackage.Serialize(messageIds);

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
