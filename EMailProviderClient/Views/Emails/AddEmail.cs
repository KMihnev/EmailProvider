using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Enums;

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
            if (!await SaveEmail())
            {
                this.Show();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async Task<bool> SaveEmail(bool bIsDraft = false)
        {
            emailSerializable.ReceiverEmails = new List<string>((RECEIVER_EDIT.Text.Split(";")));
            emailSerializable.Subject = SUBJECT_EDIT.Text;
            emailSerializable.Content = CONTENT_BOX.Text;
            emailSerializable.DateOfCompletion = DateTime.Now;

            if (bIsDraft)
                emailSerializable.Status = EmailStatusProvider.GetDraftStatus();

            emailSerializable.Files = FILES_LIST.Items.Cast<ListViewItem>()
                .Select(item => new FileSerializable
                {
                    Content = System.IO.File.ReadAllBytes(item.Tag.ToString())
                }).ToList();

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

            if (e.CloseReason == CloseReason.UserClosing && this.DialogResult != DialogResult.OK && !IsEmailEmpty())
            {
                var result = MessageBox.Show(
                    LogMessages.DoYouWishToSaveDraft,
                    LogMessages.SaveAsDraft,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (!await SaveEmail(true))
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

        private void CLOSE_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsEmailEmpty()
        {
            if (RECEIVER_EDIT.Text.Count() > 0
             || SUBJECT_EDIT.Text.Count() > 0
             || CONTENT_BOX.Text.Count() > 0)
                return false;

            return true;
        }
    }
}
