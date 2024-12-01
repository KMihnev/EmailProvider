using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface IMessageRepository : IRepositoryS<Message>
    {
        Task<int> AddMessageAsync(Message message);
        Task<Message> GetMessageAsync(int messageId);
    }

    public interface IInnerMessageRepository : IRepositoryS<InnerMessage>
    {
        Task AddInnerMessageAsync(InnerMessage innerMessage);
    }

    public interface IOutgoingMessageRepository : IRepositoryS<OutgoingMessage>
    {
        Task AddOutgoingMessageAsync(OutgoingMessage outgoingMessage);
    }

    public interface IIncomingMessageRepository : IRepositoryS<IncomingMessage>
    {
        Task AddOutgoingMessageAsync(OutgoingMessage outgoingMessage);
    }
}
