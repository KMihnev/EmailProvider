using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.ServiceDispatches;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;

namespace EmailProviderServer.TCP_Server.ServiceDispatches
{
    public class SendEmailServiceDispatchS
    {
        private readonly IMessageService _messageService;

        public SendEmailServiceDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<bool> SendEmail(EmailViewModel messageSerializable)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.SendEmailToService);
                InPackage.Serialize(messageSerializable);

                //Изпращаме заявката
                DispatchHandlerServiceS dispatchHandlerServiceS = new DispatchHandlerServiceS();

                if (!await dispatchHandlerServiceS.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCallingSilent();
                    return false;
                }

                BulkOutgoingMessageSerializable bulkOutgoingMessage = null;
                OutPackage.Deserialize(out bulkOutgoingMessage);
                if (bulkOutgoingMessage != null)
                {
                    await _messageService.AddBulkOutgoingMessagesAsyncm<BulkOutgoingMessageSerializable>(bulkOutgoingMessage);
                    await _messageService.UpdateSentStatusAsync(messageSerializable.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogErrorCallingSilent();
                return false;
            }
        }
    }
}
