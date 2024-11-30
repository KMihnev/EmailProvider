using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using EmailServiceIntermediate.Models;
using EmailProviderServer.Validation.User;
using EmailProviderServer.Validation.Email;
using EmailProvider.Enums;

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

            AddEmailValidationS AddEmailValidator = new AddEmailValidationS();
            AddEmailValidator.AddValidation(EmailServiceIntermediate.Enums.EmailValidationTypes.ValidationTypeReceiver, messageSerializable.ReceiverEmail);
            AddEmailValidator.AddValidation(EmailServiceIntermediate.Enums.EmailValidationTypes.ValidationTypeSubject, messageSerializable.Subject);

            if(AddEmailValidator.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            UserSerializable Receiver = _userService.GetByEmail<UserSerializable>(messageSerializable.ReceiverEmail);

            //ако не сме го открили при нас значи не е вътрешен
            if (Receiver == null)
                messageSerializable.Direction = EmailDirectionProvider.GetEmailDirectionOut();
            else
            {
                messageSerializable.ReceiverId = Receiver.Id;
                messageSerializable.Direction = EmailDirectionProvider.GetEmailDirectionInner();
            }

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
