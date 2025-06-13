using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using System.Threading.Tasks;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models.Serializables;

namespace EmailProviderServer.TCP_Server.Dispatches.Emails
{
    public class ReceiveEmailDispatchS : BaseDispatchHandler
    {
        protected override bool RequiresSession => false;

        private readonly IMessageService _messageService;

        public ReceiveEmailDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                BulkIncomingMessageSerializable rawMessage;
                EmailViewModel message;
                InPackage.Deserialize(out message);
                InPackage.Deserialize(out rawMessage);

                if (message == null || string.IsNullOrWhiteSpace(message.Subject))
                {
                    errorMessage = LogMessages.InvalidData;
                    return false;
                }

                await _messageService.ProcessMessageAsync(message);

                if (rawMessage != null && message?.Id != 0)
                {
                    rawMessage.IncomingMessageId = message.Id;
                    await _messageService.AddBulkIncomingMessagesAsyncm(rawMessage);
                }

                OutPackage.Serialize(true);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("ReceiveEmailDispatchS failed: " + ex.Message);
                return false;
            }
        }
    }
}
