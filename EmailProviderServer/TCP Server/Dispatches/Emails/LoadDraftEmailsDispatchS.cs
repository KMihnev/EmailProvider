//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Models;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.DBContext.Services;
using EmailServiceIntermediate.Models.Serializables;
using EmailProvider.Models.Serializables;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoadEmailsDispatchS
    //------------------------------------------------------
    public class LoadDraftEmailsDispatchS : BaseDispatchHandler
    {
        private readonly IUserMessageService _userMessageService;

        //Constructor
        public LoadDraftEmailsDispatchS(IUserMessageService userMessageService)
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
                List<EmailListModel> filteredMessages = new List<EmailListModel>();
                filteredMessages = await _userMessageService.GetDraftMessagesAsync<EmailListModel>(searchData, 0, 10);
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
