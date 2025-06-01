using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EMailProviderClient.ClientModels;
using EMailProviderClient.Dispatches.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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

            // Make system folders bold
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

        protected override Task<bool> DeleteItemsAsync(List<FolderListModel> selectedItems)
        {
            return Task.FromResult(false);
        }
    }
}
