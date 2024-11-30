﻿using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Dispatches.Countries;
using EMailProviderClient.Dispatches.Users;
using EMailProviderClient.Controllers.UserControl;
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
            {
                this.Show();
                return;
            }

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
                    CountrySerializable currentCountry = listCountries.FirstOrDefault(c => c.Name == "Unknown");
                    if (currentCountry == null)
                        return;

                    CMB_COUNTRY.SelectedItem = currentCountry;
                    EDC_PHONE_NUMBER.Text = currentCountry.PhoneNumberCode;
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
