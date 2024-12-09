//Includes
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Enums;
using EMailProviderClient.Views.Enums;

namespace EMailProviderClient.Views.Emails
{
    //------------------------------------------------------
    //	AddEmail
    //------------------------------------------------------
    public partial class AddEmail : Form
    {
        private DialogMode DialogMode { get; set; }
        private MessageSerializable emailSerializable { get; set; }

        //Constructor
        public AddEmail(DialogMode mode)
        {
            InitializeComponent();

            this.DialogMode = mode;
            emailSerializable = new MessageSerializable();
            emailSerializable.SenderId = UserController.GetCurrentUserID();
            InitDialog();
        }

        //Methods
        private void InitDialog()
        {
            switch (DialogMode)
            {
                case DialogMode.DialogModePreview:
                    DialogModePreview();
                    break;
                case DialogMode.DialogModeAdd:
                    DialogModeAdd();
                    break;
                case DialogMode.DialogModeEdit:
                    DialogModeEdit();
                    break;
                default:
                    break;
            }

            if (DialogMode == DialogMode.DialogModePreview)
                DialogModePreview();
        }

        private void DialogModePreview()
        {
            SEND_BTN.Hide();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = false;
            }

            CLOSE_BTN.Enabled = true;

        }

        private void DialogModeEdit()
        {
        }

        private void FillDialogData()
        {
            if (emailSerializable.ReceiverEmails != null && emailSerializable.ReceiverEmails.Count() > 0)
            {
                RECEIVER_EDIT.Text = string.Join(";", emailSerializable.ReceiverEmails);
            }
            else
            {
                RECEIVER_EDIT.Text = string.Empty;
            }

            SUBJECT_EDIT.Text = emailSerializable.Subject ?? string.Empty;
            CONTENT_BOX.Text = emailSerializable.Content ?? string.Empty;

            FILES_LIST.Items.Clear();
            if (emailSerializable.Files != null)
            {
                foreach (var file in emailSerializable.Files)
                {
                    ListViewItem item = new ListViewItem("AttachedFile")
                    {
                    };

                    FILES_LIST.Items.Add(item);
                }
            }
        }

        public void LoadMessage(MessageSerializable message)
        {
            this.emailSerializable = message;
            FillDialogData();
        }

        private void DialogModeAdd()
        {
            return;
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
            else
                emailSerializable.Status= EmailStatusProvider.GetNewStatus();

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

            bool bIsExistingDraft = DialogMode == DialogMode.DialogModeEdit && emailSerializable.Status == EmailStatusProvider.GetDraftStatus();
            bool bIsNewMessage = DialogMode == DialogMode.DialogModeAdd && emailSerializable.Status == EmailStatusProvider.GetNewStatus();

            if (DialogMode == DialogMode.DialogModePreview)
                return;

            if (e.CloseReason != CloseReason.UserClosing)
                return;

            if (this.DialogResult == DialogResult.OK)
                return;

            if (bIsNewMessage)
            {
                if (IsEmailEmpty())
                    return;

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

            if(bIsExistingDraft)
            {
                if (!await SaveEmail(true))
                    return;
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
