//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Enums;
using AutoMapper;
using System.Diagnostics.Metrics;
using EmailProviderServer.DBContext.Repositories;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProvider.Models.Serializables.Base;

namespace EmailProviderServer.DBContext.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IInnerMessageRepository _innerMessageRepository;
        private readonly IOutgoingMessageRepository _outgoingMessageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MessageService(
            IMessageRepository messageRepository,
            IInnerMessageRepository innerMessageRepository,
            IOutgoingMessageRepository outgoingMessageRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _innerMessageRepository = innerMessageRepository;
            _outgoingMessageRepository = outgoingMessageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllByStatus<T>(int nStatus, int? nCount = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllDrafts<T>(int? nCount = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetByDateOfSend<T>(DateTime dDateOfSend, DateSearchType eDateSearchType, int? nCount = null)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int nId)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessMessageAsync<TMessageDTO>(TMessageDTO messageDTO) where TMessageDTO : SendMessageDTOBase
        {
            var message = _mapper.Map<Message>(messageDTO);

            var messageId = await _messageRepository.AddMessageAsync(message);

            foreach (var receiverEmail in messageDTO.ReceiverEmails)
            {
                // Дали е вътрешен потребител
                var receiver = await _userRepository.GetUserByEmailAsync(receiverEmail);

                if (receiver != null)
                {
                    //Вътрешен
                    var innerMessage = new InnerMessage
                    {
                        MessageId = messageId,
                        SenderId = messageDTO.SenderId,
                        ReceiverId = receiver.Id
                    };

                    await _innerMessageRepository.AddInnerMessageAsync(innerMessage);
                }
                else
                {
                    //Външен
                    var outgoingMessage = new OutgoingMessage
                    {
                        MessageId = messageId,
                        SenderId = messageDTO.SenderId,
                        ReceiverEmail = receiverEmail
                    };

                    await _outgoingMessageRepository.AddOutgoingMessageAsync(outgoingMessage);
                }
            }
        }
    }

}
