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
    public class DeleteFolderDispatchS : BaseDispatchHandler
    {
        private readonly IFolderService _folderService;

        //Constructor
        public DeleteFolderDispatchS(IFolderService folderService)
        {
            _folderService = folderService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            int folderID = 0;
            try
            {
                InPackage.Deserialize(out folderID);
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            if (folderID == 0)
            {
                Logger.LogNullValue();
                return false;
            }

            Folder? folder = await _folderService.GetFolderByIdAsync<Folder>(folderID);
            if (folder == null)
                return false;

            if (folder.UserId != SessionUser.Id)
                return false;

            await _folderService.DeleteFolderAsync(folderID);

            try
            {
                OutPackage.Serialize(true);
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
