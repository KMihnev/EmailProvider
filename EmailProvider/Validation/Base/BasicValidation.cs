//Includes

namespace EmailProvider.Validation.Base
{
    //------------------------------------------------------
    //	BasicValidation
    //------------------------------------------------------

    /// <summary> Базов клас за валидации </summary>
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
            return strValue.All(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c));
        }
    }

    //------------------------------------------------------
    //	ValidationPatterns
    //------------------------------------------------------
    public static class ValidationPatterns
    {
        public const string EmailPattern = @"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$";
        public const string PhoneNumberPattern = @"^\+?\d{1,3}? ?\d{6,14}$";
    }
}
