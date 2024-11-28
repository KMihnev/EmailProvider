//Includes

using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IMessageService
    {
        IEnumerable<T> GetAll<T>(int? nCount = null);

        IEnumerable<T> GetAllByStatus<T>(int nStatus, int? nCount = null);

        IEnumerable<T> GetByDateOfSend<T>(DateTime dDateOfSend, DateSearchType eDateSearchType, int? nCount = null);

        IEnumerable<T> GetBySenderID<T>(int nSenderID, int? nCount = null);

        IEnumerable<T> GetByReceiverId<T>(int nReceiverID, int? nCount = null);

        IEnumerable<T> GetAllDrafts<T>(int? nCount = null);

        T GetById<T>(int nId);
    }
}
