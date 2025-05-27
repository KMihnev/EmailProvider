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
        private readonly IMessageService _messageService;

        public ReceiveEmailDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                MessageSerializable message;
                InPackage.Deserialize(out message);

                if (message == null || string.IsNullOrWhiteSpace(message.Subject))
                {
                    errorMessage = LogMessages.InvalidData;
                    return false;
                }

                await _messageService.ProcessMessageAsync(message);

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
