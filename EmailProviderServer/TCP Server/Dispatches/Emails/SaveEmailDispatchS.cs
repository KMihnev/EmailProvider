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
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Enums;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class SaveEmailDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;
        public SaveEmailDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
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

            foreach(string email in messageSerializable.ReceiverEmails)
            {
                //TO DO
                //AddEmailValidator.AddValidation(EmailServiceIntermediate.Enums.EmailValidationTypes.ValidationTypeReceiver, email);
            }

            AddEmailValidator.AddValidation(EmailServiceIntermediate.Enums.EmailValidationTypes.ValidationTypeSubject, messageSerializable.Subject);

            if(!AddEmailValidator.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            try
            {
                bool bExists = await _messageService.CheckIfExists(messageSerializable.Id);
                if (bExists)
                {
                    if (!await _messageService.UpdateMessageAsync(messageSerializable.Id, messageSerializable))
                    {
                        errorMessage = LogMessages.UpdateRecordError;
                        return false;
                    }
                }
                else
                {
                    await _messageService.ProcessMessageAsync(messageSerializable);
                }

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
