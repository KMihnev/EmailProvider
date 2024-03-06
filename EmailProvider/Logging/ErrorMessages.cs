using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Logging
{
    public static class ErrorMessages
    {
        public const string InvalidName = "Name: Only Alphabetic characters are allowed";
        public const string InvalidPassword = "Password: Only Alphabetic characters and numbers are allowed";
        public const string InvalidPasswordNoUpper = "Password: An uppercase letter is required";
    }
}
