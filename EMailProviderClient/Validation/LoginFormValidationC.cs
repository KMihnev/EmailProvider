//Includes
using EmailServiceIntermediate.Logging;

namespace EMailProviderClient.Validation
{
    //------------------------------------------------------
    //	LoginFormValidationC
    //------------------------------------------------------
    public class LoginFormValidationC : UserValidatorC
    {
        //Constructor
        public LoginFormValidationC()
        {
            
        }

        //Methods
        protected override bool ValidatePassword(string Password, bool bLog = false)
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
