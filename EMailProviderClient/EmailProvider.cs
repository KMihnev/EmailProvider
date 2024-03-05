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
    }
}
