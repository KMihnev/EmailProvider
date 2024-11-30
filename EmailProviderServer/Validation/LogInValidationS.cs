using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.Validation
{
    public class LogInValidationS : UserValidator
    {
        protected override bool ValidatePassword(string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                Logger.LogWarning(LogMessages.RequiredFieldPassword);
                return false;
            } //if
            return true;
        }

        protected override bool ValidateEmail(string email, bool bLog = false)
        {
            if (!base.ValidateEmail(email, bLog) && !!base.ValidateName(email))
                return false;

            return true;
        }
    }
}
