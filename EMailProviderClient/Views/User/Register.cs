using EmailProvider.Enums;
using EmailProvider.Logging;
using EMailProviderClient.Dispatches;
using EMailProviderClient.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using EmailServiceIntermediate.Models;

namespace EMailProviderClient.Views.User
{
    public partial class Register : Form
    {
        //Members
        //-------
        private ValidatorC FieldValidator;


        //Constructor
        //----------
        public Register()
        {
            InitializeComponent();
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
            if (!FieldValidator.Validate())
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
                return;

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
            FieldValidator = new ValidatorC();
            FieldValidator.AddValidationField(ValidationTypes.ValidationTypeEmail, EDC_EMAIL);
            FieldValidator.AddValidationField(ValidationTypes.ValidationTypePassword, EDC_PASSWORD);
        }


    }
}
