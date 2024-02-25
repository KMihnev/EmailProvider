//Includes

namespace EmailProviderServer.DBContext.Services
{
    public interface IBulkOutgoingMessageService
    {
        int GetCount(); 

        IEnumerable<T> GetAll<T>(int? nCount = null);

        T GetOutgoingMessage<T>(int nOutgoingMessageID);
    }
}
