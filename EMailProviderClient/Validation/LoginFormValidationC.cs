using EmailProvider.Logging;
using EmailProvider.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Validation
{
    public class LoginFormValidationC : ValidatorC
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
    }
}
