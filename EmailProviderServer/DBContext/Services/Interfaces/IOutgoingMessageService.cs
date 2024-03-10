//Includes

using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IOutgoingMessageService
    {
        IEnumerable<OutgoingMessage> GetAll(int? nCount = null);

        IEnumerable<OutgoingMessage> GetAllByStatus(string strStatus, int? nCount = null);

        IEnumerable<OutgoingMessage> GetByDateOfSend(DateTime dDateOfSend, DateSearchType eDateSearchType, int? nCount = null);

        IEnumerable<OutgoingMessage> GetBySenderID(int nSenderID, int? nCount = null);

        IEnumerable<OutgoingMessage> GetByReceiverId(int nReceiverID, int? nCount = null);

        IEnumerable<OutgoingMessage> GetAllDrafts(int? nCount = null);

        OutgoingMessage GetById(int nId);
    }
}
