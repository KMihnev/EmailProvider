using EmailProvider.Models.DBModels;
using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface IMessageRepository : IRepositoryS<Message>
    {
        Task<List<ViewMessage>> GetCombinedMessagesAsync(int userId, int searchType, string whereClause);
    }

    public interface IInnerMessageRepository : IRepositoryS<InnerMessage>
    {

    }

    public interface IOutgoingMessageRepository : IRepositoryS<OutgoingMessage>
    {

    }

    public interface IIncomingMessageRepository : IRepositoryS<IncomingMessage>
    {

    }
}
