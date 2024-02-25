//Includes

using EmailServiceIntermediate.Enums;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IIncomingMessageService
    {
        IEnumerable<T> GetAll<T>(int? nCount = null);


        IEnumerable<T> GetAllByCategoryId<T>(int nCategoryID, int? nCount = null);

        IEnumerable<T> GetAllByDateOfReceive<T>(DateTime dDateOfReceive, DateSearchType eDateSearchType, int? nCount = null);

        IEnumerable<T> GetAllBySenderID<T>(int nSenderID, int? nCount = null);

        IEnumerable<T> GetAllByReceiverId<T>(int nReceiverID, int? nCount = null);

        T GetById<T>(int nId);
    }
}
