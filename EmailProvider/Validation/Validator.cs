using EmailProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Validation
{
    public class Validator
    {
        public Dictionary<ValidationTypes, string> ValidationFields { get; set; }

        public Validator(Dictionary<ValidationTypes, string> ValidationFields)
        {
            this.ValidationFields = ValidationFields;
        }

        public bool ValidateName(string Name)
        {
            return BasicValidation.IsAlpha(Name);
        }

        public bool Password(string Password)
        {
            bool bIsValid = BasicValidation.IsAlphaNumeric(Password);
            if (!bIsValid)
                return false;

            bIsValid = Password.Any(char.IsUpper);
            if (!bIsValid)
                return false;

            return bIsValid;
        }
    }
}
