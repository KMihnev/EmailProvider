// File: EMailProviderClient/EmailProvider.cs
using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EmailProvider.SearchData;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Views.Emails;
using EMailProviderClient.Views.Folders;
using EmailServiceIntermediate.Models;
using MDITest;
using System.Windows.Forms.VisualStyles;
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
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.Text = string.Empty;

            emailsList = new EmailsList(EMAILS_LIST, SearchData);
            foldersList = new FoldersList(CATEGORIES_LIST);

            SELECT_ALL_CHB.Enabled = false;
            CMB_SORT.DataSource = Enum.GetValues(typeof(OrderBy));
            CMB_SORT.SelectedItem = SearchData.OrderBy;
            CMB_SORT.SelectedIndexChanged += CMB_SORT_SelectedIndexChanged;

            if (UserController.GetCurrentUser().UserRoleId != (int)UserRoles.UserRoleAdministrator)
                smartButton1.Hide();
        }

        private async void EmailProvider_Load(object sender, EventArgs e)
        {
            MDIClientSupport.SetBevel(this, false);

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
            if (!IsLogOutInvoked)
            {
                if (!Confirm("Are you sure you want to exit the application?", "Confirm Exit"))
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(true);
        }

        private void SELECT_ALL_CHB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void CMB_SORT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CMB_SORT.SelectedItem is OrderBy selectedSort)
            {
                SearchData.OrderBy = selectedSort;
                await emailsList.RefreshAsync();
            }
        }

        private CancellationTokenSource debounceToken;
        private async void SEARCH_BOX_TextChanged(object sender, EventArgs e)
        {
            debounceToken?.Cancel();
            debounceToken = new CancellationTokenSource();
            var token = debounceToken.Token;

            try
            {
                await Task.Delay(300, token);
                if (!token.IsCancellationRequested)
                {
                    SearchData.Keyword = SEARCH_BOX.Text;
                    await emailsList.RefreshAsync();
                }
            }
            catch (TaskCanceledException) { }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SearchData.Clear();
            SEARCH_BOX.Clear();
            emailsList.RefreshAsync();
        }

        private void SELECT_ALL_CHB_MouseUp(object sender, MouseEventArgs e)
        {
            bool isChecked = SELECT_ALL_CHB.Checked;
            if (isChecked)
                emailsList.SelectAllItems();
            else
                emailsList.ClearSelection();
        }

        private void smartButton1_Click(object sender, EventArgs e)
        {
            var addEmailForm = new EMAIL_VIEW(DialogMode.Add, true)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.Manual,
                Location = new Point((this.Width - 600) / 2, (this.Height - 400) / 2),
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
    }
}
