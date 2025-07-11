﻿//Includes
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Dispatches.Countries;
using EMailProviderClient.Dispatches.Users;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Validation;
using EMailProviderClient.LangSupport;
using EmailProvider.Models.DBModels;

namespace EMailProviderClient.Views.User
{
    //------------------------------------------------------
    //	SetupProfile
    //------------------------------------------------------
    public partial class SetupProfile : Form
    {
        private UserValidatorC UserValidatorC;
        private bool _isExiting = false;
        public SetupProfile(bool firstTime = true)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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
            this.Close();
        }

        private async void BTN_CONTINUE_Click(object sender, EventArgs e)
        {
            if (!UserValidatorC.Validate(true))
                return;

            var user = UserController.GetCurrentUser();
            user.Name = EDC_NAME.Text;
            user.PhoneNumber = EDC_PHONE_NUMBER.Text;

            if (CMB_COUNTRY.SelectedIndex < 0)
            {
                Logger.Log(LogMessages.NoCountrySelected, LogType.LogTypeScreen, LogSeverity.Warning);
                return;
            }

            var selectedCountry = (CountryViewModel)CMB_COUNTRY.SelectedItem;
            user.CountryId = selectedCountry.Id;

            if (PB_PROFILE.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    PB_PROFILE.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    user.Photo = ms.ToArray();
                }
            }

            if(user.CountryId != 0 && selectedCountry.LanguageId != (int)Languages.LanguagesEnglish)
            {
                var confirmResult = MessageBox.Show(
                  "The selected country has a default language. Do you want to switch your interface language accordingly?",
                  "Change Language",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    user.PrefferedLanguageId = selectedCountry.LanguageId;
                }
            }

            if (!await UserDispatchesC.SetUpProfile(user))
            {
                this.Show();
                return;
            }

            this.Close();
        }


        private void AddValidation()
        {
            UserValidatorC = new UserValidatorC();
            if(!string.IsNullOrEmpty(EDC_NAME.Text))
                UserValidatorC.AddValidationField(UserValidationTypes.ValidationTypeName, EDC_NAME);
            if (!string.IsNullOrEmpty(EDC_PHONE_NUMBER.Text))
                UserValidatorC.AddValidationField(UserValidationTypes.ValidationTypePhoneNumber, EDC_PHONE_NUMBER);
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
                CMB_COUNTRY.DataSource = listCountries;
                CMB_COUNTRY.DisplayMember = "Name";
                CMB_COUNTRY.ValueMember = "Id";

                if (listCountries.Count > 0)
                {
                    CountryViewModel currentCountry = listCountries.FirstOrDefault(c => c.Name == "Unknown");
                    if (currentCountry == null)
                        return;

                    CMB_COUNTRY.SelectedItem = currentCountry;
                    EDC_PHONE_NUMBER.Text = currentCountry.PhoneNumberCode;
                }
            });
        }

        private void ON_COUNTRIES_CHANGE(object sender, EventArgs e)
        {
            var selectedCountry = CMB_COUNTRY.SelectedItem as CountryViewModel;
            if (selectedCountry != null)
            {
                EDC_PHONE_NUMBER.Text = selectedCountry.PhoneNumberCode;
            }
        }
    }
}
