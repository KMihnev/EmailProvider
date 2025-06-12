//Includes
using EmailProvider.Validation.User;
using EmailServiceIntermediate.Enums;

namespace EMailProviderClient.Validation
{
    //------------------------------------------------------
    //	UserValidatorC
    //------------------------------------------------------
    public class UserValidatorC : UserValidator
    {
        public Dictionary<UserValidationTypes, TextBox> ValidationFieldsC { get; set; }


        //Methods
        public UserValidatorC()
        {
            ValidationFieldsC = new Dictionary<UserValidationTypes, TextBox>();
        }

        //Methods
        public void AddValidationField(UserValidationTypes eValidationType, TextBox textBox)
        {
            ValidationFieldsC[eValidationType] =textBox;
        }

        public override bool Validate(bool bLog = false)
        {
            EmptyFields();
            foreach (var pair in ValidationFieldsC)
            {
                AddValidation(pair.Key, pair.Value.Text);
            }

            return base.Validate(bLog);
        }


    }
}
