//Includes
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;

namespace EmailProviderServer.TCP_Server.Dispatches.Emails
{
    //------------------------------------------------------
    //	GetEmailDispatchS
    //------------------------------------------------------

    public class GetEmailDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;

        //Constructor
        public GetEmailDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            int nMessageId = 0;
            try
            {
                InPackage.Deserialize(out nMessageId);
    
                if (nMessageId == null)
                {
                    Logger.LogNullValue();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }
    
            try
            {
                MessageSerializable messageSerializable = new MessageSerializable();
                messageSerializable = await _messageService.GetByIDIncludingAll<MessageSerializable>(nMessageId);
                OutPackage.Serialize(true);
                OutPackage.Serialize(messageSerializable);
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
