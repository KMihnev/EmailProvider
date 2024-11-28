using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EmailProvider.Logging;

namespace EMailProviderClient.Views.Emails
{
    public partial class AddEmail : Form
    {
        private MessageSerializable emailSerializable { get; set; }

        public AddEmail()
        {
            InitializeComponent();

            emailSerializable = new MessageSerializable();
            emailSerializable.SenderId = UserController.GetCurrentUserID();
        }

        private async void SEND_BTN_Click(object sender, EventArgs e)
        {
            emailSerializable.ReceiverEmail = RECEIVER_EDIT.Text;
            emailSerializable.Subject = SUBJECT_EDIT.Text;
            emailSerializable.Content = CONTENT_BOX.Text;
            emailSerializable.DateOfCompletion = DateTime.Now;

            emailSerializable.Files = FILES_LIST.Items.Cast<ListViewItem>()
                .Select(item => new FileSerializable
                {
                    Content = System.IO.File.ReadAllBytes(item.Tag.ToString())
                }).ToList();

            if (!await SaveEmail())
           {
               this.Show();
               return;
           }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async Task<bool> SaveEmail()
        {
            emailSerializable.StatusID = 3;
            if (!await SendEmailDispatchC.SendEmail(emailSerializable))
            {
                Logger.LogError(LogMessages.ErrorSavingEmail);
                return false;
            }

            return true;
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing && this.DialogResult != DialogResult.OK)
            {
                var result = MessageBox.Show(
                    LogMessages.DoYouWishToSaveDraft,
                    LogMessages.SaveAsDraft,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    emailSerializable.StatusID = 2;
                    if (!await SaveEmail())
                        return;
                }
                else if (result == DialogResult.Cancel)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();       
                }
            }
        }

        private void UPLOAD_BTN_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem item = new ListViewItem(openFileDialog.SafeFileName)
                    {
                        Tag = openFileDialog.FileName
                    };
                    FILES_LIST.Items.Add(item);
                }
            }
        }
    }
}
