//Includes
using EmailServiceIntermediate.Logging;

namespace EMailProviderClient.Views.User
{
    //------------------------------------------------------
    //	StartUp
    //------------------------------------------------------
    public partial class StartUp : Form
    {
        private bool _bClosing = false;

        //Constructor
        public StartUp()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        //Methods
        private void StartUp_LogIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LogIn();
            var dialogResult = loginForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close(); // closes StartUp, lets Program.cs proceed
            }
            else
            {
                this.Show();
            }
        }

        private void StartUp_Register_Click(object sender, EventArgs e)
        {
            this.Hide();
            var registerForm = new Register();
            var dialogResult = registerForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close(); // closes StartUp, lets Program.cs proceed
            }
            else
            {
                this.Show();
            }
        }

        private void StartUp_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}
