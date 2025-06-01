//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.DBContext.Services;
using EmailProvider.Models.Serializables;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoadEmailsDispatchS
    //------------------------------------------------------
    public class AddFolderDispatchS : BaseDispatchHandler
    {
        private readonly IFolderService _folderService;

        //Constructor
        public AddFolderDispatchS(IFolderService folderService)
        {
            _folderService = folderService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            FolderViewModel folder = null;
            try
            {
                InPackage.Deserialize(out folder);
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            if (folder == null)
            {
                Logger.LogNullValue();
                return false;
            }

            try
            {
                folder = await _folderService.CreateFolderAsync<FolderViewModel>(folder, SessionUser.Id);
                if (folder == null)
                    return false;

                OutPackage.Serialize(true);
                OutPackage.Serialize(folder);
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            return true;
        }
    }
}
