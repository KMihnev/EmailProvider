//Includes
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;
using AutoMapper;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models.Serializables;
using EmailProvider.Models.DBModels;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	MessageService
    //------------------------------------------------------
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IInnerMessageRepository _innerMessageRepository;
        private readonly IOutgoingMessageRepository _outgoingMessageRepository;
        private readonly IIncomingMessageRepository _incomingMessageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //Constructor
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

        //Methods
        public async Task ProcessMessageAsync(MessageSerializable messageDTO)
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

        public async Task<T> GetByIDIncludingAll<T>(int id)
        {
            var message = await _messageRepository.GetByIDIncludingAll(id);
            return _mapper.Map<T>(message);
        }

        public async Task<bool> CheckIfExists(int id)
        {
            return await _messageRepository.CheckIfExists(id);
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var message = await _messageRepository.GetByID(id);
            return _mapper.Map<T>(message);
        }

        public async Task<bool> UpdateMessageAsync(int messageId, MessageSerializable updatedMessageDTO)
        {
            var existingMessage = await _messageRepository.GetByIDIncludingAll(messageId);

            if (existingMessage == null)
                return false;
            
            //обновяваме данни за самият имейл
            existingMessage.Subject = updatedMessageDTO.Subject;
            existingMessage.Content = updatedMessageDTO.Content;
            existingMessage.Status = updatedMessageDTO.Status;

            //зареждаме всички имейли до които ще изпращаме
            var existingReceiverEmails = new HashSet<string>();
            existingReceiverEmails.UnionWith(existingMessage.InnerMessages.Select(im => im.Receiver.Email));
            existingReceiverEmails.UnionWith(existingMessage.OutgoingMessages.Select(om => om.ReceiverEmail));

            var newReceiverEmails = new HashSet<string>(updatedMessageDTO.ReceiverEmails);

            //Махаме липсващи вътрешни
            var innerMessagesToRemove = existingMessage.InnerMessages
                .Where(im => !newReceiverEmails.Contains(im.Receiver.Email))
                .ToList();

            foreach (var im in innerMessagesToRemove)
            {
                _innerMessageRepository.Delete(im);
            }

            //Махаме липсващи външни
            var outgoingMessagesToRemove = existingMessage.OutgoingMessages
                .Where(om => !newReceiverEmails.Contains(om.ReceiverEmail))
                .ToList();
            foreach (var om in outgoingMessagesToRemove)
            {
                _outgoingMessageRepository.Delete(om);
            }

            //добавяме нови
            foreach (var receiverEmail in newReceiverEmails)
            {
                if (!existingReceiverEmails.Contains(receiverEmail))
                {
                    var receiver = await _userRepository.GetUserByEmail(receiverEmail);
                    if (receiver != null)
                    {
                        var newInnerMessage = new InnerMessage
                        {
                            MessageId = existingMessage.Id,
                            SenderId = updatedMessageDTO.SenderId,
                            ReceiverId = receiver.Id
                        };
                        await _innerMessageRepository.AddAsync(newInnerMessage);
                    }
                    else
                    {
                        var newOutgoingMessage = new OutgoingMessage
                        {
                            MessageId = existingMessage.Id,
                            SenderId = updatedMessageDTO.SenderId,
                            ReceiverEmail = receiverEmail
                        };
                        await _outgoingMessageRepository.AddAsync(newOutgoingMessage);
                    }
                }
            }

            _messageRepository.Update(existingMessage);
            await _messageRepository.SaveChangesAsync();

            return true;
        }
    }

}
