// File: EMailProviderClient/EmailProvider.cs
using EmailProvider.Models.Serializables;
using EmailProvider.SearchData;
using EMailProviderClient.Views.Emails;
using EMailProviderClient.Views.Folders;
using WindowsFormsCore;

namespace EMailProviderClient
{
    public partial class EmailListDialog : SmartDialog
    {
        private SearchData SearchData = new();
        private EmailsList emailsList;
        private FoldersList foldersList;

        public EmailListDialog() : base(DialogMode.Edit, showStandardButtons: false, isMdiEmbedded: true)
        {
            InitializeComponent();

            emailsList = new EmailsList(EMAILS_LIST, SearchData);
            foldersList = new FoldersList(CATEGORIES_LIST);
        }

        private async void EmailProvider_Load(object sender, EventArgs e)
        {
            foldersList.FolderSelected += async _ =>
            {
                SELECT_ALL_CHB.Checked = false;

                var folder = foldersList.GetSelectedItem();
                if (folder is not null)
                {
                    emailsList.SetCurrentFolder(folder);
                    await emailsList.RefreshAsync();
                }
            };

            await foldersList.RefreshAsync();
            emailsList.SetAvailableFolders(foldersList.AllFolders);
        }

        private void ON_ADD_EMAIL_Click(object sender, EventArgs e)
        {
            var addEmailForm = new EMAIL_VIEW(DialogMode.Add)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.Manual,
                Location = new Point(
                    (this.Width - 600) / 2,
                    (this.Height - 400) / 2
                ),
                BackColor = Color.White
            };

            addEmailForm.EmailSaved += async (_, _) =>
            {
                await emailsList.RefreshAsync();
            };

            this.Controls.Add(addEmailForm);
            addEmailForm.BringToFront();
            addEmailForm.Show();
        }

        private void FILTER_BTN_Click(object sender, EventArgs e)
        {
            RunSafe(async () =>
            {
                var filterDlg = new FilterEmails(SearchData);
                filterDlg.ShowDialog();

                if (filterDlg.DialogResult == DialogResult.OK)
                {
                    SearchData = filterDlg.SearchData;
                    await emailsList.RefreshAsync();
                }
            });
        }

        protected override Task<bool> OnBeforeClose()
        {
            if (!Confirm("Are you sure you want to exit the application?", "Confirm Exit"))
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        private void SELECT_ALL_CHB_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = SELECT_ALL_CHB.Checked;
            if (isChecked)
                emailsList.SelectAllItems();
            else
                emailsList.ClearSelection();
        }
    }
}
