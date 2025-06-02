using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EMailProviderClient.ClientModels;
using EMailProviderClient.Dispatches.Folders;
using WindowsFormsCore.Lists;

namespace EMailProviderClient.Views.Folders
{
    public class FoldersList : SmartList<FolderListModel>
    {
        public event Action<FolderListModel> FolderSelected;

        public FoldersList(ListView listView) : base(listView)
        {
            listView.CheckBoxes = false;
            listView.MultiSelect = false;
            listView.ItemSelectionChanged += OnItemSelectionChanged;
        }

        protected override void SetupColumns()
        {
            listView.Columns.Add("Folders", listView.Width - 4);
        }

        protected override async Task<List<FolderListModel>> LoadDataAsync()
        {
            var folders = new List<FolderViewModel>();
            await LoadFoldersDispatchC.LoadFolders(folders);

            var result = new List<FolderListModel>();

            void AddSystemAndUserFolders(SystemFolders type, EmailDirections? direction)
            {
                // Add system folder
                result.Add(new FolderListModel
                {
                    FolderID = 0,
                    Name = type.ToString(),
                    FolderType = type
                });

                if (direction.HasValue)
                {
                    foreach (var userFolder in folders.Where(f => f.FolderDirection == direction))
                    {
                        result.Add(new FolderListModel
                        {
                            FolderID = userFolder.Id,
                            Name = "    " + userFolder.Name, // indented
                            FolderType = type
                        });
                    }
                }
            }

            AddSystemAndUserFolders(SystemFolders.Incoming, EmailDirections.EmailDirectionIn);
            AddSystemAndUserFolders(SystemFolders.Outgoing, EmailDirections.EmailDirectionOut);
            AddSystemAndUserFolders(SystemFolders.Drafts, null);

            return result;
        }

        protected override ListViewItem RenderItem(FolderListModel folder)
        {
            var item = new ListViewItem(folder.Name)
            {
                Tag = folder
            };

            if (folder.FolderID == 0)
            {
                item.Font = new System.Drawing.Font(listView.Font, System.Drawing.FontStyle.Bold);
            }

            return item;
        }

        private void OnItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item.Tag is FolderListModel selected)
            {
                FolderSelected?.Invoke(selected);
            }
        }

        protected override async Task<bool> DeleteItemsAsync(List<FolderListModel> selectedItems)
        {
            foreach (var item in selectedItems)
            {
                if (!await DeleteFolderDispatchC.DeleteFolder(item.FolderID))
                    return false;

            }
            return true;
        }

        public async Task AddFolderAsync(FolderListModel parentFolder)
        {
            var newItem = new ListViewItem("New Folder (editing...)")
            {
                Tag = parentFolder
            };

            int insertIndex = listView.Items.IndexOf(
                listView.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag == parentFolder)
            ) + 1;

            listView.Items.Insert(insertIndex, newItem);
            newItem.Selected = true;
            newItem.EnsureVisible();

            var bounds = newItem.Bounds;

            var textBox = new TextBox
            {
                Bounds = bounds,
                Font = listView.Font,
                Text = "",
                BorderStyle = BorderStyle.FixedSingle
            };

            listView.Controls.Add(textBox);
            textBox.Focus();

            bool finished = false;

            async void FinishEditing(bool save)
            {
                if (finished) return;
                finished = true;

                listView.Controls.Remove(textBox);
                textBox.Dispose();

                if (save && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    var folderName = textBox.Text.Trim();

                    var folderModel = new FolderViewModel
                    {
                        Name = folderName,
                        FolderDirection = parentFolder?.FolderType switch
                        {
                            SystemFolders.Incoming => EmailDirections.EmailDirectionIn,
                            SystemFolders.Outgoing => EmailDirections.EmailDirectionOut,
                            _ => EmailDirections.EmailDirectionOut
                        }
                    };

                    bool success = await AddFolderDispatchC.AddFolder(folderModel);

                    if (success)
                    {
                        listView.Items.Remove(newItem);

                        var newFolderModel = new FolderListModel
                        {
                            FolderID = folderModel.Id,
                            Name = "    " + folderModel.Name,
                            FolderType = parentFolder.FolderType
                        };

                        var newListItem = new ListViewItem(newFolderModel.Name)
                        {
                            Tag = newFolderModel
                        };

                        listView.Items.Insert(insertIndex, newListItem);
                        newListItem.Selected = true;
                        newListItem.EnsureVisible();
                        items.Insert(insertIndex, newFolderModel);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listView.Items.Remove(newItem);
                    }
                }
                else
                {
                    listView.Items.Remove(newItem);
                }
            }

            textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    FinishEditing(true);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    FinishEditing(false);
                }
            };

            textBox.LostFocus += (s, e) => FinishEditing(true);
        }





        protected override void InitilizeContextMenu(ContextMenuStrip contextMenu)
        {
            var addItem = new ToolStripMenuItem("Add");
            var deleteItem = new ToolStripMenuItem("Delete");

            addItem.Click += async (s, e) =>
            {
                if (listView.SelectedItems.Count > 0 &&
                    listView.SelectedItems[0].Tag is FolderListModel selected &&
                    selected.FolderID == 0)
                {
                    await AddFolderAsync(selected);
                }
            };

            deleteItem.Click += async (s, e) => await DeleteSelectedItemsAsync();

            contextMenu.Opening += (s, e) =>
            {
                bool showAdd = listView.SelectedItems.Count > 0 &&
                               listView.SelectedItems[0].Tag is FolderListModel selected &&
                               selected.FolderID == 0 &&
                               selected.FolderType != SystemFolders.Drafts;

                addItem.Visible = showAdd;

                bool userSelected =
                    listView.SelectedItems.Cast<ListViewItem>().Any(i => i.Tag is FolderListModel f && f.FolderID != 0) ||
                    listView.CheckedItems.Cast<ListViewItem>().Any(i => i.Tag is FolderListModel f && f.FolderID != 0);

                deleteItem.Visible = userSelected;
                deleteItem.Enabled = userSelected;
            };

            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(addItem);
        }


    }
}
