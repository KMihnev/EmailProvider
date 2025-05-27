//Includes
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Enums;
using EMailProviderClient.Views.Enums;
using EmailProvider.Models.Serializables;
using EmailProvider.Enums;

namespace EMailProviderClient.Views.Emails
{
    //------------------------------------------------------
    //	AddEmail
    //------------------------------------------------------
    public partial class EMAIL_VIEW : Form
    {
        private DialogMode DialogMode { get; set; }
        private MessageSerializable emailSerializable { get; set; }

        //Constructor
        public EMAIL_VIEW(DialogMode mode)
        {
            InitializeComponent();

            this.DialogMode = mode;
            emailSerializable = new MessageSerializable();
            emailSerializable.FromEmail = UserController.GetCurrentUserEmail();
            emailSerializable.Direction = EmailDirections.EmailDirectionOut;
            InitDialog();
            AddFileContextMenu();
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

        private void AddFileContextMenu()
        {
            FILES_LIST.ContextMenuStrip = FILES_CONTEXT;

            this.downloadToolStripMenuItem.Click += DownloadFileMenuItem_Click;
            this.removeToolStripMenuItem.Click += RemoveFileMenuItem_Click;
        }

        private void DialogModePreview()
        {
            SEND_BTN.Hide();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = false;
            }

            FILES_LIST.Enabled = true;
            CLOSE_BTN.Enabled = true;

        }

        private void DialogModeEdit()
        {
        }

        private void FillDialogData()
        {
            var toRecipients = emailSerializable.Recipients.Select(r => r.Email);

            RECEIVER_EDIT.Text = string.Join(";", toRecipients);

            SUBJECT_EDIT.Text = emailSerializable.Subject ?? string.Empty;
            CONTENT_BOX.Text = emailSerializable.Body ?? string.Empty;

            FILES_LIST.Items.Clear();

            if (emailSerializable.Files == null)
                return;

            foreach (var file in emailSerializable.Files)
            {
                ListViewItem item = new ListViewItem(file.Name)
                {
                    Tag = file,
                };

                FILES_LIST.Items.Add(item);
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
            emailSerializable.Recipients = RECEIVER_EDIT.Text
            .Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(email => new MessageRecipientSerializable { Email = email} )
            .ToList();

            emailSerializable.Body = CONTENT_BOX.Text;
            emailSerializable.Subject = SUBJECT_EDIT.Text;
            emailSerializable.DateOfRegistration = DateTime.Now;

            if (bIsDraft)
                emailSerializable.Status = EmailStatuses.EmailStatusDraft;
            else
                emailSerializable.Status = EmailStatuses.EmailStatusNew;

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

            bool bIsExistingDraft = DialogMode == DialogMode.DialogModeEdit && emailSerializable.Status == EmailStatuses.EmailStatusDraft;
            bool bIsNewMessage = DialogMode == DialogMode.DialogModeAdd && emailSerializable.Status == EmailStatuses.EmailStatusNew;

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

            if (bIsExistingDraft)
            {
                if (!await SaveEmail(true))
                    return;
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

        private void UPLOAD_BTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var file = new FileSerializable
            {
                Content = System.IO.File.ReadAllBytes(openFileDialog.FileName),
                Name = System.IO.Path.GetFileName(openFileDialog.FileName)
            };

            emailSerializable.Files.Add(file);

            ListViewItem item = new ListViewItem(file.Name)
            {
                Tag = file
            };

            FILES_LIST.Items.Add(item);
        }

        private void DownloadFileMenuItem_Click(object sender, EventArgs e)
        {
            if(FILES_LIST.SelectedItems.Count <= 0)
                return;

            var selectedItem = FILES_LIST.SelectedItems[0];
            var file = selectedItem.Tag as FileSerializable;

            if (file == null)
                return;
             
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = file.Name
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                System.IO.File.WriteAllBytes(saveFileDialog.FileName, file.Content);
                MessageBox.Show("File downloaded successfully.", "Download File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while downloading the file: {ex.Message}", "Download File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveFileMenuItem_Click(object sender, EventArgs e)
        {
            if (FILES_LIST.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem selectedItem in FILES_LIST.SelectedItems)
            {
                var fileContent = selectedItem.Tag as FileSerializable;

                if (fileContent != null)
                {
                    emailSerializable.Files.Remove(fileContent);
                }

                FILES_LIST.Items.Remove(selectedItem);
            }
        }

        private void FILES_CONTEXT_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogMode == DialogMode.DialogModePreview)
             removeToolStripMenuItem.Enabled = false;
        }
    }
}
