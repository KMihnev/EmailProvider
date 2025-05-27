using AutoMapper;
using EmailProvider.SearchData;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IUserMessageRepository _repository;
        private readonly IMapper _mapper;

        public UserMessageService(IUserMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<T>> GetIncomingMessagesAsync<T>(SearchData searchData, int skip, int take)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: true);
            var messages = await _repository.GetIncomingMessagesAsync(searchData.UserId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetOutgoingMessagesAsync<T>(SearchData searchData, int skip, int take)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isOutgoing: true);
            var messages = await _repository.GetOutgoingMessagesAsync(searchData.UserId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetDraftMessagesAsync<T>(SearchData searchData, int skip, int take)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isOutgoing: true, isDraft: true);
            var messages = await _repository.GetDraftMessagesAsync(searchData.UserId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetMessagesInFolderAsync<T>(SearchData searchData, int folderId, int skip, int take)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions);
            var messages = await _repository.GetMessagesInFolderAsync(searchData.UserId, folderId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }
    }
}
