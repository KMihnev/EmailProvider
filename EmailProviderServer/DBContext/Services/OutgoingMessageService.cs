//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Enums;

namespace EmailProviderServer.DBContext.Services
{
    public class OutgoingMessageService : IOutgoingMessageService
    {
        private readonly IRepositoryS<OutgoingMessage> oOutgoingMessageRepositoryS;

        public OutgoingMessageService(IRepositoryS<OutgoingMessage> oOutgoingMessageRepository)
        {
            this.oOutgoingMessageRepositoryS = oOutgoingMessageRepository; 
        }

        public IEnumerable<OutgoingMessage> GetAll(int? nCount = null)
        {
            IQueryable<OutgoingMessage> oQuery = this.oOutgoingMessageRepositoryS
               .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<OutgoingMessage> GetAllByStatus(string strStatus, int? nCount = null)
        {
            IQueryable<OutgoingMessage> oQuery = this.oOutgoingMessageRepositoryS
               .All().Where(om => om.Status == strStatus);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<OutgoingMessage> GetAllDrafts(int? nCount = null)
        {
            IQueryable<OutgoingMessage> oQuery = this.oOutgoingMessageRepositoryS
               .All().Where(om => om.IsDraft == true);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<OutgoingMessage> GetByDateOfSend(DateTime dDateOfSend, DateSearchType eDateSearchType, int? nCount = null)
        {
            IQueryable<OutgoingMessage> oQuery = oOutgoingMessageRepositoryS.All().Take(0);

            switch (eDateSearchType)
            {
                case DateSearchType.DateSearchTypeAfter:
                    this.oOutgoingMessageRepositoryS
                    .All().Where(x => x.DateOfSend > dDateOfSend);
                    break;
                case DateSearchType.DateSearchTypeBefore:
                    this.oOutgoingMessageRepositoryS
                    .All().Where(x => x.DateOfSend < dDateOfSend);
                    break;
                case DateSearchType.DateSearchTypeSame:
                    this.oOutgoingMessageRepositoryS
                    .All().Where(x => x.DateOfSend.Day == dDateOfSend.Day
                              && x.DateOfSend.Month == dDateOfSend.Month
                              && x.DateOfSend.Year == dDateOfSend.Year);
                    break;
                default:
                    this.oOutgoingMessageRepositoryS
                    .All();
                    break;
            }

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<OutgoingMessage> GetByReceiverId(int nReceiverID, int? nCount = null)
        {
            IQueryable<OutgoingMessage> oQuery = this.oOutgoingMessageRepositoryS
               .All().Where(om => om.ReceiverId == nReceiverID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<OutgoingMessage> GetBySenderID(int nSenderID, int? nCount = null)
        {
            IQueryable<OutgoingMessage> oQuery = this.oOutgoingMessageRepositoryS
                .All().Where(om => om.SenderId == nSenderID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }
        public OutgoingMessage GetById(int nId)
        {
            var oOutgoingMessage = this.oOutgoingMessageRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return oOutgoingMessage;
        }
    }
}
