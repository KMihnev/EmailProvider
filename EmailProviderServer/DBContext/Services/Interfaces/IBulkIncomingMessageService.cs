//Includes

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IBulkIncomingMessageService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>(int? nCount = null);
    }
}
