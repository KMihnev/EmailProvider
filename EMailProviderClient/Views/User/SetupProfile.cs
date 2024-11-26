using EmailProvider.Enums;
using EmailProvider.Logging;
using EMailProviderClient.Dispatches;
using EMailProviderClient.UserControl;
using EMailProviderClient.Validation;
using EmailServiceIntermediate.Models;
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
    public partial class SetupProfile : Form
    {
        private UserValidatorC UserValidatorC;

        public SetupProfile(bool firstTime = true)
        {
            InitializeComponent();
            AddValidation();
            //LoadCountries();

            if (firstTime)
                BTN_SKIP.Text = LogMessages.BtnTextSkip;
            else
                BTN_SKIP.Text = LogMessages.BtnTextCancel;
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
                PB_PROFILE.ImageLocation = ofd.FileName.ToString();
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

            EmailServiceIntermediate.Models.User user = UserController.GetCurrentUser();
            user.PhoneNumber = EDC_PHONE_NUMBER.Text;
            user.Name = EDC_NAME.Text;

            if (CMB_COUNTRY.SelectedIndex < 0)
                Logger.Log(LogMessages.NoCountrySelected, LogType.LogTypeScreen, LogSeverity.Warning);

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
            List<Country> listCountries = new List<Country>();
            if (!await CountriesDispatchC.LoadCountries(listCountries))
                return;

            if(listCountries.Count == 0)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                CMB_COUNTRY.DataSource = listCountries;
                CMB_COUNTRY.DisplayMember = "Name";
                CMB_COUNTRY.ValueMember = "Id";
            });
        }

        private void ON_COUNTRIES_CHANGE(object sender, EventArgs e)
        {
            if(EDC_PHONE_NUMBER.Text == string.Empty)
                EDC_PHONE_NUMBER.Text = (CMB_COUNTRY.SelectedItem as Country).PhoneNumberCode;
        }
    }
}
