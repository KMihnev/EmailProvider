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
    }
}
