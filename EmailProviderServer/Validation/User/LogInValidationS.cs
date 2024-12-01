using EmailProvider.Validation.User;
using EmailServiceIntermediate.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.Validation.User
{
    public class LogInValidationS : UserValidator
    {
        protected override bool ValidatePassword(string Password, bool bLog = false)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                if(bLog)
                    Logger.LogWarning(LogMessages.RequiredFieldPassword);
                return false;
            } //if
            return true;
        }

        protected override bool ValidateEmail(string email, bool bLog = false)
        {
            if (!base.ValidateEmail(email, bLog) && !!base.ValidateName(email, bLog))
                return false;

            return true;
        }
    }
}
