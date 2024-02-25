//Includes

namespace EmailProviderServer.DBContext.Services
{
    public interface IIncomingMessageService
    {
        IEnumerable<T> GetAll<T>(int? nCount = null);

        IEnumerable<T> GetAllByCategoryId<T>(int nCategoryID, int? nCount = null);

        T GetById<T>(int nId);

        T GetBySenderID <T>(int nSenderID);

        T GetByReceiverId<T>(int nReceiverID);

        T GetByDateOfReceiver<T>(DateTime dDateTime);
        
    }
}
