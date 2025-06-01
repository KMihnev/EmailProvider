using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EmailProvider.SearchData;
using EMailProviderClient.ClientModels;
using EMailProviderClient.Dispatches.Emails;
using WindowsFormsCore;
using WindowsFormsCore.Lists;

namespace EMailProviderClient.Views.Emails
{
    public class EmailsList : SmartList<EmailListModel>
    {
        private readonly SearchData searchData;

        private FolderListModel? currentFolder;

        public EmailsList(ListView listView, SearchData searchData) : base(listView)
        {
            this.searchData = searchData;
        }

        protected override void SetupColumns()
        {
            listView.Columns.Add("Date", 100);
            listView.Columns.Add("From/To", 100);
            listView.Columns.Add("Subject", 200);
            listView.Columns.Add("Content", 300);
        }

        protected override ListViewItem RenderItem(EmailListModel email)
        {
            string subject = Truncate(email.Subject, 30);
            string body = Truncate(email.Body, 100);
            var item = new ListViewItem(email.DateOfRegistration.ToString("yyyy-MM-dd HH:mm"));
            item.SubItems.Add(currentFolder.FolderType == SystemFolders.Incoming ? email.FromEmail : string.Join(";", email.Recipients.Select(r => r.Email)));
            item.SubItems.Add(subject);
            item.SubItems.Add(body);
            return item;
        }

        protected override async Task<List<EmailListModel>> LoadDataAsync()
        {
            var list = new List<EmailListModel>();

            if (currentFolder == null)
                return list;

            if (currentFolder.FolderID == 0)
            {
                switch (currentFolder.FolderType)
                {
                    case SystemFolders.Incoming:
                        await LoadEmailsDispatchC.LoadIncomingEmails(list, searchData);
                        break;

                    case SystemFolders.Outgoing:
                        await LoadEmailsDispatchC.LoadOutgoingEmails(list, searchData);
                        break;

                    case SystemFolders.Drafts:
                        await LoadEmailsDispatchC.LoadDrafts(list, searchData);
                        break;
                }
            }
            else
            {
                await LoadEmailsDispatchC.LoadEmailsByFolder(list, searchData, currentFolder.FolderID);
            }

            return list;
        }

        protected override async Task<bool> DeleteItemsAsync(List<EmailListModel> selectedItems)
        {
            var ids = selectedItems.Select(x => x.Id).ToList();
            return await DeleteEmailDispatchC.DeleteEmails(ids);
        }

        protected override async void OnItemActivate()
        {
            if (listView.SelectedItems.Count == 0)
                return;

            int index = listView.SelectedItems[0].Index;
            if (index < 0 || index >= items.Count)
                return;

            var selectedMessage = items[index];
            var (result, message) = await GetEmailDispatchC.LoadEmail(selectedMessage.Id);

            if (!result || message == null) return;

            var dialogMode = message.Status == EmailStatuses.EmailStatusDraft
                ? DialogMode.Edit
                : DialogMode.Preview;

            var emailView = new EMAIL_VIEW(dialogMode)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.Manual,
                Location = new Point( (listView.FindForm().Width - 600) / 2, (listView.FindForm().Height - 400) / 2 )
            };

            emailView.LoadMessage(message);

            var container = listView.FindForm();
            container.Controls.Add(emailView);
            emailView.BringToFront();
            emailView.Show();

            if (dialogMode != DialogMode.Preview)
                await RefreshAsync();
        }

        private string Truncate(string text, int max)
        {
            return string.IsNullOrEmpty(text) ? "" : (text.Length > max ? text[..(max - 3)] + "..." : text);
        }

        public void SetCurrentFolder(FolderListModel folder)
        {
            currentFolder = folder;

            // Update column header for email direction
            if (listView.Columns.Count > 1)
            {
                listView.Columns[1].Text = folder.FolderType == SystemFolders.Incoming
                    ? "Sender Email"
                    : "Receiver Email";
            }
        }
    }

}
