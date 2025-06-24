using AutoMapper;
using EmailProvider.Enums;
using EmailProvider.SearchData;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.Helpers;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<T>> GetIncomingMessagesAsync<T>(SearchData searchData, int UserId, string userEmail)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: true);
            var messages = await _repository.GetIncomingMessagesAsync(UserId, userEmail, filter, searchData.Skip, searchData.Take, searchData.OrderBy, searchData.Keyword);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetOutgoingMessagesAsync<T>(SearchData searchData, int UserId, string userEmail)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: false);
            var messages = await _repository.GetOutgoingMessagesAsync(UserId, userEmail, filter, searchData.Skip, searchData.Take, searchData.OrderBy, searchData.Keyword);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetDraftMessagesAsync<T>(SearchData searchData, int UserId)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: false, isDraft: true);
            var messages = await _repository.GetDraftMessagesAsync(UserId, filter, searchData.Skip, searchData.Take, searchData.OrderBy, searchData.Keyword);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetMessagesInFolderAsync<T>(SearchData searchData, int UserId, int folderId)
        {
        var Folder = await _folderRepository.GetByIdAsync(folderId);

            bool bIsIncoming = Folder.FolderDirection == EmailDirections.EmailDirectionIn;

            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: bIsIncoming);
            var messages = await _repository.GetMessagesInFolderAsync(UserId, folderId, filter, searchData.Skip, searchData.Take, searchData.OrderBy, searchData.Keyword);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task<List<T>> GetDeletedMessagesAsync<T>(SearchData searchData, int UserId)
        {
            var filter = SearchExpressionBuilder.BuildExpression(searchData.Conditions, isIncoming: false, isDraft: false);

            var messages = await _repository.GetDeletedMessagesForUserAsync(UserId, filter, searchData.Skip, searchData.Take, searchData.OrderBy, searchData.Keyword);
            return _mapper.Map<List<T>>(messages);
        }

        public async Task MarkAsDeletedAsync(int userId, List<int> messageIdArray)
        {
            foreach (var messageId in messageIdArray)
                await _repository.SetIsDeletedAsync(userId, messageId, true);
        }

        public async Task MarkAsReadAsync(int userId, List<int> messageIdArray)
        {
            foreach(var messageId in messageIdArray)
                await _repository.SetIsReadAsync(userId, messageId, true);
        }

        public async Task MarkAsUnreadAsync(int userId, List<int> messageIdArray)
        {
            foreach (var messageId in messageIdArray)
                await _repository.SetIsReadAsync(userId, messageId, false);
        }

        public async Task<bool> MoveMessagesToFolderAsync(List<int> messageIds, int folderId, int userId)
        {
            if (messageIds == null || messageIds.Count == 0)
                return false;

            return await _repository.MoveMessagesToFolderAsync(messageIds, folderId, userId);
        }

        public async Task<bool> RemoveMessagesFromFolderAsync(int userId, List<int> messageIds)
        {
            if (messageIds == null || !messageIds.Any())
                return false;

            return await _repository.RemoveFromFolderAsync(userId, messageIds);
        }
    }
}
