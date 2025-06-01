using AutoMapper;
using EmailProvider.Enums;
using EmailProvider.SearchData;
using EmailProviderServer.DBContext.Repositories;
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
        private readonly IFolderRepository _folderRepository;
        private readonly IUserMessageRepository _repository;
        private readonly IMapper _mapper;

        public UserMessageService(IUserMessageRepository repository, IMapper mapper, IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<T>> GetIncomingMessagesAsync<T>(SearchData searchData, int UserId, int skip, int take)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: true);
            var messages = await _repository.GetIncomingMessagesAsync(UserId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetOutgoingMessagesAsync<T>(SearchData searchData, int UserId, int skip, int take)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isOutgoing: true);
            var messages = await _repository.GetOutgoingMessagesAsync(UserId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetDraftMessagesAsync<T>(SearchData searchData, int UserId, int skip, int take)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isOutgoing: true, isDraft: true);
            var messages = await _repository.GetDraftMessagesAsync(UserId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetMessagesInFolderAsync<T>(SearchData searchData, int UserId, int folderId, int skip, int take)
        {
            var Folder = await _folderRepository.GetByIdAsync(folderId);

            bool bIsIncoming = Folder.FolderDirection == EmailDirections.EmailDirectionIn;

            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: bIsIncoming);
            var messages = await _repository.GetMessagesInFolderAsync(UserId, folderId, filter, skip, take);
            return _mapper.Map<List<T>>(messages);
        }
    }
}
