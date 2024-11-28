//Includes

using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAll(int? nCount = null);

        IEnumerable<Message> GetAllByStatus(int nStatus, int? nCount = null);

        IEnumerable<Message> GetByDateOfSend(DateTime dDateOfSend, DateSearchType eDateSearchType, int? nCount = null);

        IEnumerable<Message> GetBySenderID(int nSenderID, int? nCount = null);

        IEnumerable<Message> GetByReceiverId(int nReceiverID, int? nCount = null);

        IEnumerable<Message> GetAllDrafts(int? nCount = null);

        Message GetById(int nId);
    }
}
