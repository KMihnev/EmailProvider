//Includes
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	BulkIncomingMessageService
    //------------------------------------------------------
    public class BulkIncomingMessageService : IBulkIncomingMessageService
    {
        private readonly IBulkIncomingMessagesRepositoryS oBulkIncomingMessagesRepositoryS;

        //Constructor
        public BulkIncomingMessageService(IBulkIncomingMessagesRepositoryS oBulkIncomingMessagesRepository)
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
