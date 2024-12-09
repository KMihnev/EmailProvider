//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.SearchData;
using EmailProvider.Models.DBModels;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoadEmailsDispatchS
    //------------------------------------------------------
    public class LoadEmailsDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;

        //Constructor
        public LoadEmailsDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        //Methods
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
