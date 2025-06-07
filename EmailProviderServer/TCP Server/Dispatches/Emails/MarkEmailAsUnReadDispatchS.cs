//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.SearchData;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.DBContext.Services.Interfaces;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoadEmailsDispatchS
    //------------------------------------------------------
    public class MarkEmailAsUnReadDispatchS : BaseDispatchHandler
    {
        private readonly IUserMessageService _userMessageService;

        //Constructor
        public MarkEmailAsUnReadDispatchS(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            List<int> messagesToUnRead = new List<int>();
            try
            {
                InPackage.Deserialize(out messagesToUnRead);

                if (messagesToUnRead == null)
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
                await _userMessageService.MarkAsUnreadAsync(SessionUser.Id, messagesToUnRead);
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
