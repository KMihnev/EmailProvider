using EmailServiceIntermediate.Logging;
using EMailProviderClient.Controllers.Email;
using EMailProviderClient.Controllers.UserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace EMailProviderClient.Views.User
{
    public partial class StartUp : Form
    {
        private bool _bClosing = false;

        public StartUp()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void StartUp_LogIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var LogInForm = new LogIn();
            var dialogResult = LogInForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
                this.Show();

            OpenDashBoard();
        }

        private void StartUp_Register_Click(object sender, EventArgs e)
        {
            this.Hide();
            var RegisterForm = new Register();
            var dialogResult = RegisterForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
                this.Show();

            OpenDashBoard();
        }

        private void StartUp_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void OpenDashBoard()
        {
            DashboardController.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_bClosing)
                return;

            base.OnFormClosing(e);

            DialogResult result = MessageBox.Show(
                LogMessages.ExitSureCheck,
                LogMessages.ExitConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _bClosing = true;
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
