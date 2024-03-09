using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailProvider.Enums;
using EmailProvider.Validation;

namespace EMailProviderClient.Validation
{
    public class ValidatorC : Validator
    {
        public Dictionary<ValidationTypes, TextBox> ValidationFieldsC { get; set; }

        public ValidatorC()
        {
            ValidationFieldsC = new Dictionary<ValidationTypes, TextBox>();
        }

        public void AddValidationField(ValidationTypes eValidationType, TextBox textBox)
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
