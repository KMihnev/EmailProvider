//Includes
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Validation;
using EMailProviderClient.Dispatches.Users;

namespace EMailProviderClient.Views.User
{
    //------------------------------------------------------
    //	Register
    //------------------------------------------------------
    public partial class Register : Form
    {
        //Members
        //-------
        private UserValidatorC FieldValidator;

        //Constructor
        //----------
        public Register()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            AddValidation();
        }

        //Event Handlers
        //--------------

        private void Register_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            BTN_REGISTER.Select();
        }

        private void STT_GO_TO_LOGIN_Click(object sender, EventArgs e)
        {
            this.Hide();
            var LogInForm = new LogIn();
            LogInForm.ShowDialog();
            this.Close();
        }

        private async void BTN_REGISTER_Click(object sender, EventArgs e)
        {
            if (!FieldValidator.Validate(true))
                return;

            if (EDC_RE_PASSWORD.Text != EDC_PASSWORD.Text)
            {
                Logger.LogWarning(LogMessages.PasswordMismatch);
                return;
            }

            EmailServiceIntermediate.Models.User user = new EmailServiceIntermediate.Models.User();
            user.Email = EDC_EMAIL.Text;
            user.Password = EDC_PASSWORD.Text;

            if (!await UserDispatchesC.Register(user))
            {
                this.Show();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Hide();
            var SetUpProfile = new SetupProfile();
            SetUpProfile.ShowDialog();
            this.Close();
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Hide();
            var StartUpForm = new StartUp();
            StartUpForm.ShowDialog();
            this.Close();
        }

        //Methods
        //-------

        private void AddValidation()
        {
            FieldValidator = new UserValidatorC();
            FieldValidator.AddValidationField(UserValidationTypes.ValidationTypeEmail, EDC_EMAIL);
            FieldValidator.AddValidationField(UserValidationTypes.ValidationTypePassword, EDC_PASSWORD);
        }


    }
}
