using EmailProvider.Validation.Base;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailProvider.Validation.User
{
    public class UserValidator : BaseUserValidator
    {
        public UserValidator()
        {
            ValidationFields = new Dictionary<UserValidationTypes, string>();
        }

        public UserValidator(Dictionary<UserValidationTypes, string> ValidationFields)
        {
            this.ValidationFields = ValidationFields;
        }

        public void EmptyFields()
        {
            ValidationFields.Clear();
        }

        public override bool Validate(bool bLog = false)
        {
            if (ValidationFields == null || ValidationFields.Count == 0)
            {
                Logger.LogError(LogMessages.NoFieldsToValidate);
                return false;
            }

            foreach (var pair in ValidationFields)
            {
                switch (pair.Key)
                {
                    case UserValidationTypes.ValidationTypeNone:
                        break;
                    case UserValidationTypes.ValidationTypeName:
                        {
                            if (!ValidateName(pair.Value, bLog))
                                return false;
                            break;
                        } //case
                    case UserValidationTypes.ValidationTypePassword:
                        {
                            if (!ValidatePassword(pair.Value, bLog))
                                return false;
                            break;
                        } //case
                    case UserValidationTypes.ValidationTypeEmail:
                        {
                            if (!ValidateEmail(pair.Value, bLog))
                                return false;
                            break;
                        } //case
                    case UserValidationTypes.ValidationTypePhoneNumber:
                        {
                            if (!ValidatePhoneNumber(pair.Value, bLog))
                                return false;
                            break;
                        } //case
                    case UserValidationTypes.ValidationTypeCountry:
                        {
                            if (!ValidateCountry(int.Parse(pair.Value)))
                                return false;
                            break;
                        } //case
                } //switch
            } // foreach
            return true;
        }

        public bool IsEmail(string value)
        {
            if (!ValidateEmail(value))
                return false;

            return true;
        }

        public void AddValidation(UserValidationTypes eValidationType, string value)
        {
            ValidationFields.Add(eValidationType, value);
        }

        protected virtual bool ValidateName(string Name, bool bLog = false)
        {
            if (!BasicValidation.IsAlpha(Name))
            {
                if (bLog)
                    Logger.LogWarning(LogMessages.InvalidName);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidatePassword(string Password, bool bLog = false)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                if(bLog)
                    Logger.LogWarning(LogMessages.RequiredFieldPassword);
                return false;
            } //if

            if (!BasicValidation.IsAlphaNumeric(Password))
            {
                if (bLog)
                    Logger.LogWarning(LogMessages.InvalidPassword);
                return false;
            } //if

            if (!Password.Any(char.IsUpper))
            {
                if (bLog)
                    Logger.LogWarning(LogMessages.InvalidPasswordNoUpper);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidateEmail(string email, bool bLog = false)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                if (bLog)
                    Logger.LogWarning(LogMessages.RequiredFieldEmail);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidatePhoneNumber(string phoneNumber, bool bLog = false)
        {
            string pattern = @"^\+?\d{1,3}? ?\d{6,14}$";
            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                if (bLog)
                    Logger.LogWarning(LogMessages.InvalidPhoneNumber);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidateCountry(int value)
        {
            if (value < 0)
                return false;

            return true;
        }
    }
}
