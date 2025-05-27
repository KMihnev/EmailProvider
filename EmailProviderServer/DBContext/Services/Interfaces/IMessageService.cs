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
        Task ProcessMessageAsync(MessageSerializable messageDTO);

        Task<T> GetByIDIncludingAll<T>(int id);

        Task<bool> CheckIfExists(int id);

        Task<bool> UpdateMessageAsync(int messageId, MessageSerializable updatedMessage);
    }
}
