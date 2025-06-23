//Includes
using EmailProviderServer.DBContext.Services.Base;
using AutoMapper;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EmailServiceIntermediate.Models;
using EmailProviderServer.DBContext.Repositories;
using EmailProvider.Enums;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	MessageService
    //------------------------------------------------------
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IBulkIncomingMessagesRepositoryS _bulkIncomingMessagesRepository;
        private readonly IBulkOutgoingMessagesRepositoryS _bulkOutgoingMessagesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //Constructor
        public MessageService(
            IMessageRepository messageRepository,
            IBulkIncomingMessagesRepositoryS bulkIncomingMessagesRepository,
            IBulkOutgoingMessagesRepositoryS bulkOutgoingMessagesRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _bulkIncomingMessagesRepository = bulkIncomingMessagesRepository;
            _bulkOutgoingMessagesRepository = bulkOutgoingMessagesRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ProcessMessageAsync(EmailViewModel dto)
        {
            bool bIsForUs = false;

            var message = new Message
            {
                FromEmail = dto.FromEmail,
                Subject = dto.Subject,
                Body = dto.Body,
                DateOfRegistration = DateTime.UtcNow,
                Direction = dto.Direction,
                Status = dto.Status,
            };

            if (dto.IsAccouncement)
            {
                var allUsers = await _userRepository.AllAsNoTracking().Where(x=>x.Email != dto.FromEmail).ToListAsync();
                bIsForUs = true;

                foreach (var user in allUsers)
                {
                    var messageRecipient = new MessageRecipient
                    {
                        Email = user.Email,
                        IsOurUser = true
                    };
                    message.MessageRecipients.Add(messageRecipient);

                    message.UserMessages.Add(new UserMessage
                    {
                        UserId = user.Id,
                        IsRead = false,
                        IsDeleted = false
                    });
                }
            }
            else
            {
                foreach (var recipientDto in dto.Recipients)
                {
                    var internalUser = await _userRepository.GetUserByEmail(recipientDto.Email);

                    var messageRecipient = new MessageRecipient();
                    messageRecipient.Email = recipientDto.Email;

                    if (internalUser != null)
                    {
                        bIsForUs = true;
                        messageRecipient.IsOurUser = true;
                        message.UserMessages.Add(new UserMessage
                        {
                            UserId = internalUser.Id,
                            IsRead = false,
                            IsDeleted = false,
                        });
                    }

                    message.MessageRecipients.Add(messageRecipient);
                }
            }

            var sender = await _userRepository.GetUserByEmail(dto.FromEmail);
            if (sender != null)
            {
                bIsForUs = true;
                message.UserMessages.Add(new UserMessage
                {
                    UserId = sender.Id,
                    IsRead = true,
                    IsDeleted = false,
                });
            }

            // Add files
            foreach (var fileDto in dto.Files)
            {
                message.Files.Add(new EmailServiceIntermediate.Models.File
                {
                    Name = fileDto.Name,
                    Content = fileDto.Content
                });
            }

            if(bIsForUs)
            {
                await _messageRepository.AddAsync(message);
                await _messageRepository.SaveChangesAsync();
                dto.Id = message.Id;
            }

        }

        public async Task<T> GetByIDIncludingAll<T>(int id)
        {
            var message = await _messageRepository.GetByIDIncludingAll(id);
            return _mapper.Map<T>(message);
        }

        public async Task<T> GetByID<T>(int id)
        {
            var message = await _messageRepository.GetByID(id);
            return _mapper.Map<T>(message);
        }

        public async Task<bool> CheckIfExists(int id)
        {
            return await _messageRepository.CheckIfExists(id);
        }

        public async Task<bool> UpdateMessageAsync(int messageId, EmailViewModel updatedMessageDTO)
        {
            var message = await _messageRepository.GetByIDIncludingAll(messageId);
            if (message == null) return false;

            message.Subject = updatedMessageDTO.Subject;
            message.Body = updatedMessageDTO.Body;
            message.Status = updatedMessageDTO.Status;

            var updatedEmails = updatedMessageDTO.Recipients.Select(r => r.Email).ToHashSet();
            var currentEmails = message.MessageRecipients.Select(r => r.Email).ToHashSet();

            var toRemove = message.MessageRecipients.Where(r => !updatedEmails.Contains(r.Email)).ToList();
            foreach (var r in toRemove)
            {
                message.MessageRecipients.Remove(r);
            }

            foreach (var recipient in updatedEmails.Except(currentEmails))
            {
                message.MessageRecipients.Add(new MessageRecipient
                {
                    Email = recipient,
                    MessageId = message.Id
                });
            }

            _messageRepository.Update(message);
            await _messageRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<EmailViewModel>> GetMessagesForSending(int take)
        {
            var messages = await _messageRepository.GetMessagesForSend(take);

            return _mapper.Map<List<EmailViewModel>>(messages);
        }

        public async Task AddBulkIncomingMessagesAsyncm<T>(T bulkIncomingMessageModel)
        {
            BulkIncomingMessage message = _mapper.Map<BulkIncomingMessage>(bulkIncomingMessageModel);
            await _bulkIncomingMessagesRepository.AddBulkIncomingMessageAsync(message);
        }

        public async Task AddBulkOutgoingMessagesAsyncm<T>(T bulkIncomingMessageModel)
        {
            BulkOutgoingMessage message = _mapper.Map<BulkOutgoingMessage>(bulkIncomingMessageModel);
            await _bulkOutgoingMessagesRepository.AddBulkOutgoingMessageAsync(message);
        }

        public async Task UpdateSentStatusAsync(int messageId)
        {
            var message = await _messageRepository.GetByID(messageId);
            message.Status = EmailProvider.Enums.EmailStatuses.EmailStatusComplete;
            _messageRepository.Update(message);
            await _messageRepository.SaveChangesAsync();
        }

        public async Task<int> GetMessagesCount(EmailDirections direction)
        {
            return await _messageRepository
                .AllAsNoTracking()
                .Where(m => m.Direction == direction)
                .CountAsync();
        }
    }
}
