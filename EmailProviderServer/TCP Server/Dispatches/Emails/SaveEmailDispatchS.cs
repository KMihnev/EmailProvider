//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation.Email;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.Models.Serializables;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	SaveEmailDispatchS
    //------------------------------------------------------
    public class SaveEmailDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;

        //Constructor
        public SaveEmailDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        //Methods
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

            foreach(MessageRecipientSerializable email in messageSerializable.Recipients)
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
