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
        Task<List<Message>> GetIncomingMessagesAsync(int userId, Expression<Func<Message, bool>>? filter, int skip, int take);
        Task<List<Message>> GetOutgoingMessagesAsync(int userId, Expression<Func<Message, bool>>? filter, int skip, int take);
        Task<List<Message>> GetDraftMessagesAsync(int userId, Expression<Func<Message, bool>>? filter, int skip, int take);
        Task<List<Message>> GetMessagesInFolderAsync(int userId, int folderId, Expression<Func<Message, bool>>? filter, int skip, int take);
    }
}
