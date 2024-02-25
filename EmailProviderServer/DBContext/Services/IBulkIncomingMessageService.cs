//Includes

namespace EmailProviderServer.DBContext.Services
{
    public interface IBulkIncomingMessageService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>(int? nCount = null);
    }
}
