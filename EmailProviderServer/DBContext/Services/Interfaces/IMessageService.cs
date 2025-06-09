//Includes
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models.Serializables;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    //------------------------------------------------------
    //	IMessageService
    //------------------------------------------------------
    public interface IMessageService
    {
        Task ProcessMessageAsync(EmailViewModel messageDTO);

        Task<T> GetByIDIncludingAll<T>(int id);

        Task<bool> CheckIfExists(int id);

        Task<bool> UpdateMessageAsync(int messageId, EmailViewModel updatedMessage);

        Task<List<EmailViewModel>> GetMessagesForSending(int take);

        Task AddBulkIncomingMessagesAsyncm<T>(T bulkIncomingMessageModel);

        Task AddBulkOutgoingMessagesAsyncm<T>(T bulkIncomingMessageModel);

        Task UpdateSentStatusAsync(int messageId);
    }
}
