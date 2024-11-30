using EmailProvider.Validation.Base;
using EmailServiceIntermediate.Enums;

namespace EmailProvider.Validation.Email
{
    public class BaseEmailValidator : IValidator
    {
        protected Dictionary<EmailValidationTypes, string> ValidationFields { get; set; }

        public virtual bool Validate()
        {
            return true;
        }
    }
}
