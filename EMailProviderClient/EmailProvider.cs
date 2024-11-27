using EMailProviderClient.Views.Emails;

namespace EMailProviderClient
{
    public partial class EmailProvider : Form
    {
        public EmailProvider()
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void EmailProvider_Load(object sender, EventArgs e)
        {

        }

        private void ON_ADD_EMAIL_Click(object sender, EventArgs e)
        {
            var AddEmailForm = new AddEmail();
            AddEmailForm.ShowDialog();
        }
    }
}
