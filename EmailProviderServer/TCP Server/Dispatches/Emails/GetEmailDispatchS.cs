using EmailProvider.Models.DBModels;
using EmailProvider.SearchData;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches.Emails
{
    public class GetEmailDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;
        public GetEmailDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }
    
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
