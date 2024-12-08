//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;
using EmailServiceIntermediate.Enums;
using AutoMapper;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProvider.Models.Serializables.Base;
using EmailProvider.Enums;
using EmailProvider.SearchData;
using Microsoft.EntityFrameworkCore;
using EmailServiceIntermediate.Models.Serializables;
using AutoMapper.QueryableExtensions;
using EmailProvider.Models.DBModels;

namespace EmailProviderServer.DBContext.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IInnerMessageRepository _innerMessageRepository;
        private readonly IOutgoingMessageRepository _outgoingMessageRepository;
        private readonly IIncomingMessageRepository _incomingMessageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MessageService(
            IMessageRepository messageRepository,
            IInnerMessageRepository innerMessageRepository,
            IOutgoingMessageRepository outgoingMessageRepository,
            IIncomingMessageRepository incomingMessageRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _innerMessageRepository = innerMessageRepository;
            _outgoingMessageRepository = outgoingMessageRepository;
            _incomingMessageRepository = incomingMessageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ProcessMessageAsync<TMessageDTO>(TMessageDTO messageDTO) where TMessageDTO : BaseMessageSerializable
        {
            var message = _mapper.Map<Message>(messageDTO);

            await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();
            var messageId = message.Id;

            foreach (var receiverEmail in messageDTO.ReceiverEmails)
            {
                var receiver = await _userRepository.GetUserByEmail(receiverEmail);

                if (receiver != null)
                {
                    //Вътрешен
                    var innerMessage = new InnerMessage
                    {
                        MessageId = messageId,
                        SenderId = messageDTO.SenderId,
                        ReceiverId = receiver.Id
                    };

                    await _innerMessageRepository.AddAsync(innerMessage);
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

                    await _outgoingMessageRepository.AddAsync(outgoingMessage);
                }
            }

            await _messageRepository.SaveChangesAsync();
        }

        public async Task<List<ViewMessage>> GetCombinedMessagesAsync(SearchData seacrhData)
        {
            return await _messageRepository.GetCombinedMessagesAsync(seacrhData.UserID, (int)seacrhData.GetSearchTypeFolder(), seacrhData.ConstructWhereClause());
        }
    }

}
