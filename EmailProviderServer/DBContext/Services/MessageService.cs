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

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	MessageService
    //------------------------------------------------------
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //Constructor
        public MessageService(
            IMessageRepository messageRepository,
            IFileRepository fileRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Create and save a new message
        public async Task ProcessMessageAsync(EmailViewModel dto)
        {
            var message = new Message
            {
                FromEmail = dto.FromEmail,
                Subject = dto.Subject,
                Body = dto.Body,
                DateOfRegistration = DateTime.UtcNow,
                Direction = dto.Direction,
                Status = dto.Status,
            };

            // Add recipients
            foreach (var recipientDto in dto.Recipients)
            {
                message.MessageRecipients.Add(new MessageRecipient
                {
                    Email = recipientDto.Email
                });
            }

            // Add user messages (sender)
            var sender = await _userRepository.GetUserByEmail(dto.FromEmail);
            message.UserMessages.Add(new UserMessage
            {
                UserId = sender.Id,
                IsRead = true,
                IsDeleted = false,
            });

            // Add user messages (internal recipients)
            foreach (var recipientDto in dto.Recipients)
            {
                var internalUser = await _userRepository.GetUserByEmail(recipientDto.Email);
                if (internalUser != null)
                {
                    message.UserMessages.Add(new UserMessage
                    {
                        UserId = internalUser.Id,
                        IsRead = false,
                        IsDeleted = false,
                    });
                }
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

            await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIDIncludingAll<T>(int id)
        {
            var message = await _messageRepository.GetByIDIncludingAll(id);
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

            // Remove recipients no longer present
            var toRemove = message.MessageRecipients.Where(r => !updatedEmails.Contains(r.Email)).ToList();
            foreach (var r in toRemove)
            {
                message.MessageRecipients.Remove(r);
            }

            // Add new recipients
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
    }
}
