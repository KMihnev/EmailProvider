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
        private SearchData SearchData;
        private SearchTypeFolder CurrentFolderType;

        private List<ViewMessage> messageList;

        public EmailProvider()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            CurrentFolderType = SearchTypeFolder.SearchTypeFolderOutgoing;
            LoadAllForCurrentFolder();
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
            EMAILS_LIST.Items.Clear();

            foreach (var message in messageList)
            {
                string displayText = $"[{message.DateOfCompletion:yyyy-MM-dd HH:mm}] {message.Subject} - {message.SenderEmail}";

                EMAILS_LIST.Items.Add(displayText);
            }
        }

        private void FILTER_BTN_Click(object sender, EventArgs e)
        {

        }

        private async void LoadAllForCurrentFolder()
        {
            var searchData = new SearchData();
            searchData.UserID = UserController.GetCurrentUserID();
            SearchConditionFolder searchConditionFolder = new SearchConditionFolder(CurrentFolderType, "");

            SearchCondition searchCondition = null;

            switch(CurrentFolderType)
            {
                case SearchTypeFolder.SearchTypeFolderDrafts:
                case SearchTypeFolder.SearchTypeFolderOutgoing:
                    searchCondition = new SearchConditionEmail(SearchTypeEmail.SearchTypeEmailSender, UserController.GetCurrentUser().Email);
                    break;
                case SearchTypeFolder.SearchTypeFolderIncoming:
                    searchCondition = new SearchConditionEmail(SearchTypeEmail.SearchTypeEmailReceiver, UserController.GetCurrentUser().Email);
                    break;
            }

            if(searchCondition != null)
                searchData.AddCondition(searchCondition);
            searchData.AddCondition(searchConditionFolder);


            if(! await LoadEmailsDispatchC.LoadEmails(messageList, searchData))
                return;

            FillEmailList();
        }
    }
}
