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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
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
            this.Hide();
            var SetUpProfile = new SetupProfile();
            SetUpProfile.ShowDialog();
            this.Close();
        }
    }
}
