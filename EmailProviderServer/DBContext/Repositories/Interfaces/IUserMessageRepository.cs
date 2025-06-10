using EmailProvider.Enums;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface IUserMessageRepository
    {
        Task<List<UserMessage>> GetIncomingMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy);
        Task<List<UserMessage>> GetOutgoingMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy);
        Task<List<UserMessage>> GetDraftMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy);
        Task<List<UserMessage>> GetMessagesInFolderAsync(int userId, int folderId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy);
        Task<List<UserMessage>> GetDeletedMessagesForUserAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy);
        Task<UserMessage> GetByUserAndMessageIdAsync(int userId, int messageId);
        Task SetIsDeletedAsync(int userId, int messageId, bool isDeleted);
        Task SetIsReadAsync(int userId, int messageId, bool isRead);
        Task<bool> MoveMessagesToFolderAsync(List<int> messageIds, int folderId);
        Task<bool> RemoveFromFolderAsync(int userId, List<int> messageIds);
    }
}
