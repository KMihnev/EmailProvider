using EmailProvider.SearchData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Services.Interfaces
{
    public interface IUserMessageService
    {
        Task<List<T>> GetIncomingMessagesAsync<T>(SearchData searchData, int UserId, int skip, int take);
        Task<List<T>> GetOutgoingMessagesAsync<T>(SearchData searchData, int UserId, int skip, int take);
        Task<List<T>> GetDraftMessagesAsync<T>(SearchData searchData, int UserId, int skip, int take);
        Task<List<T>> GetMessagesInFolderAsync<T>(SearchData searchData, int UserId, int folderId, int skip, int take);
    }
}
