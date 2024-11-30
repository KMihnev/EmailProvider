//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Enums;
using AutoMapper;
using System.Diagnostics.Metrics;
using EmailProviderServer.DBContext.Repositories;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;

namespace EmailProviderServer.DBContext.Services
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository oMessageRepositoryS;
        private readonly IMapper _mapper;

        public MessageService(MessageRepository oMessageRepository, IMapper mapper)
        {
            this.oMessageRepositoryS = oMessageRepository;
            _mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
               .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            var eMails = oQuery.ToList();

            return _mapper.Map<IEnumerable<T>>(eMails);
        }

        public IEnumerable<T> GetAllByStatus<T>(int nStatus, int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
               .All().Where(om => om.Status == nStatus);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            var eMails = oQuery.ToList();

            return _mapper.Map<IEnumerable<T>>(eMails);
        }

        public IEnumerable<T> GetAllDrafts<T>(int? nCount = null)
        {
            return GetAllByStatus<T>(EmailStatusProvider.GetDraftStatus());
        }

        public IEnumerable<T> GetByDateOfSend<T>(DateTime dDateOfSend, DateSearchType eDateSearchType, int? nCount = null)
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

            var eMails = oQuery.ToList();

            return _mapper.Map<IEnumerable<T>>(eMails);
        }

        public IEnumerable<T> GetByReceiverId<T>(int nReceiverID, int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
               .All().Where(om => om.ReceiverId == nReceiverID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            var eMails = oQuery.ToList();

            return _mapper.Map<IEnumerable<T>>(eMails);
        }

        public IEnumerable<T> GetBySenderID<T>(int nSenderID, int? nCount = null)
        {
            IQueryable<Message> oQuery = this.oMessageRepositoryS
                .All().Where(om => om.SenderId == nSenderID);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            var eMails = oQuery.ToList();

            return _mapper.Map<IEnumerable<T>>(eMails);
        }
        public T GetById<T>(int nId)
        {
            var oOutgoingMessage = this.oMessageRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return _mapper.Map<T>(oOutgoingMessage);
        }

        public async void CreateAsync<T>(T message)
        {
            await oMessageRepositoryS.AddAsync(_mapper.Map<Message>(message));

            await oMessageRepositoryS.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync<T>(T message)
        {
            Message dbRec = _mapper.Map<Message>(message);

            if (dbRec == null)
                Logger.Log(LogMessages.UserNotFound, EmailServiceIntermediate.Enums.LogType.LogTypeLog, EmailServiceIntermediate.Enums.LogSeverity.Error);

            oMessageRepositoryS.Update(dbRec);

            await oMessageRepositoryS.SaveChangesAsync();

            return _mapper.Map<T>(dbRec);
        }
    }
}
