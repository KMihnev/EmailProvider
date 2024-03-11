using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailProvider.Enums;
using EmailProvider.Validation;

namespace EMailProviderClient.Validation
{
    public class UserValidatorC : UserValidator
    {
        public Dictionary<UserValidationTypes, TextBox> ValidationFieldsC { get; set; }

        public UserValidatorC()
        {
            ValidationFieldsC = new Dictionary<UserValidationTypes, TextBox>();
        }

        public void AddValidationField(UserValidationTypes eValidationType, TextBox textBox)
        {
            ValidationFieldsC.Add(eValidationType, textBox);
        }

        public override bool Validate()
        {
            EmptyFields();
            foreach (var pair in ValidationFieldsC)
            {
                AddValidation(pair.Key, pair.Value.Text);
            }

            return base.Validate();
        }


    }
}
