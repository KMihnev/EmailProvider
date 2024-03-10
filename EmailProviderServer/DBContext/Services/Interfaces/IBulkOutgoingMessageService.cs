//Includes

using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IBulkOutgoingMessageService
    {
        int GetCount();

        IEnumerable<BulkOutgoingMessage> GetAll(int? nCount = null);

        BulkOutgoingMessage GetOutgoingMessage(int nOutgoingMessageID);
    }
}
