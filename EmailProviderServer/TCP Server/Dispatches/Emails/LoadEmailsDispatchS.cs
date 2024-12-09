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
using EmailProvider.SearchData;
using EmailProvider.Models.DBModels;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class LoadEmailsDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;
        public LoadEmailsDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            SearchData searchData = new SearchData();
            try
            {
                InPackage.Deserialize(out searchData);

                if (searchData == null)
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
                List<ViewMessage> filteredMessages = new List<ViewMessage> ();
                filteredMessages = await _messageService.GetCombinedMessagesAsync(searchData);
                OutPackage.Serialize(true);
                OutPackage.Serialize(filteredMessages);
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
