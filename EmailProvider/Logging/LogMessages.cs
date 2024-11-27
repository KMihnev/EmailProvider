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
        public const string DispatchErrorRegister = "Error calling register dispatch";
        public const string InvalidUserDetails = "Invalid user details";
        public const string DispatchErrorSetUpProfile = "Setting up Profile failed!";
        public const string DispatchErrorUserNotFound = "User not found!";
        public const string BtnTextSkip = "Skip";
        public const string BtnTextCancel = "Cancel";
        public const string DispatchErrorGetCountries = "Error calling GetCountries dispatch";
        public const string NoCountriesLoaded = "No countries have been loaded";
        public const string NoCountrySelected = "No countries has been selected";
        public const string InvalidData = "InvalidData";
        public const string InteralError = "Internal Error";
        public const string NullObject = "{0} is null";
        public const string ErrorCalling = "Error calling: ";
    }
}
