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
    public class Validator
    {
        public Dictionary<ValidationTypes, string> ValidationFields { get; set; }

        public Validator()
        {
            ValidationFields = new Dictionary<ValidationTypes, string>();
        }

        public Validator(Dictionary<ValidationTypes, string> ValidationFields)
        {
            this.ValidationFields = ValidationFields;
        }

        public void EmptyFields()
        {
            this.ValidationFields.Clear();
        }

        public virtual bool Validate()
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
                    case ValidationTypes.ValidationTypeNone:
                        break;
                    case ValidationTypes.ValidationTypeName:
                        {
                            if (!ValidateName(pair.Value))
                                return false;
                            break;
                        } //if
                    case ValidationTypes.ValidationTypePassword:
                        {
                            if (!ValidatePassword(pair.Value))
                                return false;
                            break;
                        } //if
                    case ValidationTypes.ValidationTypeEmail:
                        {
                            if (!ValidateEmail(pair.Value))
                                return false;
                            break;
                        } //if
                    case ValidationTypes.ValidationTypePhoneNumber:
                        {
                            if (!ValidatePhoneNumber(pair.Value))
                                return false;
                            break;
                        } //if
                } //switch
            } // foreach
            return true;
        }

        public void AddValidation(ValidationTypes eValidationType, string value)
        {
            ValidationFields.Add(eValidationType, value);
        }

        private bool ValidateName(string Name)
        {
            if (!BasicValidation.IsAlpha(Name))
            {
                Logger.LogWarning(LogMessages.InvalidName);
                return false;
            } //if

            return true;
        }

        private bool ValidatePassword(string Password)
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

        private bool ValidateEmail(string email)
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

        private bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+?\d{1,3}? ?\d{6,14}$";
            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                Logger.LogWarning(LogMessages.InvalidPhoneNumber);
                return false;
            } //if

            return true;
        }
    }
}
