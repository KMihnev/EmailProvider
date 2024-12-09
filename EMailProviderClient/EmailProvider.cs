//Includes
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Views.Emails;
using EmailProvider.SearchData;
using EmailProvider.Enums;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Emails;
using EmailProvider.Models.DBModels;
using EMailProviderClient.Views.Enums;
using EmailServiceIntermediate.Enums;

namespace EMailProviderClient
{
    //------------------------------------------------------
    //	EmailProvider
    //------------------------------------------------------
    public partial class EmailProvider : Form
    {
        private bool _bClosing = false;
        private SearchData SearchData = new SearchData();
        private SearchTypeFolder CurrentFolderType;

        private List<ViewMessage> messageList;

        //Constructor
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

            EMAILS_LIST.ItemActivate += EMAILS_LIST_ItemActivate;
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

                string formattedContent = message.Content.Length > MaxContentLength
                    ? message.Content.Substring(0, MaxContentLength - 3) + "..."
                    : message.Content;

                var item = new ListViewItem(message.DateOfCompletion.ToString("yyyy-MM-dd HH:mm"));

                string emailInfo;
                if (CurrentFolderType == SearchTypeFolder.SearchTypeFolderIncoming)
                    emailInfo = message.SenderEmail;
                else
                    emailInfo = message.ReceiverEmails;

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

                switch (CurrentFolderType)
                {
                    case SearchTypeFolder.SearchTypeFolderIncoming:
                        EMAILS_LIST.Columns[1].Text = "Sender Email";
                        break;
                    case SearchTypeFolder.SearchTypeFolderDrafts:
                    case SearchTypeFolder.SearchTypeFolderOutgoing:
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

            var selectedMessageId = messageList[selectedIndex].MessageId;

            var (result, messageSerializable) = await GetEmailDispatchC.LoadEmail(selectedMessageId);

            if (!result || messageSerializable == null)
                return;

            int draftStatus = EmailStatusProvider.GetDraftStatus();

            DialogMode dialogMode;
            if (messageSerializable.Status == draftStatus)
                dialogMode = DialogMode.DialogModeEdit;
            else
                dialogMode = DialogMode.DialogModePreview;

            var addEmailForm = new EMAIL_VIEW(dialogMode);
            addEmailForm.LoadMessage(messageSerializable);
            addEmailForm.ShowDialog();

            if(dialogMode != DialogMode.DialogModePreview)
                LoadAllForCurrentFolder();
        }
    }
}
