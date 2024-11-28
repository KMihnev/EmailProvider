//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Enums;

namespace EmailProviderServer.DBContext.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepositoryS<Message> oMessageRepositoryS;

        public MessageService(IRepositoryS<Message> oOutgoingMessageRepository)
        {
            this.oMessageRepositoryS = oOutgoingMessageRepository; 
        }

        public IEnumerable<Message> GetAll(int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
               .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<Message> GetAllByStatus(int nStatus, int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
               .All().Where(om => om.StatusId == nStatus);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<Message> GetAllDrafts(int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
               .All().Where(om => om.Status.Value == "Draft");

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<Message> GetByDateOfSend(DateTime dDateOfSend, DateSearchType eDateSearchType, int? nCount = null)
        {
            IQueryable<Message> oQuery = oMessageRepositoryS.All().Take(0);

            switch (eDateSearchType)
            {
                case DateSearchType.DateSearchTypeAfter:
                    this.oMessageRepositoryS
                    .All().Where(x => x.DateOfCompletion > dDateOfSend);
                    break;
                case DateSearchType.DateSearchTypeBefore:
                    this.oMessageRepositoryS
                    .All().Where(x => x.DateOfCompletion < dDateOfSend);
                    break;
                case DateSearchType.DateSearchTypeSame:
                    this.oMessageRepositoryS
                    .All().Where(x => x.DateOfCompletion.Day == dDateOfSend.Day
                              && x.DateOfCompletion.Month == dDateOfSend.Month
                              && x.DateOfCompletion.Year == dDateOfSend.Year);
                    break;
                default:
                    this.oMessageRepositoryS
                    .All();
                    break;
            }

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<Message> GetByReceiverId(int nReceiverID, int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
               .All().Where(om => om.ReceiverId == nReceiverID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<Message> GetBySenderID(int nSenderID, int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
                .All().Where(om => om.SenderId == nSenderID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }
        public Message GetById(int nId)
        {
            var oOutgoingMessage = this.oMessageRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return oOutgoingMessage;
        }
    }
}
