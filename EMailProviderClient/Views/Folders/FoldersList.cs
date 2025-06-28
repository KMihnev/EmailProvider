using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EMailProviderClient.ClientModels;
using EMailProviderClient.Dispatches.Folders;
using EMailProviderClient.LangSupport;
using EmailServiceIntermediate.Models;
using System.Collections.ObjectModel;
using WindowsFormsCore.Lists;

namespace EMailProviderClient.Views.Folders
{
    public class FoldersList : SmartList<FolderListModel>
    {
        public event Action<FolderListModel> FolderSelected;

        public ObservableCollection<FolderListModel> AllFolders => Items;

        public FoldersList(ListView listView) : base(listView)
        {
            listView.CheckBoxes = false;
            listView.MultiSelect = false;
            listView.ItemSelectionChanged += OnItemSelectionChanged;
        }

        protected override void SetupColumns()
        {
            listView.Columns.Add(DlgLangSupport.Folders, listView.Width - 4);
        }

        protected override async Task<List<FolderListModel>> LoadDataAsync()
        {
            var folders = new List<FolderViewModel>();
            await LoadFoldersDispatchC.LoadFolders(folders);

            var result = new List<FolderListModel>();

            AddSystemAndUserFolders(SystemFolders.Incoming, EmailDirections.EmailDirectionIn, result, folders);
            AddSystemAndUserFolders(SystemFolders.Outgoing, EmailDirections.EmailDirectionOut, result, folders);
            AddSystemAndUserFolders(SystemFolders.Drafts, null, result, folders);
            AddDeletedMessageFolders(result);

            items.Clear();
            foreach (var folder in result)
                items.Add(folder);

            return items.ToList();
        }

        public void AddSystemAndUserFolders(SystemFolders type, EmailDirections? direction, List<FolderListModel> oFoldersList, List<FolderViewModel> oDatabaseFoldersList)
        {
            oFoldersList.Add(new FolderListModel
            {
                FolderID = 0,
                Name = DlgLangSupport.Get(type.ToString()),
                FolderType = type,
                IsMainFolder = true,
            });

            if (direction.HasValue)
            {
                foreach (var userFolder in oDatabaseFoldersList.Where(f => f.FolderDirection == direction))
                {
                    oFoldersList.Add(new FolderListModel
                    {
                        FolderID = userFolder.Id,
                        Name = "    " + userFolder.Name,
                        FolderType = type
                    });
                }
            }
        }

        public void AddDeletedMessageFolders(List<FolderListModel> oFoldersList)
        {
            oFoldersList.Add(new FolderListModel
            {
                FolderID = -1,
                Name = DlgLangSupport.Deleted,
                FolderType = SystemFolders.Deleted,
                OnlyDeleted = true,
                IsMainFolder = true,
            });

            oFoldersList.Add(new FolderListModel
            {
                FolderID = 0,
                Name = "    " + DlgLangSupport.Incoming,
                FolderType = SystemFolders.Incoming,
                OnlyDeleted = true
            });

            oFoldersList.Add(new FolderListModel
            {
                FolderID = 0,
                Name = "    " + DlgLangSupport.Outgoing,
                FolderType = SystemFolders.Outgoing,
                OnlyDeleted = true
            });
        }

        protected override ListViewItem RenderItem(FolderListModel folder)
        {
            var item = new ListViewItem(folder.Name)
            {
                Tag = folder
            };

            if (folder.IsMainFolder)
            {
                item.Font = new System.Drawing.Font(listView.Font, System.Drawing.FontStyle.Bold);
            }

            if (folder.FolderID == -1)
            {
                item.ForeColor = System.Drawing.Color.Gray;
            }

            return item;
        }

        private void OnItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item.Tag is FolderListModel selected)
            {
                if (selected.FolderID == -1)
                {
                    e.Item.Selected = false;
                    return;
                }

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
            var newItem = new ListViewItem(DlgLangSupport.NewFolderEditing)
            {
                Tag = parentFolder
            };

            int insertIndex = listView.Items.IndexOf(
                listView.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag == parentFolder)
            );
            if (insertIndex < 0) insertIndex = listView.Items.Count;

            insertIndex += 1;

            listView.Items.Insert(insertIndex, newItem);
            newItem.Selected = true;
            newItem.EnsureVisible();

            listView.Update();
            var bounds = newItem.Bounds;

            var textBox = new TextBox
            {
                Bounds = bounds,
                Font = listView.Font,
                Text = "",
                BorderStyle = BorderStyle.FixedSingle
            };

            listView.Controls.Add(textBox);
            textBox.BringToFront();
            textBox.Focus();
            await Task.Delay(50);
            textBox.Focus();

            bool finished = false;
            bool keyHandled = false;

            async void FinishEditing(bool save)
            {
                if (finished) return;
                finished = true;

                string Text = textBox.Text;
                listView.Controls.Remove(textBox);
                textBox.Dispose();

                if (save && !string.IsNullOrWhiteSpace(Text))
                {
                    var folderName = Text.Trim();

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

                    folderModel= await AddFolderDispatchC.AddFolder(folderModel);

                    if (folderModel != null)
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
                        MessageBox.Show(DlgLangSupport.FailedToAddFolder, DlgLangSupport.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listView.Items.Remove(newItem);
                    }
                }
                else
                {
                    listView.Items.Remove(newItem);
                }
            }

            textBox.PreviewKeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                    e.IsInputKey = true;
            };

            textBox.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    keyHandled = true;
                    await Task.Delay(1);
                    FinishEditing(true);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    keyHandled = true;
                    await Task.Delay(1);
                    FinishEditing(false);
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (!keyHandled)
                {
                    FinishEditing(true);
                }
            };
        }


        protected override void InitilizeContextMenu(ContextMenuStrip contextMenu)
        {
            var addItem = new ToolStripMenuItem(DlgLangSupport.Add);
            var deleteItem = new ToolStripMenuItem(DlgLangSupport.Delete);

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

            FolderListModel folder = null;
            var selectedItem = listView.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (selectedItem?.Tag is FolderListModel selectedFolder)
            {
                folder = selectedFolder;
            }

            if (folder?.OnlyDeleted == false && folder?.FolderID == 0 && folder?.FolderType != SystemFolders.Drafts)
                contextMenu.Items.Add(addItem);

            if(folder?.FolderID != 0 && folder != null)
                contextMenu.Items.Add(deleteItem);
        }


    }
}
