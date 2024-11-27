using EmailProvider.Enums;
using EmailProvider.Logging;
using EmailProvider.Models.Serializables;
using EMailProviderClient.Dispatches.Countries;
using EMailProviderClient.Dispatches.Users;
using EMailProviderClient.UserControl;
using EMailProviderClient.Validation;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EMailProviderClient.Views.User
{
    public partial class SetupProfile : Form
    {
        private UserValidatorC UserValidatorC;

        public SetupProfile(bool firstTime = true)
        {
            InitializeComponent();
            AddValidation();
            LoadCountries();

            BTN_SKIP.Text = firstTime ? LogMessages.BtnTextSkip : LogMessages.BtnTextCancel;
        }

        private void SetupProfile_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            BTN_CONTINUE.Select();
        }

        private void BTN_UPLOAD_PICTURE_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PB_PROFILE.ImageLocation = ofd.FileName;
            }
        }

        private void BTN_SKIP_Click(object sender, EventArgs e)
        {
            GoToMainScreen();
        }

        private async void BTN_CONTINUE_Click(object sender, EventArgs e)
        {
            if (!UserValidatorC.Validate())
                return;

            var user = UserController.GetCurrentUser();
            user.Name = EDC_NAME.Text;
            user.PhoneNumber = EDC_PHONE_NUMBER.Text;

            if (CMB_COUNTRY.SelectedIndex < 0)
            {
                Logger.Log(LogMessages.NoCountrySelected, LogType.LogTypeScreen, LogSeverity.Warning);
                return;
            }

            var selectedCountry = (CountrySerializable)CMB_COUNTRY.SelectedItem;
            user.CountryId = selectedCountry.Id;

            if (!await UserDispatchesC.SetUpProfile(user))
                return;

            GoToMainScreen();
        }

        private void GoToMainScreen()
        {
            this.Hide();
            var emailProvider = new EmailProvider();
            emailProvider.ShowDialog();
            this.Close();
        }

        private void AddValidation()
        {
            UserValidatorC = new UserValidatorC();
            UserValidatorC.AddValidationField(UserValidationTypes.ValidationTypeName, EDC_NAME);
            UserValidatorC.AddValidationField(UserValidationTypes.ValidationTypePhoneNumber, EDC_PHONE_NUMBER);
        }

        private async void LoadCountries()
        {
            List<CountrySerializable> listCountries = new List<CountrySerializable>();
            if (!await CountriesDispatchC.LoadCountries(listCountries))
                return;

            if (listCountries.Count == 0)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                CMB_COUNTRY.DataSource = listCountries;
                CMB_COUNTRY.DisplayMember = "Name";
                CMB_COUNTRY.ValueMember = "Id";

                if (listCountries.Count > 0)
                {
                    CMB_COUNTRY.SelectedIndex = 0;
                    EDC_PHONE_NUMBER.Text = listCountries[0].PhoneNumberCode;
                }
            });
        }

        private void ON_COUNTRIES_CHANGE(object sender, EventArgs e)
        {
            var selectedCountry = CMB_COUNTRY.SelectedItem as CountrySerializable;
            if (selectedCountry != null)
            {
                EDC_PHONE_NUMBER.Text = selectedCountry.PhoneNumberCode;
            }
        }
    }
}
