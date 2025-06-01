using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCore.Lists
{
    public abstract class SmartList<TModel> where TModel : class
    {
        protected readonly ListView listView;
        protected List<TModel> items = new();

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

        private void SetupContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            var deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += async (s, e) => await DeleteSelectedItemsAsync();

            var refreshItem = new ToolStripMenuItem("Refresh");
            refreshItem.Click += async (s, e) => await RefreshAsync();

            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(refreshItem);

            contextMenu.Opening += (s, e) =>
            {
                deleteItem.Enabled = listView.CheckedItems.Count > 0 || listView.SelectedItems.Count > 0;
            };

            listView.ContextMenuStrip = contextMenu;
        }

        public async Task RefreshAsync()
        {
            items = await LoadDataAsync();
            listView.Items.Clear();
            foreach (var item in items)
            {
                listView.Items.Add(RenderItem(item));
            }
        }

        private async Task DeleteSelectedItemsAsync()
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
            foreach (ListViewItem item in listView.CheckedItems)
            {
                if (item.Index >= 0 && item.Index < items.Count)
                    selected.Add(items[item.Index]);
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
