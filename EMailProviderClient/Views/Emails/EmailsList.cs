using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EmailProvider.SearchData;
using EMailProviderClient.ClientModels;
using EMailProviderClient.Dispatches.Emails;
using EMailProviderClient.Views.Folders;
using System.Collections.ObjectModel;
using WindowsFormsCore;
using WindowsFormsCore.Lists;

namespace EMailProviderClient.Views.Emails
{
    public class EmailsList : SmartList<EmailListModel>
    {
        private readonly SearchData searchData;

        private FolderListModel? currentFolder;

        private ObservableCollection<FolderListModel> allUserFolders = new();

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
            if (!email.bIsRead)
            {
                item.Font = new Font(listView.Font, FontStyle.Bold);
            }
            item.SubItems.Add(currentFolder.FolderType == SystemFolders.Incoming ? email.FromEmail : string.Join(";", email.Recipients.Select(r => r.Email)));
            item.SubItems.Add(subject);
            item.SubItems.Add(body);
            item.Tag = email;
            return item;
        }

        protected override async Task<List<EmailListModel>> LoadDataAsync()
        {
            var list = new List<EmailListModel>();

            if (currentFolder == null)
                return list;

            if (currentFolder.FolderID == 0)
            {
                if (currentFolder.OnlyDeleted)
                    searchData.AddCondition(new SearchCondition() { SearchType = SearchType.SearchTypeDeleted });
                else
                    searchData.RemoveCondition(SearchType.SearchTypeDeleted);

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

        public void SetAvailableFolders(ObservableCollection<FolderListModel> folders)
        {
            allUserFolders = folders;
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

            var selectedMessage = GetSelectedItemTag();
            if (selectedMessage == null)
                return;

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

            List<int > messagesToRead = new List<int>();
            messagesToRead.Add(message.Id);

            if(await MarkEmailAsReadDispatchC.MarkEmailsAsRead(messagesToRead));
                selectedMessage.bIsRead = true;

            emailView.LoadMessage(message);

            var container = listView.FindForm();
            container.Controls.Add(emailView);
            emailView.BringToFront();
            emailView.Show();

            await RefreshAsync();
        }

        private string Truncate(string text, int max)
        {
            return string.IsNullOrEmpty(text) ? "" : (text.Length > max ? text[..(max - 3)] + "..." : text);
        }

        public void SetCurrentFolder(FolderListModel folder)
        {
            currentFolder = folder;

            if (listView.Columns.Count > 1)
            {
                listView.Columns[1].Text = folder.FolderType == SystemFolders.Incoming
                    ? "Sender Email"
                    : "Receiver Email";
            }

            _ = RefreshAsync();
        }

        protected override void InitilizeContextMenu(ContextMenuStrip contextMenu)
        {
            var deleteItem = new ToolStripMenuItem("Move to bin");
            deleteItem.Click += async (s, e) => await DeleteSelectedItemsAsync();

            contextMenu.Items.Add(deleteItem);

            contextMenu.Opening += (s, e) =>
            {
                deleteItem.Enabled = listView.CheckedItems.Count > 0 || listView.SelectedItems.Count > 0;
            };

            var refreshItem = new ToolStripMenuItem("Refresh");
            refreshItem.Click += async (s, e) => await RefreshAsync();

            var removeFromFolderItem = new ToolStripMenuItem("Remove from Folder");
            removeFromFolderItem.Click += async (s, e) =>
            {
                if (currentFolder == null || currentFolder.FolderID == 0)
                {
                    MessageBox.Show("This action is only available in custom folders.", "Invalid Operation",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIds = GetSelectedModels().Select(x => x.Id).ToList();

                if (!selectedIds.Any()) return;

                var success = await MoveEmailToFolderDispatchC.RemoveFromFolder(selectedIds);
                if (success)
                    await RefreshAsync();
                else
                    MessageBox.Show("Failed to remove email(s) from folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            var moveToItem = new ToolStripMenuItem("Move to Folder");

            moveToItem.DropDownOpening += (s, e) =>
            {
                moveToItem.DropDownItems.Clear();

                if (currentFolder == null) return;

                var sameDirectionFolders = allUserFolders
                    .Where(f => f.FolderType == currentFolder.FolderType && !f.OnlyDeleted && f.FolderID != 0)
                    .ToList();

                foreach (var folder in sameDirectionFolders)
                {
                    var subItem = new ToolStripMenuItem(folder.Name.Trim());
                    subItem.Tag = folder;
                    subItem.Click += async (sender, args) =>
                    {
                        if (listView.SelectedItems.Count == 0) return;

                        var selectedIds = GetSelectedModels().Select(x => x.Id).ToList();

                        var targetFolder = (FolderListModel)((ToolStripMenuItem)sender).Tag;

                        bool moved = await MoveEmailToFolderDispatchC.MoveEmailToFolder(selectedIds, targetFolder.FolderID);
                         
                        if (moved)
                            await RefreshAsync();
                        else
                            MessageBox.Show("Failed to move email(s).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                    moveToItem.DropDownItems.Add(subItem);
                }
            };

            var markAsReadItem = new ToolStripMenuItem("Mark as Read");
            markAsReadItem.Click += async (s, e) =>
            {
                var selectedIds = GetSelectedModels().Where(m => !m.bIsRead).Select(m => m.Id).ToList();
                if (!selectedIds.Any()) return;

                bool success = await MarkEmailAsReadDispatchC.MarkEmailsAsRead(selectedIds);
                if (success)
                    await RefreshAsync();
            };

            var markAsUnreadItem = new ToolStripMenuItem("Mark as Unread");
            markAsUnreadItem.Click += async (s, e) =>
            {
                var selectedIds = GetSelectedModels().Where(m => m.bIsRead).Select(m => m.Id).ToList();
                if (!selectedIds.Any()) return;

                bool success = await MarkEmailAsUnReadDispatchC.MarkEmailsAsUnRead(selectedIds);
                if (success)
                    await RefreshAsync();
            };

            contextMenu.Items.Add(refreshItem);

            if(currentFolder?.OnlyDeleted == false && allUserFolders.Count > 0)
                contextMenu.Items.Add(moveToItem);
            if(currentFolder?.FolderID !=0 )
                contextMenu.Items.Add(removeFromFolderItem);
            if(currentFolder?.FolderType == SystemFolders.Incoming && currentFolder?.OnlyDeleted == false)
            {
                contextMenu.Items.Add(markAsReadItem);
                contextMenu.Items.Add(markAsUnreadItem);
            }
            if (currentFolder?.OnlyDeleted == false)
                contextMenu.Items.Add(deleteItem);
        }

    }

}
