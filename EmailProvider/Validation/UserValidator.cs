using EmailProvider.Enums;
using EmailProvider.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailProvider.Validation
{
    public class UserValidator : BaseValidator
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
            this.ValidationFields.Clear();
        }

        public override bool Validate()
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
                            if (!ValidateName(pair.Value))
                                return false;
                            break;
                        } //case
                    case UserValidationTypes.ValidationTypePassword:
                        {
                            if (!ValidatePassword(pair.Value))
                                return false;
                            break;
                        } //case
                    case UserValidationTypes.ValidationTypeEmail:
                        {
                            if (!ValidateEmail(pair.Value))
                                return false;
                            break;
                        } //case
                    case UserValidationTypes.ValidationTypePhoneNumber:
                        {
                            if (!ValidatePhoneNumber(pair.Value))
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

        public void AddValidation(UserValidationTypes eValidationType, string value)
        {
            ValidationFields.Add(eValidationType, value);
        }

        protected virtual bool ValidateName(string Name)
        {
            if (!BasicValidation.IsAlpha(Name))
            {
                Logger.LogWarning(LogMessages.InvalidName);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidatePassword(string Password)
        {
            if(string.IsNullOrWhiteSpace(Password))
            {
                Logger.LogWarning(LogMessages.RequiredFieldPassword);
                return false;
            } //if

            if (!BasicValidation.IsAlphaNumeric(Password))
            {
                Logger.LogWarning(LogMessages.InvalidPassword);
                return false;
            } //if

            if (!Password.Any(char.IsUpper))
            {
                Logger.LogWarning(LogMessages.InvalidPasswordNoUpper);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Logger.LogWarning(LogMessages.RequiredFieldEmail);
                return false;
            } //if

            if (!Regex.IsMatch(email, ValidationPatterns.EmailPattern))
            {
                Logger.LogWarning(LogMessages.InvalidEmail);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+?\d{1,3}? ?\d{6,14}$";
            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                Logger.LogWarning(LogMessages.InvalidPhoneNumber);
                return false;
            } //if

            return true;
        }

        protected virtual bool ValidateCountry(int value)
        {
            if(value < 0)
                return false;

            return true;
        }
    }
}
