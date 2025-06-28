//Includes

namespace EmailServiceIntermediate.Logging
{
    //------------------------------------------------------
    //	LogMessages
    //------------------------------------------------------
    public static class LogMessages
    {
        public static string ReplaceTokens(this string template, params (string key, string value)[] tokens)
        {
            if (string.IsNullOrEmpty(template) || tokens == null)
                return template;

            foreach (var (key, value) in tokens)
            {
                var placeholder = $"{{{key}}}";
                template = template.Replace(placeholder, value ?? string.Empty);
            }

            return template;
        }

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
        public const string ErrorCalling = "Error calling: {0}";
        public const string NullValue = "Null value in: {0}";
        public const string NoUserForEmail = "No user with this email was found";
        public const string NoUserForName = "No user with this name was found";
        public const string IncorrectPassword = "Incorrect Password";
        public const string ExitSureCheck = "Are you sure you want to close the applciation?";
        public const string ExitConfirmation = "Do you wish to leave?";
        public const string ErrorSavingEmail = "There was an error saving the email";
        public const string DoYouWishToSaveDraft = "Do you want to save this message as a draft?";
        public const string SaveAsDraft = "Save as Draft";
        public const string RecordNotFound = "Record not found: {0}, Searched value:{1}";
        public const string UpdateRecordError = "Updating record failed";
        public const string SaveChanges = "Do you want to save the changes made to this message?";
        public const string DisconnectedBeforeReadingLenght = "Disconnected before reading the length prefix.";
        public const string InvalidMessageLenght = "Invalid message length";
        public const string DisconnectedBeforeReadingFullPayload = "Disconnected before reading the full payload.";
        public const string InvalidEmailFormat = "Invalid email format: {email}";
        public const string EmailDomainMustBe = "Email domain must be @'{domain}' : {email}";
    }
}
