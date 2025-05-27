//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.DBContext.Services;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoadEmailsDispatchS
    //------------------------------------------------------
    public class LoadIncomingEmailsDispatchS : BaseDispatchHandler
    {
        private readonly IUserMessageService _userMessageService;

        //Constructor
        public LoadIncomingEmailsDispatchS(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
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
                List<Message> filteredMessages = new List<Message>();
                filteredMessages = await _userMessageService.GetIncomingMessagesAsync<Message>(searchData, 0, 10);
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
