//Includes
using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	BulkIncomingMessageService
    //------------------------------------------------------
    public class BulkIncomingMessageService : IBulkIncomingMessageService
    {
        private readonly IRepositoryS<BulkIncomingMessage> oBulkIncomingMessagesRepositoryS;

        //Constructor
        public BulkIncomingMessageService(IRepositoryS<BulkIncomingMessage> oBulkIncomingMessagesRepository)
        {
            this.oBulkIncomingMessagesRepositoryS = oBulkIncomingMessagesRepository;
        }

        //Methods
        public IEnumerable<BulkIncomingMessage> GetAll(int? nCount = null)
        {
            IQueryable<BulkIncomingMessage> oQuery = this.oBulkIncomingMessagesRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public int GetCount()
        {
            return this.oBulkIncomingMessagesRepositoryS.All().Count();
        }
    }
}
