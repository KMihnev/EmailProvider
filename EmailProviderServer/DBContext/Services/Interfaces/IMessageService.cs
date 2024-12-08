//Includes

using EmailProvider.Models.DBModels;
using EmailProvider.Models.Serializables;
using EmailProvider.Models.Serializables.Base;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Models.Serializables;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IMessageService
    {
        Task ProcessMessageAsync<TMessageDTO>(TMessageDTO messageDTO) where TMessageDTO : BaseMessageSerializable;

        Task<List<ViewMessage>> GetCombinedMessagesAsync(SearchData seacrhData);
    }
}
