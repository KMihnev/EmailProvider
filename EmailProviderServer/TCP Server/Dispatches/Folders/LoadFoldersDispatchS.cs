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
    public class LoadFoldersDispatchS : BaseDispatchHandler
    {
        private readonly IFolderService _folderService;

        //Constructor
        public LoadFoldersDispatchS(IFolderService folderService)
        {
            _folderService = folderService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                List<FolderViewModel> folderList = new List<FolderViewModel>();
                folderList = await _folderService.GetUserFoldersAsync<FolderViewModel>(SessionUser.Id);
                OutPackage.Serialize(true);
                OutPackage.Serialize(folderList);
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
