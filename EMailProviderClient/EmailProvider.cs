using EmailServiceIntermediate.Logging;
using EMailProviderClient.Views.Emails;
using EmailServiceIntermediate.Models.Serializables;
using EmailProvider.Models.Serializables;
using EmailProvider.SearchData;
using EmailProvider.Enums;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EmailProvider.Models.DBModels;

namespace EMailProviderClient
{
    public partial class EmailProvider : Form
    {
        private bool _bClosing = false;
        private SearchData SearchData = new SearchData();
        private SearchTypeFolder CurrentFolderType;

        private List<ViewMessage> messageList;

        public EmailProvider()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.EMAILS_LIST.Resize += (sender, e) => AdjustColumnWidths();
            AdjustColumnWidths();

            CurrentFolderType = SearchTypeFolder.SearchTypeFolderOutgoing;
            messageList = new List<ViewMessage>();

            InitializeCategories();
        }

        private void EmailProvider_Load(object sender, EventArgs e)
        {
        }

        private void ON_ADD_EMAIL_Click(object sender, EventArgs e)
        {
            var AddEmailForm = new AddEmail();
            AddEmailForm.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_bClosing)
                return;

            base.OnFormClosing(e);

            DialogResult result = MessageBox.Show(
                LogMessages.ExitSureCheck,
                LogMessages.ExitConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _bClosing = true;
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void FillEmailList()
        {
            const int MaxSubjectLength = 30;
            const int MaxContentLength = 100;

            EMAILS_LIST.Items.Clear();

            foreach (var message in messageList)
            {
                string formattedSubject = message.Subject.Length > MaxSubjectLength
                    ? message.Subject.Substring(0, MaxSubjectLength - 3) + "..."
                    : message.Subject;

                string formattedContent = message.Content.Length > MaxContentLength
                    ? message.Content.Substring(0, MaxContentLength - 3) + "..."
                    : message.Content;

                var item = new ListViewItem(message.DateOfCompletion.ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(formattedSubject);
                item.SubItems.Add(formattedContent);

                EMAILS_LIST.Items.Add(item);
            }
        }

        private void FILTER_BTN_Click(object sender, EventArgs e)
        {
            FilterEmails filterDlg = new FilterEmails(SearchData);
            filterDlg.ShowDialog();

            if (filterDlg.DialogResult == DialogResult.OK)
            {
                SearchData = filterDlg.SearchData;
                LoadAllForCurrentFolder();
            }
        }

        private async void LoadAllForCurrentFolder()
        {
            SearchData.UserID = UserController.GetCurrentUserID();
            SearchData.SearchTypeFolder = CurrentFolderType;

            if(! await LoadEmailsDispatchC.LoadEmails(messageList, SearchData))
                return;

            FillEmailList();
        }

        private void AdjustColumnWidths()
        {
            int totalWidth = EMAILS_LIST.ClientSize.Width;
            int remainingWidth = totalWidth - (int)(totalWidth * 0.4) - SystemInformation.VerticalScrollBarWidth;

            EMAILS_LIST.Columns[0].Width = (int)(totalWidth * 0.1);
            EMAILS_LIST.Columns[1].Width = (int)(totalWidth * 0.3);
            EMAILS_LIST.Columns[2].Width = Math.Max(remainingWidth, 100);
        }

        private void InitializeCategories()
        {
            CATEGORIES_LIST.View = View.List;

            AddCategory(SearchTypeFolder.SearchTypeFolderIncoming, "Received");
            AddCategory(SearchTypeFolder.SearchTypeFolderOutgoing, "Sent");
            AddCategory(SearchTypeFolder.SearchTypeFolderDrafts, "Drafts");

            if (CATEGORIES_LIST.Items.Count > 0)
            {
                CATEGORIES_LIST.Items[0].Selected = true;
            }

            CATEGORIES_LIST.ItemSelectionChanged += CategoriesList_ItemSelectionChanged;
        }

        private void AddCategory(SearchTypeFolder folderType, string displayName)
        {
            CATEGORIES_LIST.Items.Add(new ListViewItem(displayName) { Tag = folderType });
        }

        private void CategoriesList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;

            if (e.Item.Tag is SearchTypeFolder selectedFolder)
            {
                CurrentFolderType = selectedFolder;
                LoadAllForCurrentFolder();
            }
        }
    }
}
