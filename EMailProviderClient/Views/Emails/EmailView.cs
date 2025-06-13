// File: EMailProviderClient/Views/Emails/EMAIL_VIEW.cs
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EmailServiceIntermediate.Logging;
using EmailProvider.Models.Serializables;
using EmailProvider.Enums;
using WindowsFormsCore;
using WindowsFormsCore.Controls;

namespace EMailProviderClient.Views.Emails
{
    public partial class EMAIL_VIEW : SmartDialog
    {
        public event EventHandler EmailSaved;

        private EmailViewModel emailSerializable;
        private AttachedFileList attachedFileList;

        public EMAIL_VIEW() : this(DialogMode.Add) { }

        public EMAIL_VIEW(DialogMode mode) : base(mode, false, true)
        {
            InitializeComponent();

            emailSerializable = new EmailViewModel
            {
                FromEmail = UserController.GetCurrentUserEmail(),
                Direction = EmailDirections.EmailDirectionOut,
                Files = new List<FileViewModel>()
            };

            attachedFileList = new AttachedFileList(FILES_LIST, FILES_CONTEXT, downloadToolStripMenuItem, removeToolStripMenuItem, mode == DialogMode.Preview);

            if (mode == DialogMode.Preview)
            {
                UPLOAD_BTN.Hide();
                SEND_BTN.Hide();
            }
        }

        protected override void FillData()
        {
            if (emailSerializable.Direction == EmailDirections.EmailDirectionOut)
            {
                if (Mode != DialogMode.Add)
                {
                    if(Mode == DialogMode.Edit)
                        HEADER.Text = "Draft Email";
                    else
                        HEADER.Text = "Sent Email";
                }

                if (emailSerializable?.Recipients != null)
                    RECEIVER_EDIT.Text = string.Join(";", emailSerializable.Recipients.Select(r => r.Email));
            }
            else
            {
                RECEIVER_LABEL.Text = "Sender";
                HEADER.Text = "Received Email";
                RECEIVER_EDIT.Text = emailSerializable.FromEmail;
            }

            SUBJECT_EDIT.Text = emailSerializable.Subject ?? string.Empty;

            CONTENT_BOX.Text = emailSerializable.Body ?? string.Empty;

            if (emailSerializable.Files != null)
                attachedFileList.LoadFiles(emailSerializable.Files);
        }

        public void LoadMessage(EmailViewModel message)
        {
            emailSerializable = message;

            FillData();
        }

        private async void SEND_BTN_Click(object sender, EventArgs e)
        {
            if (!await SaveEmail())
            {
                this.Show();
                return;
            }

            EmailSaved?.Invoke(this, EventArgs.Empty);
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task<bool> SaveEmail(bool isDraft = false)
        {
            emailSerializable.Recipients = RECEIVER_EDIT.Text
                .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(email => new MessageRecipientSerializable { Email = email })
                .ToList();

            emailSerializable.Subject = SUBJECT_EDIT.Text;
            emailSerializable.Body = CONTENT_BOX.Text;
            emailSerializable.DateOfRegistration = DateTime.Now;
            emailSerializable.Status = isDraft ? EmailStatuses.EmailStatusDraft : EmailStatuses.EmailStatusNew;
            emailSerializable.Files = attachedFileList.GetFiles();

            if (!await SendEmailDispatchC.SendEmail(emailSerializable))
            {
                Logger.LogError(LogMessages.ErrorSavingEmail);
                return false;
            }

            return true;
        }

        private bool IsEmailEmpty()
        {
            return string.IsNullOrWhiteSpace(RECEIVER_EDIT.Text)
                && string.IsNullOrWhiteSpace(SUBJECT_EDIT.Text)
                && string.IsNullOrWhiteSpace(CONTENT_BOX.Text);
        }

        private void CLOSE_BTN_Click(object sender, EventArgs e) => Close();

        private void UPLOAD_BTN_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;

            var file = new FileViewModel
            {
                Content = File.ReadAllBytes(dialog.FileName),
                Name = Path.GetFileName(dialog.FileName)
            };

            attachedFileList.AddFile(file);
        }

        protected override async Task<bool> OnBeforeClose()
        {
            bool isDraft = Mode == DialogMode.Edit && emailSerializable.Status == EmailStatuses.EmailStatusDraft;
            bool isNew = Mode == DialogMode.Add && emailSerializable.Status == EmailStatuses.EmailStatusNew;

            if (Mode == DialogMode.Preview || DialogResult == DialogResult.OK)
                return true;

            if (IsLogOutInvoked)
                return true;

            if (isNew && !IsEmailEmpty())
            {
                var result = MessageBox.Show(LogMessages.DoYouWishToSaveDraft, LogMessages.SaveAsDraft,
                                             MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    return await SaveEmail(true);
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            if (isDraft)
            {
                var result = MessageBox.Show(LogMessages.SaveChanges, LogMessages.SaveAsDraft,
                             MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    return await SaveEmail(true);
            }

            return true;
        }

    }
}
