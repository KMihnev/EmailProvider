//Includes

using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IIncomingMessageService
    {
        IEnumerable<IncomingMessage> GetAll(int? nCount = null);


        IEnumerable<IncomingMessage> GetAllByCategoryId(int nCategoryID, int? nCount = null);

        IEnumerable<IncomingMessage> GetAllByDateOfReceive(DateTime dDateOfReceive, DateSearchType eDateSearchType, int? nCount = null);

        IEnumerable<IncomingMessage> GetAllBySenderID(int nSenderID, int? nCount = null);

        IEnumerable<IncomingMessage> GetAllByReceiverId(int nReceiverID, int? nCount = null);

        IncomingMessage GetById(int nId);
    }
}
