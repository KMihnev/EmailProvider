using EmailServiceIntermediate.Models;
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
        Task<List<UserMessage>> GetIncomingMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take);
        Task<List<UserMessage>> GetOutgoingMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take);
        Task<List<UserMessage>> GetDraftMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take);
        Task<List<UserMessage>> GetMessagesInFolderAsync(int userId, int folderId, Expression<Func<UserMessage, bool>>? filter, int skip, int take);
    }
}
