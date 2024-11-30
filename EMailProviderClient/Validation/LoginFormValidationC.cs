using EmailServiceIntermediate.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Validation
{
    public class LoginFormValidationC : UserValidatorC
    {
        public LoginFormValidationC()
        {
            
        }

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
            if (!base.ValidateEmail(email, false) && !base.ValidateName(email))
                return false;

            return true;
        }
    }
}
