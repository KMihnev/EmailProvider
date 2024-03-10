using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Logging
{
    public static class LogMessages
    {
        public const string InvalidName = "Name: Only Alphabetic characters are allowed";
        public const string InvalidPassword = "Password: Only Alphabetic characters and numbers are allowed";
        public const string InvalidPasswordNoUpper = "Password: An uppercase letter is required";
        public const string CantReachLogFileLocation = "Cant reach the Log file location!";
        public const string InvalidEmail = "Email: Invalid";
        public const string InvalidPhoneNumber = "Phone Number: Invalid";
        public const string Warning = "Warning";
        public const string Error = "Warning";
        public const string Info = "Info";
        public const string FileLogMessage = "{0}: {1}";
        public const string NoFieldsToValidate = "Validator: No fields to Validate";
        public const string RequiredFieldPassword = "Password: Is Required";
        public const string RequiredFieldEmail = "Email: Is Required";
        public const string PasswordMismatch = "Repeat Password: The password doesn't match";
        public const string UserAlreadyExists = "User with this email already exists";
        public const string UserNotFound = "User with this Id not Found";
        public const string ErrorAddingUser = "Error adding new user";
    }
}
