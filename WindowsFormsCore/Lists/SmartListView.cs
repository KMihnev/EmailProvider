using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
            listView.CheckBoxes = false;
            listView.MultiSelect = true;

            listView.BorderStyle = BorderStyle.None;
            listView.BackColor = Color.White;
            listView.ForeColor = Color.Black;
            listView.Font = new Font("Segoe UI", 10);
            listView.GridLines = false;
            listView.OwnerDraw = true;

            typeof(ListView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, listView, new object[] { true });

            listView.DrawColumnHeader += (s, e) =>
            {
                using var headerBrush = new SolidBrush(Color.Gainsboro);
                e.Graphics.FillRectangle(headerBrush, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Header.Text, listView.Font, e.Bounds, Color.Black);
            };

            listView.DrawItem += (s, e) =>{};

            listView.DrawSubItem += (s, e) =>
            {
                bool isSelected = e.Item.Selected && listView.Focused;

                using var backgroundBrush = new SolidBrush(isSelected ? Color.LightSkyBlue : Color.White);
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

                TextRenderer.DrawText(
                    e.Graphics,
                    e.SubItem.Text,
                    e.Item.Font,
                    e.Bounds,
                    Color.Black,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                );
            };

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

            contextMenu.Opening += (s, e) =>
            {
                contextMenu.Items.Clear();
                InitilizeContextMenu(contextMenu);
            };

            contextMenu.Opening += (s, e) =>
            {
                if (listView.SelectedItems.Count == 0 && listView.CheckedItems.Count == 0)
                {
                    e.Cancel = true;
                    return;
                }

                contextMenu.Items.Clear();
                InitilizeContextMenu(contextMenu);
            };

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

        public void SelectAllItems()
        {
            listView.BeginUpdate();

            bool first = true;
            foreach (ListViewItem item in listView.Items)
            {
                item.Selected = true;
                item.Focused = first;
                first = false;
            }

            listView.Focus();
            listView.EndUpdate();
            listView.Invalidate();
        }

        public void ClearSelection()
        {
            listView.BeginUpdate();

            foreach (ListViewItem item in listView.Items)
            {
                item.Selected = false;
                item.Focused = false;
            }

            listView.EndUpdate();
            listView.Invalidate();
        }
    }
}
