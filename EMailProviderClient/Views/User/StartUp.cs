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
        public StartUp()
        {
            InitializeComponent();
        }

        private void StartUp_LogIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var LogInForm = new LogIn();
            LogInForm.ShowDialog();

            OpenDashBoard();
        }

        private void StartUp_Register_Click(object sender, EventArgs e)
        {
            this.Hide();
            var RegisterForm = new Register();
            RegisterForm.ShowDialog();

            OpenDashBoard();
        }

        private void StartUp_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void OpenDashBoard()
        {
            var DashBoard = new EmailProvider();
            DashBoard.ShowDialog();
            this.Close();
        }
    }
}
