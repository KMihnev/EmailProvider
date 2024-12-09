//Includes
using EmailProvider.Validation.User;
using EmailServiceIntermediate.Logging;

namespace EmailProviderServer.Validation.User
{
    //------------------------------------------------------
    //	LogInValidationS
    //------------------------------------------------------
    public class LogInValidationS : UserValidator
    {
        //Constructor
        public LogInValidationS() : base()
        {
            
        }

        //Methods
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
