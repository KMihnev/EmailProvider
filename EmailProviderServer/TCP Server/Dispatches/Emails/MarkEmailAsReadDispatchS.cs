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
    public class MarkEmailAsReadDispatchS : BaseDispatchHandler
    {
        private readonly IUserMessageService _userMessageService;

        //Constructor
        public MarkEmailAsReadDispatchS(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            List<int> messagesToRead = new List<int>();
            try
            {
                InPackage.Deserialize(out messagesToRead);

                if (messagesToRead == null)
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
                await _userMessageService.MarkAsReadAsync(SessionUser.Id, messagesToRead);
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
