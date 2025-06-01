//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;

namespace EMailProviderClient.Dispatches.Folders
{
    //------------------------------------------------------
    //	SendEmailDispatchC
    //------------------------------------------------------
    public class LoadFoldersDispatchC
    {
        public static async Task<bool> LoadFolders(List<FolderViewModel> folders)
        {
            folders.Clear();
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadFolders);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<FolderViewModel> foldersList = null;
                OutPackage.Deserialize(out foldersList);

                folders.AddRange(foldersList);

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
