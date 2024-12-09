//Includes
using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	BulkOutgoingMessageService
    //------------------------------------------------------
    public class BulkOutgoingMessageService : IBulkOutgoingMessageService
    {
        private readonly IRepositoryS<BulkOutgoingMessage> oBulkOutgoingMessagesRepositoryS;

        //Constructor
        public BulkOutgoingMessageService(IRepositoryS<BulkOutgoingMessage> oBulkOutgoingMessagesRepository)
        {
            this.oBulkOutgoingMessagesRepositoryS = oBulkOutgoingMessagesRepository;
        }

        //Methods
        public IEnumerable<BulkOutgoingMessage> GetAll(int? nCount = null)
        {
            IQueryable<BulkOutgoingMessage> oQuery = this.oBulkOutgoingMessagesRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public int GetCount()
        {
            return this.oBulkOutgoingMessagesRepositoryS.All().Count();
        }

        public BulkOutgoingMessage GetOutgoingMessage(int nOutgoingMessageID)
        {
            var oBulkOutgoingMessage = this.oBulkOutgoingMessagesRepositoryS
                .All()
                .Where(bOm => bOm.OutgoingMessageId == nOutgoingMessageID)
                .FirstOrDefault();

            return oBulkOutgoingMessage;
        }
    }
}
