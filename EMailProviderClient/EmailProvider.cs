//Includes
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Views.Emails;
using EmailProvider.SearchData;
using EmailProvider.Enums;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EMailProviderClient.Views.Enums;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models.Serializables;

namespace EMailProviderClient
{
    enum SystemFolders
    {
        Incoming = 0, Outgoing = 1, Drafts = 2
    }

    //------------------------------------------------------
    //	EmailProvider
    //------------------------------------------------------
    public partial class EmailProvider : Form
    {
        private bool _bClosing = false;
        private SearchData SearchData = new SearchData();
        private SystemFolders CurrentFolderType;

        private List<MessageSerializable> messageList;

        //Constructor
        public EmailProvider()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.EMAILS_LIST.Resize += (sender, e) => AdjustColumnWidths();
            AdjustColumnWidths();

            CurrentFolderType = SystemFolders.Outgoing;
            messageList = new List<MessageSerializable>();

            InitializeCategories();

            EMAILS_LIST.ItemActivate += EMAILS_LIST_ItemActivate;

            AddEmailListContextMenu();
        }

        //Methods
        private void EmailProvider_Load(object sender, EventArgs e)
        {
        }

        private void ON_ADD_EMAIL_Click(object sender, EventArgs e)
        {
            var AddEmailForm = new EMAIL_VIEW(Views.Enums.DialogMode.DialogModeAdd);
            AddEmailForm.ShowDialog();
            if (AddEmailForm.DialogResult == DialogResult.OK)
                LoadAllForCurrentFolder();
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

                string formattedContent = message.Body.Length > MaxContentLength
                    ? message.Body.Substring(0, MaxContentLength - 3) + "..."
                    : message.Body;

                var item = new ListViewItem(message.DateOfRegistration.ToString("yyyy-MM-dd HH:mm"));

                string emailInfo;
                if (CurrentFolderType == SystemFolders.Incoming)
                    emailInfo = message.FromEmail;
                else
                    emailInfo = string.Join(";",message.Recipients.Select(e=>e.Email));

                item.SubItems.Add(emailInfo);

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
            SearchData.UserId = UserController.GetCurrentUserID();

            switch (CurrentFolderType)
            {
                case SystemFolders.Incoming:
                {
                    if (!await LoadEmailsDispatchC.LoadIncomingEmails(messageList, SearchData))
                        return;
                    break;
                }
                case SystemFolders.Outgoing:
                {
                    if (!await LoadEmailsDispatchC.LoadOutgoingEmails(messageList, SearchData))
                        return;
                    break;
                }
                case SystemFolders.Drafts:
                {
                    if (!await LoadEmailsDispatchC.LoadDrafts(messageList, SearchData))
                        return;
                    break;
                }
            }

            FillEmailList();
        }

        private void AdjustColumnWidths()
        {
            int totalWidth = EMAILS_LIST.ClientSize.Width;
            int remainingWidth = totalWidth - (int)(totalWidth * 0.4) - SystemInformation.VerticalScrollBarWidth;

            EMAILS_LIST.Columns[0].Width = (int)(totalWidth * 0.1);
            EMAILS_LIST.Columns[1].Width = (int)(totalWidth * 0.2);
            EMAILS_LIST.Columns[2].Width = (int)(totalWidth * 0.3);
            EMAILS_LIST.Columns[3].Width = Math.Max(remainingWidth, 100);
        }

        private void InitializeCategories()
        {
            CATEGORIES_LIST.View = View.Details;
            CATEGORIES_LIST.HeaderStyle = ColumnHeaderStyle.Clickable;
            CATEGORIES_LIST.Columns.Clear();
            CATEGORIES_LIST.Columns.Add("System Folders", CATEGORIES_LIST.Width - 4);
            CATEGORIES_LIST.Columns[0].TextAlign = HorizontalAlignment.Center;

            AddCategory(SystemFolders.Incoming, "Received");
            AddCategory(SystemFolders.Outgoing, "Sent");
            AddCategory(SystemFolders.Drafts, "Drafts");

            if (CATEGORIES_LIST.Items.Count > 0)
            {
                CATEGORIES_LIST.Items[0].Selected = true;
            }

            CATEGORIES_LIST.ItemSelectionChanged += CategoriesList_ItemSelectionChanged;
        }

        private void AddCategory(SystemFolders folderType, string displayName)
        {
            CATEGORIES_LIST.Items.Add(new ListViewItem(displayName) { Tag = folderType });
        }

        private void CategoriesList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;

            if (e.Item.Tag is SystemFolders selectedFolder)
            {
                CurrentFolderType = selectedFolder;

                switch (CurrentFolderType)
                {
                    case SystemFolders.Incoming:
                        EMAILS_LIST.Columns[1].Text = "Sender Email";
                        break;
                    case SystemFolders.Drafts:
                    case SystemFolders.Outgoing:
                    default:
                        EMAILS_LIST.Columns[1].Text = "Receiver Email";
                        break;
                }

                LoadAllForCurrentFolder();
            }
        }

        private async void EMAILS_LIST_ItemActivate(object sender, EventArgs e)
        {
            if (EMAILS_LIST.SelectedItems.Count == 0)
                return;

            int selectedIndex = EMAILS_LIST.SelectedItems[0].Index;
            if (selectedIndex < 0 || selectedIndex >= messageList.Count)
                return;

            var selectedMessageId = messageList[selectedIndex].Id;

            var (result, messageSerializable) = await GetEmailDispatchC.LoadEmail(selectedMessageId);

            if (!result || messageSerializable == null)
                return;

            DialogMode dialogMode;
            if (messageSerializable.Status == EmailStatuses.EmailStatusDraft)
                dialogMode = DialogMode.DialogModeEdit;
            else
                dialogMode = DialogMode.DialogModePreview;

            var addEmailForm = new EMAIL_VIEW(dialogMode);
            addEmailForm.LoadMessage(messageSerializable);
            addEmailForm.ShowDialog();

            if(dialogMode != DialogMode.DialogModePreview)
                LoadAllForCurrentFolder();
        }

        private void AddEmailListContextMenu()
        {
            EMAILS_LIST.ContextMenuStrip = contextMenuStrip2;

            this.deleteToolStripMenuItem.Click += DeleteCheckedEmails_Click;
        }

        private async void DeleteCheckedEmails_Click(object sender, EventArgs e)
        {
            var checkedItems = EMAILS_LIST.CheckedItems;
            if (checkedItems.Count == 0)
                return;

            var messageIdsToDelete = new List<int>();
            foreach (ListViewItem item in checkedItems)
            {
                int index = item.Index;
                if (index >= 0 && index < messageList.Count)
                {
                    messageIdsToDelete.Add(messageList[index].Id);
                }
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete the selected emails?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
                return;

            bool deleteResult = await DeleteEmailDispatchC.DeleteEmails(messageIdsToDelete);

            if (deleteResult)
                LoadAllForCurrentFolder();
        }
    }
}
