//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Enums;

namespace EmailProviderServer.DBContext.Services
{
    public class IncomingMessageService : IIncomingMessageService
    {

        private readonly IRepositoryS<IncomingMessage> oIncomingMessageRepositoryS;

        public IncomingMessageService(IRepositoryS<IncomingMessage> oIncomingMessageRepository)
        {
            this.oIncomingMessageRepositoryS = oIncomingMessageRepository;
        }

        public IEnumerable<IncomingMessage> GetAll(int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<IncomingMessage> GetAllByCategoryId(int nCategoryID, int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
                .All().Where(c => c.CategoryId == nCategoryID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<IncomingMessage> GetAllByDateOfReceive(DateTime dDateOfReceive, DateSearchType eDateSearchType, int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = oIncomingMessageRepositoryS.All().Take(0);

            switch (eDateSearchType)
            {
                case DateSearchType.DateSearchTypeAfter:
                    this.oIncomingMessageRepositoryS
                    .All().Where(x => x.DateOfReceive > dDateOfReceive);
                    break;
                case DateSearchType.DateSearchTypeBefore:
                    this.oIncomingMessageRepositoryS
                    .All().Where(x => x.DateOfReceive < dDateOfReceive);
                    break;
                case DateSearchType.DateSearchTypeSame:
                    this.oIncomingMessageRepositoryS
                    .All().Where(x => x.DateOfReceive.Day == dDateOfReceive.Day 
                              && x.DateOfReceive.Month == dDateOfReceive.Month
                              && x.DateOfReceive.Year == dDateOfReceive.Year);
                    break;
                default:
                    this.oIncomingMessageRepositoryS
                    .All();
                    break;
            }

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IncomingMessage GetById(int nId)
        {
            var oIncomingMessage = this.oIncomingMessageRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return oIncomingMessage;
        }

        public IEnumerable<IncomingMessage> GetAllByReceiverId(int nReceiverID, int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
                .All()
                .Where(x=>x.ReceiverId == nReceiverID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<IncomingMessage> GetAllBySenderID(int nSenderID, int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
               .All()
               .Where(x => x.SenderId == nSenderID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }
    }
}
