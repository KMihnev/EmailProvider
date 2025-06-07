using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCore.Lists
{
    public abstract class SmartList<TModel> where TModel : class
    {
        protected readonly ListView listView;

        protected readonly ObservableCollection<TModel> items = new();
        public ObservableCollection<TModel> Items => items;

        protected SmartList(ListView listView)
        {
            this.listView = listView;
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.CheckBoxes = true;
            listView.GridLines = true;
            listView.MultiSelect = false;

            SetupColumns();
            SetupContextMenu();

            listView.Resize += (s, e) => AdjustColumnWidths();
            listView.ItemActivate += (s, e) => OnItemActivate();
        }

        protected abstract void SetupColumns();
        protected abstract ListViewItem RenderItem(TModel model);
        protected abstract Task<List<TModel>> LoadDataAsync();
        protected abstract Task<bool> DeleteItemsAsync(List<TModel> selectedItems);
        protected virtual void OnItemActivate() { }

        protected virtual void InitilizeContextMenu(ContextMenuStrip contextMenu)
        {

            var deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += async (s, e) => await DeleteSelectedItemsAsync();

            contextMenu.Items.Add(deleteItem);

            contextMenu.Opening += (s, e) =>
            {
                deleteItem.Enabled = listView.CheckedItems.Count > 0 || listView.SelectedItems.Count > 0;
            };
        }

        private void SetupContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            InitilizeContextMenu(contextMenu);

            listView.ContextMenuStrip = contextMenu;
        }

        public async Task RefreshAsync()
        {
            var loaded = await LoadDataAsync();

            items.Clear();
            foreach (var item in loaded)
                items.Add(item);  

            listView.Items.Clear(); 
            foreach (var item in items)
            {
                listView.Items.Add(RenderItem(item));
            }
        }
        protected virtual async Task DeleteSelectedItemsAsync()
        {
            var selectedItems = GetSelectedModels();
            if (selectedItems.Count == 0) return;

            var confirm = MessageBox.Show("Delete selected?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            if (await DeleteItemsAsync(selectedItems))
            {
                await RefreshAsync();
            }
        }

        protected List<TModel> GetSelectedModels()
        {
            var selected = new List<TModel>();
            var indexes = new HashSet<int>();

            foreach (ListViewItem item in listView.CheckedItems)
            {
                if (item.Index >= 0 && item.Index < items.Count)
                    indexes.Add(item.Index);
            }

            foreach (ListViewItem item in listView.SelectedItems)
            {
                if (item.Index >= 0 && item.Index < items.Count)
                    indexes.Add(item.Index);
            }

            foreach (int index in indexes)
            {
                selected.Add(items[index]);
            }

            return selected;
        }


        private void AdjustColumnWidths()
        {
            if (listView.Columns.Count == 0) return;

            int total = listView.ClientSize.Width;
            int colWidth = total / listView.Columns.Count;

            foreach (ColumnHeader col in listView.Columns)
                col.Width = colWidth;
        }

        public TModel? GetSelectedItem()
        {
            if (listView.SelectedItems.Count == 0)
                return default;

            int index = listView.SelectedItems[0].Index;
            return (index >= 0 && index < items.Count) ? items[index] : default;
        }

        public TModel? GetSelectedItemTag()
        {
            if (listView.SelectedItems.Count == 0)
                return default;

            return listView.SelectedItems[0].Tag as TModel;
        }
    }
}
