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
    public class AddFolderDispatchC
    {
        public static async Task<bool> AddFolder(FolderViewModel folder)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.AddFolder);
                InPackage.Serialize(folder);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                FolderViewModel newfolder = null;
                OutPackage.Deserialize(out newfolder);

                folder = newfolder;

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
