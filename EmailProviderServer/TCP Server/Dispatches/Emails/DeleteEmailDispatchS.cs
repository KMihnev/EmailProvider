//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.SearchData;
using EmailProviderServer.DBContext.Services;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoadEmailsDispatchS
    //------------------------------------------------------
    public class DeleteEmailDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;

        //Constructor
        public DeleteEmailDispatchS(IMessageService messageService)
        {
            _messageService = messageService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            List<int> messagewsToDelete = new List<int>();
            try
            {
                InPackage.Deserialize(out messagewsToDelete);

                if (messagewsToDelete == null)
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
               // await _messageService.DeleteMessagesAsync(messagewsToDelete);
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
