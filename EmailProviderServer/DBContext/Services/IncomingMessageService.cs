//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.Models;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Mapping;

namespace EmailProviderServer.DBContext.Services
{
    public class IncomingMessageService : IIncomingMessageService
    {

        private readonly IRepositoryS<IncomingMessage> oIncomingMessageRepositoryS;

        public IncomingMessageService(IRepositoryS<IncomingMessage> oIncomingMessageRepository)
        {
            this.oIncomingMessageRepositoryS = oIncomingMessageRepository;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByCategoryId<T>(int nCategoryID, int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
                .All().Where(c => c.CategoryId == nCategoryID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByDateOfReceive<T>(DateTime dDateOfReceive, DateSearchType eDateSearchType, int? nCount = null)
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

            return oQuery.To<T>().ToList();
        }

        public T GetById<T>(int nId)
        {
            var oIncomingMessage = this.oIncomingMessageRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .To<T>().FirstOrDefault();
            return oIncomingMessage;
        }

        public IEnumerable<T> GetAllByReceiverId<T>(int nReceiverID, int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
                .All()
                .Where(x=>x.ReceiverId == nReceiverID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public IEnumerable<T> GetAllBySenderID<T>(int nSenderID, int? nCount = null)
        {
            IQueryable<IncomingMessage> oQuery = this.oIncomingMessageRepositoryS
               .All()
               .Where(x => x.SenderId == nSenderID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }
    }
}
