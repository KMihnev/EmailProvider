using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Validation
{
    public static class BasicValidation
    {
        public static bool IsAlpha(string strValue)
        {
            return strValue.All(char.IsLetter);
        }

        public static bool IsNumeric(string strValue)
        {
            return strValue.All(char.IsAsciiDigit);
        }

        public static bool IsAlphaNumeric(string strValue)
        {
            return strValue.All(char.IsLetterOrDigit);
        }

        public static bool IsText(string strValue)
        {
            return strValue.All(c =>char.IsLetterOrDigit(c) || char.IsPunctuation(c));
        }
    }

    public static class ValidationPatterns
    {
        public const string EmailPattern = @"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$";
        public const string PhoneNumberPattern = @"^\+?\d{1,3}? ?\d{6,14}$";
    }
}
