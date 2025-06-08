using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.PrivateService
{
    //------------------------------------------------------
    //	SaveEmailDispatchS
    //------------------------------------------------------
    public class SendEmailDispatchService : BaseDispatchHandler
    {
        //Constructor
        public SendEmailDispatchService()
        {

        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            EmailViewModel messageSerializable;
            try
            {
                InPackage.Deserialize(out messageSerializable);

                if (messageSerializable == null)
                {
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
