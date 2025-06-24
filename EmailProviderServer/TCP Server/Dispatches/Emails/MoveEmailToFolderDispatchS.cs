using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches.Emails
{
    public class MoveEmailToFolderDispatchS : BaseDispatchHandler
    {
        private readonly IUserMessageService _userMessageService;

        //Constructor
        public MoveEmailToFolderDispatchS(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            List<int> messagesToMove = new List<int>();
            int FolderId = 0;
            try
            {
                InPackage.Deserialize(out messagesToMove);
                InPackage.Deserialize(out FolderId);

                if (messagesToMove == null)
                {
                    Logger.LogNullValue();
                    return false;
                }

                if(FolderId <= 0)
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
                await _userMessageService.MoveMessagesToFolderAsync(messagesToMove, FolderId, SessionUser.Id);
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
