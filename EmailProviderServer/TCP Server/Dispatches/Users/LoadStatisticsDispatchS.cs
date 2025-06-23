//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation.Email;
using EmailProviderServer.DBContext.Services.Base;
using EmailProvider.Models.Serializables;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoadStatisticsDispatchS
    //------------------------------------------------------
    public class LoadStatisticsDispatchS : BaseDispatchHandler
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        //Constructor
        public LoadStatisticsDispatchS(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService; 
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            StatisticsViewModel statisticsModel = new StatisticsViewModel(); ;

            try
            {
                statisticsModel.numberOfOutgoingEmails = await _messageService.GetMessagesCount(EmailProvider.Enums.EmailDirections.EmailDirectionOut);
                statisticsModel.numberOfOutgoingEmails = await _messageService.GetMessagesCount(EmailProvider.Enums.EmailDirections.EmailDirectionIn);
                statisticsModel.numberOfUsers = await _userService.GetUserCount();

                OutPackage.Serialize(true);
                OutPackage.Serialize(statisticsModel);
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
