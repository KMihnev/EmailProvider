//Includes

using EmailProvider.Models.DBModels;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Models.Serializables;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IMessageService
    {
        Task ProcessMessageAsync(MessageSerializable messageDTO);

        Task<List<ViewMessage>> GetCombinedMessagesAsync(SearchData seacrhData);

        Task<T> GetByIDIncludingAll<T>(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<bool> CheckIfExists(int id);

        Task<bool> UpdateMessageAsync(int messageId, MessageSerializable updatedMessage);
    }
}
