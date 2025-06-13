using EmailProvider.Validation.User;
using EmailServiceIntermediate.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmailTests.Validation
{
    public class UserValidatorTests
    {
        [Fact]
        public void Validate_ShouldReturnFalse_WhenRequiredFieldsAreMissing()
        {
            var fields = new Dictionary<UserValidationTypes, string>
            {
                { UserValidationTypes.ValidationTypeEmail, "" },
                { UserValidationTypes.ValidationTypeName, "" },
                { UserValidationTypes.ValidationTypePassword, "" }
            };
            var validator = new UserValidator(fields);
            bool result = validator.Validate();
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenFieldsAreValid()
        {
            var fields = new Dictionary<UserValidationTypes, string>
            {
                { UserValidationTypes.ValidationTypeEmail, $"ValidUser@{EmailServiceIntermediate.Settings.SettingsProvider.GetEmailDomain()}" },
                { UserValidationTypes.ValidationTypeName, "ValidUser" },
                { UserValidationTypes.ValidationTypePassword, "StrongPassword123" }
            };
            var validator = new UserValidator(fields);
            bool result = validator.Validate();
            Assert.True(result);
        }

        [Fact]
        public void EmptyFields_NoFieldsIsTrue()
        {
            var fields = new Dictionary<UserValidationTypes, string>
            {
                { UserValidationTypes.ValidationTypeName, "User" }
            };
            var validator = new UserValidator(fields);
            validator.EmptyFields();
            bool result = validator.Validate();

            Assert.True(result);
        }
    }
}
