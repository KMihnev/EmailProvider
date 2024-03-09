using EmailProvider.Enums;
using EmailProvider.Logging;
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

namespace EMailProviderClient.Views.User
{
    public partial class Register : Form
    {
        private ValidatorC FieldValidator;
        public Register()
        {
            InitializeComponent();

            FieldValidator = new ValidatorC();
            FieldValidator.AddValidationField(ValidationTypes.ValidationTypeEmail, EDC_EMAIL);
            FieldValidator.AddValidationField(ValidationTypes.ValidationTypePassword, EDC_PASSWORD);
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            BTN_REGISTER.Select();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var LogInForm = new LogIn();
            LogInForm.ShowDialog();
            this.Close();
        }

        private void BTN_REGISTER_Click(object sender, EventArgs e)
        {
            if (!FieldValidator.Validate())
                return;

            if (EDC_RE_PASSWORD.Text != EDC_PASSWORD.Text)
            {
                Logger.LogWarning(LogMessages.PasswordMismatch);
                return;
            }

            this.Hide();
            var SetUpProfile = new SetupProfile();
            SetUpProfile.ShowDialog();
            this.Close();
        }
    }
}
