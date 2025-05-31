//Includes
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Views.Emails;
using EmailProvider.SearchData;
using EmailProvider.Enums;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EMailProviderClient.Views.Enums;
using EmailProvider.Models.Serializables;
using EMailProviderClient.ClientModels;
using System.Windows.Forms.VisualStyles;
using EMailProviderClient.Dispatches.Folders;

namespace EMailProviderClient
{

    //------------------------------------------------------
    //	EmailProvider
    //------------------------------------------------------
    public partial class EmailProvider : Form
    {
        private bool _bClosing = false;
        private SearchData SearchData = new SearchData();
        private FolderListModel CurrentFolder;

        private List<EmailListModel> messageList = new List<EmailListModel>();

        //Constructor
        public EmailProvider()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            InitializeCategories();

            AddEmailListContextMenu();
        }

        //Methods
        private void EmailProvider_Load(object sender, EventArgs e)
        {
            EMAILS_LIST.FullRowSelect = true;
            EMAILS_LIST.GridLines = true;
            EMAILS_LIST.CheckBoxes = true;
            EMAILS_LIST.Columns.Add("Date Sent", 100);
            EMAILS_LIST.Columns.Add("Email Info", 100);
            EMAILS_LIST.Columns.Add("Subject", 100);
            EMAILS_LIST.Columns.Add("Content", 100);
            this.EMAILS_LIST.Resize += (sender, e) => AdjustColumnWidths();
            AdjustColumnWidths();

            EMAILS_LIST.ItemActivate += EMAILS_LIST_ItemActivate;
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
                if (CurrentFolder.FolderType == SystemFolders.Incoming)
                    emailInfo = message.FromEmail;
                else
                    emailInfo = string.Join(";",message.Recipients);

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

            if (CurrentFolder.FolderID == 0)
            {
                switch (CurrentFolder.FolderType)
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
            }
            else
            {
                if (!await LoadEmailsDispatchC.LoadEmailsByFolder(messageList, SearchData, CurrentFolder.FolderID))
                    return;
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

        private async void InitializeCategories()
        {
            List<FolderViewModel> FoldersList = new List<FolderViewModel>();
            if(!await LoadFoldersDispatchC.LoadFolders(FoldersList))
            {
                return;
            }

            CATEGORIES_LIST.View = View.Details;
            CATEGORIES_LIST.HeaderStyle = ColumnHeaderStyle.Clickable;
            CATEGORIES_LIST.Columns.Clear();
            CATEGORIES_LIST.Columns.Add("Folders", CATEGORIES_LIST.Width - 4);
            CATEGORIES_LIST.Columns[0].TextAlign = HorizontalAlignment.Left;

            AddCategory(SystemFolders.Incoming);
            AddSubCategory(SystemFolders.Incoming, FoldersList);
            AddCategory(SystemFolders.Outgoing);
            AddSubCategory(SystemFolders.Outgoing, FoldersList);
            AddCategory(SystemFolders.Drafts);

            if (CATEGORIES_LIST.Items.Count > 0)
            {
                CATEGORIES_LIST.Items[0].Selected = true;
            }

            CATEGORIES_LIST.ItemSelectionChanged += CategoriesList_ItemSelectionChanged;
        }

        private void AddCategory(SystemFolders folderType)
        {
            FolderListModel folderListModel = new FolderListModel() { FolderID = 0, Name = folderType.ToString(), FolderType = folderType };
            CATEGORIES_LIST.Items.Add(new ListViewItem(folderListModel.Name) { Tag = folderListModel });
        }

        private void AddSubCategory(SystemFolders folderType, List<FolderViewModel> folderList)
        {
            EmailDirections emailDirections = EmailDirections.EmailDirectionIn;
            if (folderType == SystemFolders.Outgoing)
                emailDirections = EmailDirections.EmailDirectionOut;

            foreach (var folder in folderList.Where(f => f.FolderDirection == emailDirections))
            {
                FolderListModel folderListModel = new FolderListModel() { FolderID = folder.Id, Name = "    " + folder.Name, FolderType = folderType };
                CATEGORIES_LIST.Items.Add(new ListViewItem(folderListModel.Name) { Tag = folderListModel });
            }
        }

        private void CategoriesList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;

            if (e.Item.Tag is FolderListModel selectedFolder)
            {
                CurrentFolder = selectedFolder;

                switch (CurrentFolder.FolderType)
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
