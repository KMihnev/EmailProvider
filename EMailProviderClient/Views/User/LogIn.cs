using EmailProvider.Enums;
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

        private void BTN_LOGIN_Click(object sender, EventArgs e)
        {
            if (!FieldValidator.Validate())
                return;
        }

        //Methods
        //-------

        private void AddValidation()
        {
            FieldValidator = new LoginFormValidationC();
            FieldValidator.AddValidationField(UserValidationTypes.ValidationTypeEmail, EDC_NAME);
            FieldValidator.AddValidationField(UserValidationTypes.ValidationTypePassword, EDC_PASSWORD);
        }
    }
}
