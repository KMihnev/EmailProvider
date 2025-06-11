using EmailProvider.Models.Serializables;
using EMailProviderClient.Validation;
using EmailServiceIntermediate.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMailProviderClient.Views.User
{
    public partial class ChangePassword : Form
    {

        private ChangePasswordModel? _passwordModel;

        public ChangePassword(ChangePasswordModel model)
        {
            _passwordModel = model;
            InitializeComponent();

            if(_passwordModel != null)
            {
                OLD_PASSWORD_EDIT.Text = _passwordModel.OldPassword;
                NEW_PASSWORD_EDIT.Text = _passwordModel.NewPassword;
                REPEAT_PASSWORD_EDIT.Text = _passwordModel.ConfirmPassword;
            }
        }

        private void CLOSE_BTN_Click(object sender, EventArgs e)
        {
            _passwordModel = null;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        public ChangePasswordModel? GetPasswordModel() => _passwordModel;

        private void button1_Click(object sender, EventArgs e)
        {
            _passwordModel = new ChangePasswordModel
            {
                OldPassword = OLD_PASSWORD_EDIT.Text.Trim(),
                NewPassword = NEW_PASSWORD_EDIT.Text.Trim(),
                ConfirmPassword = REPEAT_PASSWORD_EDIT.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(_passwordModel.OldPassword) &&
                string.IsNullOrWhiteSpace(_passwordModel.NewPassword) &&
                string.IsNullOrWhiteSpace(_passwordModel.ConfirmPassword))
            {
                _passwordModel = null;
            }
            else
            {
                UserValidatorC UserValidator = new UserValidatorC();
                UserValidator.AddValidationField(UserValidationTypes.ValidationTypePassword, NEW_PASSWORD_EDIT);
                if (!UserValidator.Validate(true))
                    return;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
