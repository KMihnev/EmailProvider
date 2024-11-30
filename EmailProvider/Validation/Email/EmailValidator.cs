using EmailProvider.Validation.Base;
using EmailProvider.Validation.User;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailProvider.Validation.Email
{
    public class EmailValidator : BaseEmailValidator
    {
        public EmailValidator()
        {
            ValidationFields = new Dictionary<EmailValidationTypes, string>();
        }

        public EmailValidator(Dictionary<EmailValidationTypes, string> ValidationFields)
        {
            this.ValidationFields = ValidationFields;
        }

        public void EmptyFields()
        {
            ValidationFields.Clear();
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
                    case EmailValidationTypes.ValidationTypeNone:
                        break;
                    case EmailValidationTypes.ValidationTypeReceiver:
                    case EmailValidationTypes.ValidationTypeSender:
                    {
                        if (!ValidateEmail(pair.Value))
                            return false;
                        break;
                    } //case
                    case EmailValidationTypes.ValidationTypeSubject:
                    {
                        if (pair.Value.Count() < 0)
                              return false;
                        break;
                    } //case
                } //switch
            } // foreach
            return true;
        }
        public void AddValidation(EmailValidationTypes eValidationType, string value)
        {
            ValidationFields.Add(eValidationType, value);
        }

        protected virtual bool ValidateEmail(string email, bool bLog = false)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Logger.LogWarning(LogMessages.RequiredFieldEmail);
                return false;
            } //if

            if (!Regex.IsMatch(email, ValidationPatterns.EmailPattern))
            {
                if (bLog)
                    Logger.LogWarning(LogMessages.InvalidEmail);
                return false;
            } //if

            return true;
        }
    }
}
