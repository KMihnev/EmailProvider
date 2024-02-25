//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.Models;
using EmailServiceIntermediate.Mapping;

namespace EmailProviderServer.DBContext.Services
{
    public class BulkOutgoingMessageService : IBulkOutgoingMessageService
    {
        private readonly IRepositoryS<BulkOutgoingMessage> oBulkOutgoingMessagesRepositoryS;

        public BulkOutgoingMessageService(IRepositoryS<BulkOutgoingMessage> oBulkOutgoingMessagesRepository)
        {
            this.oBulkOutgoingMessagesRepositoryS = oBulkOutgoingMessagesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<BulkOutgoingMessage> oQuery = this.oBulkOutgoingMessagesRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public int GetCount()
        {
            return this.oBulkOutgoingMessagesRepositoryS.All().Count();
        }

        public T GetOutgoingMessage<T>(int nOutgoingMessageID)
        {
            var oBulkOutgoingMessage = this.oBulkOutgoingMessagesRepositoryS
                .All()
                .Where(bOm => bOm.OutgoingMessageId == nOutgoingMessageID)
                .To<T>()
                .FirstOrDefault();

            return oBulkOutgoingMessage;
        }
    }
}
