using EmailProvider.Dispatches;
using EmailProvider.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class SaveEmailDispatchS : BaseDispatchHandler
    {
        private readonly MessageService _messageService;
        private readonly UserService _userService;
        public SaveEmailDispatchS(MessageService messageService, UserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            MessageSerializable messageSerializable;
            try
            {
                InPackage.Deserialize(out messageSerializable);

                if (messageSerializable == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            UserSerializable Receiver = _userService.GetByEmail<UserSerializable>(messageSerializable.ReceiverEmail);
            if(Receiver == null)
            {
                errorMessage = LogMessages.InteralError;
                return false;
            }

            messageSerializable.ReceiverId = Receiver.Id;

            try
            {
                _messageService.CreateAsync<MessageSerializable>(messageSerializable);
                OutPackage.Serialize(true);
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            return true;
        }
    }
}
