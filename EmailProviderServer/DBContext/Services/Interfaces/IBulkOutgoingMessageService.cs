//Includes

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IBulkOutgoingMessageService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>(int? nCount = null);

        T GetOutgoingMessage<T>(int nOutgoingMessageID);
    }
}
