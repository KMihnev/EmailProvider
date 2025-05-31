//Includes


using EmailProvider.Enums;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    //------------------------------------------------------
    //	IFolderService
    //------------------------------------------------------
    public interface IFolderService
    {
        Task<List<T>> GetUserFoldersAsync<T>(int userId);
        Task<T?> GetFolderByIdAsync<T>(int folderId);
        Task CreateFolderAsync<T>(T folder);
        Task DeleteFolderAsync(int folderId);
    }
}
