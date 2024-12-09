//Includes
using EmailProvider.Models.DBModels;
using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    //------------------------------------------------------
    //	IMessageRepository
    //------------------------------------------------------
    public interface IMessageRepository : IRepositoryS<Message>
    {
        Task<List<ViewMessage>> GetCombinedMessagesAsync(int userId, int searchType, string whereClause);

        Task<Message> GetByIDIncludingAll(int nId);
    }

    //------------------------------------------------------
    //	IInnerMessageRepository
    //------------------------------------------------------
    public interface IInnerMessageRepository : IRepositoryS<InnerMessage>
    {

    }

    //------------------------------------------------------
    //	IOutgoingMessageRepository
    //------------------------------------------------------
    public interface IOutgoingMessageRepository : IRepositoryS<OutgoingMessage>
    {

    }

    //------------------------------------------------------
    //	IIncomingMessageRepository
    //------------------------------------------------------
    public interface IIncomingMessageRepository : IRepositoryS<IncomingMessage>
    {

    }
}
