//Includes
using EmailProvider.Validation.Base;
using EmailServiceIntermediate.Enums;

namespace EmailProvider.Validation.Email
{
    //------------------------------------------------------
    //	BaseEmailValidator
    //------------------------------------------------------
    public class BaseEmailValidator : IValidator
    {
        protected Dictionary<EmailValidationTypes, string> ValidationFields { get; set; }

        public virtual bool Validate(bool bLog = false)
        {
            return true;
        }
    }
}
