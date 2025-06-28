using EmailProvider.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Countries;
using EMailProviderClient.Dispatches.Users;
using EMailProviderClient.LangSupport;
using EMailProviderClient.Validation;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
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
    public partial class UserAccount : Form
    {
        private bool isEditMode = false;
        private ChangePasswordModel? passwordModel = null;
        private UserViewModel currentUser;

        public UserAccount()
        {
            InitializeComponent();
            LoadCountries();
            EMAIL_EDIT.Enabled = false;
        }

        private void UserAccount_Load(object sender, EventArgs e)
        {
            currentUser = UserController.GetCurrentUser();
            if (currentUser == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            EMAIL_EDIT.Text = currentUser.Email;
            NAME_EDIT.Text = currentUser.Name;
            PHONE_NUMBER_EDIT.Text = currentUser.PhoneNumber;
            COUNTRY_CMB.SelectedValue = currentUser.CountryId;

            if (currentUser.Photo.Length > 0)
            {
                using var ms = new MemoryStream(currentUser.Photo);
                pictureBox1.Image = Image.FromStream(ms);
            }

            SetEditMode(false);
        }

        private void SetEditMode(bool enabled)
        {
            isEditMode = enabled;

            NAME_EDIT.Enabled = enabled;
            PHONE_NUMBER_EDIT.Enabled = enabled;
            COUNTRY_CMB.Enabled = enabled;
            UPLOAD_BTN.Enabled = enabled;
            CHANGE_PASSWORD_BTN.Enabled = enabled;

            SAVE_EDIT_BTN.Text = enabled ? DlgLangSupport.Save : DlgLangSupport.Edit;
        }

        private async void SAVE_EDIT_BTN_Click(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                SetEditMode(true);
                return;
            }

            UserValidatorC UserValidatorC = new UserValidatorC();
            if(!string.IsNullOrEmpty(NAME_EDIT.Text))
                UserValidatorC.AddValidationField(UserValidationTypes.ValidationTypeName, NAME_EDIT);
            if (!string.IsNullOrEmpty(PHONE_NUMBER_EDIT.Text))
                UserValidatorC.AddValidationField(UserValidationTypes.ValidationTypePhoneNumber, PHONE_NUMBER_EDIT);
            if (!UserValidatorC.Validate(true))
                return;

            UserViewModel newUserData = currentUser;
            newUserData.Name = NAME_EDIT.Text;
            newUserData.PhoneNumber = PHONE_NUMBER_EDIT.Text;

            var selectedCountry = (CountryViewModel)COUNTRY_CMB.SelectedItem;
            if(selectedCountry != null)
                newUserData.CountryId = COUNTRY_CMB.SelectedIndex >= 0 ? selectedCountry.Id: newUserData.CountryId;

            if (pictureBox1.Image != null)
            {
                using var ms = new MemoryStream();
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                currentUser.Photo = ms.ToArray();
            }

            if (newUserData.CountryId > 0 && currentUser.PrefferedLanguageId != selectedCountry.LanguageId)
            {
                var confirmResult = MessageBox.Show(
                    DlgLangSupport.ChangeLanguageQuestion,
                    DlgLangSupport.ChangeLanguage,
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    newUserData.PrefferedLanguageId = selectedCountry.LanguageId;
                }

                Logger.LogInfo(DlgLangSupport.RestartNeeded);
            }

            var passwordModelToSend = passwordModel != null &&
                string.IsNullOrWhiteSpace(passwordModel?.OldPassword) &&
                string.IsNullOrWhiteSpace(passwordModel?.NewPassword) &&
                string.IsNullOrWhiteSpace(passwordModel?.ConfirmPassword)
                    ? null
                    : passwordModel;

            var success = await UserDispatchesC.EditProfile(
                newUserData,
                passwordModelToSend
            );

            if (!success)
            {
                MessageBox.Show(DlgLangSupport.FailedToSaveChanges, DlgLangSupport.Error);
                return;
            }

            MessageBox.Show(DlgLangSupport.ProfileUpdatedSuccessfully, DlgLangSupport.Saved);
            SetEditMode(false);
            passwordModel = null;
        }

        private void CLOSE_BTN_Click(object sender, EventArgs e) => Close();

        private void LOG_OUT_BTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void UPLOAD_BTN_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void CHANGE_PASSWORD_BTN_Click(object sender, EventArgs e)
        {
            using var dialog = new ChangePassword(passwordModel);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                passwordModel = dialog.GetPasswordModel();
            }
        }

        private async void LoadCountries()
        {
            List<CountryViewModel> listCountries = new List<CountryViewModel>();
            if (!await CountriesDispatchC.LoadCountries(listCountries))
                return;

            if (listCountries.Count == 0)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                COUNTRY_CMB.DataSource = listCountries;
                COUNTRY_CMB.DisplayMember = "Name";
                COUNTRY_CMB.ValueMember = "Id";

                if (listCountries.Count > 0)
                {
                    CountryViewModel currentCountry = null;
                    if (currentUser.CountryId == 0)
                        currentCountry = listCountries.FirstOrDefault(c => c.Name == "Unknown");
                    else
                        currentCountry = listCountries.FirstOrDefault(c => c.Id == currentUser.CountryId);

                    if (currentCountry == null)
                        return;

                    COUNTRY_CMB.SelectedItem = currentCountry;

                    if(string.IsNullOrEmpty(currentUser.PhoneNumber))
                        PHONE_NUMBER_EDIT.Text = currentCountry.PhoneNumberCode;
                }
            });
        }

        private void COUNTRY_CMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCountry = COUNTRY_CMB.SelectedItem as CountryViewModel;
            if (selectedCountry != null && string.IsNullOrEmpty(currentUser.PhoneNumber))
            {
                PHONE_NUMBER_EDIT.Text = selectedCountry.PhoneNumberCode;
            }
        }
    }
}
