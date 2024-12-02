//Includes

using EmailProvider.Models.Serializables.Base;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IMessageService
    {
        Task ProcessMessageAsync<TMessageDTO>(TMessageDTO messageDTO) where TMessageDTO : SendMessageDTOBase;
    }
}
