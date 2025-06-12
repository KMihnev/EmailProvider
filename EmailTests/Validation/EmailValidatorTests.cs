using Xunit;
using EmailProvider.Validation.Email;
using EmailServiceIntermediate.Enums;
using System.Collections.Generic;

namespace EmailTests.Validation
{
    public class EmailValidatorTests
    {
        [Fact]
        public void Validate_ShouldReturnFalse_WhenFieldsAreMissing()
        {
            var fields = new Dictionary<EmailValidationTypes, string>
            {
                { EmailValidationTypes.ValidationTypeSubject, "" },
                { EmailValidationTypes.ValidationTypeReceiver, "" }
            };
            var validator = new EmailValidator(fields);
            bool result = validator.Validate(false);
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenFieldsAreValid()
        {
            var fields = new Dictionary<EmailValidationTypes, string>
            {
                { EmailValidationTypes.ValidationTypeSubject, "Test Subject" },
                { EmailValidationTypes.ValidationTypeReceiver, "user@example.com" },
            };
            var validator = new EmailValidator(fields);
            bool result = validator.Validate(false);
            Assert.True(result);
        }

        [Fact]
        public void EmptyFields_ShouldClearAllEntries()
        {
            var fields = new Dictionary<EmailValidationTypes, string>
            {
                { EmailValidationTypes.ValidationTypeSubject, "Some value" }
            };
            var validator = new EmailValidator(fields);
            validator.EmptyFields();
            bool result = validator.Validate(false);
            Assert.False(result);
        }
    }
}
