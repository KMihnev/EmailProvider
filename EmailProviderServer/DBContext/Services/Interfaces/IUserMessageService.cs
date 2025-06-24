using EmailProvider.SearchData;
using EmailServiceIntermediate.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Services.Interfaces
{
    public interface IUserMessageService
    {
        Task<List<T>> GetIncomingMessagesAsync<T>(SearchData searchData, int UserId,  string userEmail);
        Task<List<T>> GetOutgoingMessagesAsync<T>(SearchData searchData, int UserId, string userEmail);
        Task<List<T>> GetDraftMessagesAsync<T>(SearchData searchData, int UserId);
        Task<List<T>> GetMessagesInFolderAsync<T>(SearchData searchData, int UserId, int folderId);
        Task<List<T>> GetDeletedMessagesAsync<T>(SearchData searchData, int UserId);
        Task MarkAsDeletedAsync(int userId, List<int> messageIdArray);
        Task MarkAsReadAsync(int userId, List<int> messageIdArray);
        Task MarkAsUnreadAsync(int userId, List<int> messageIdArray);
        Task<bool> MoveMessagesToFolderAsync(List<int> messageIds, int folderId, int userId);
        Task<bool> RemoveMessagesFromFolderAsync(int userId, List<int> messageIds);
    }
}
