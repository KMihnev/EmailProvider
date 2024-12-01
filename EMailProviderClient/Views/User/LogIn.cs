using EmailServiceIntermediate.Enums;
using EMailProviderClient.Dispatches.Users;
using EMailProviderClient.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMailProviderClient.Views.User
{
    public partial class LogIn : Form
    {
        //Members
        //-------

        private LoginFormValidationC FieldValidator;

        //Constructor
        //----------

        public LogIn()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            AddValidation();
        }

        //Event Handlers
        //--------------

        private void LogIn_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            BTN_LOGIN.Select();
        }

        private void STT_SUGGESTION_Click(object sender, EventArgs e)
        {
            this.Hide();
            var RegisterForm = new Register();
            RegisterForm.ShowDialog();
            this.Close();
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Hide();
            var StartUpForm = new StartUp();
            StartUpForm.ShowDialog();
            this.Close();
        }

        private async void BTN_LOGIN_Click(object sender, EventArgs e)
        {
            EmailServiceIntermediate.Models.User user = new EmailServiceIntermediate.Models.User();

            if (FieldValidator.IsEmail(EDC_NAME.Text))
                user.Email = EDC_NAME.Text;
            else
                user.Name = EDC_NAME.Text;

            if (!FieldValidator.Validate(true))
                return;

            user.Password = EDC_PASSWORD.Text;

            if (!await UserDispatchesC.LogIn(user))
            {
                this.Show();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //Methods
        //-------

        private void AddValidation()
        {
            FieldValidator = new LoginFormValidationC();

            FieldValidator.AddValidationField(UserValidationTypes.ValidationTypePassword, EDC_PASSWORD);
            FieldValidator.AddValidationField(UserValidationTypes.ValidationTypeEmail, EDC_NAME);
        }
    }
}
