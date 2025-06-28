// File: WindowsFormsCore/Controls/AttachedFileList.cs
using EmailProvider.Models.Serializables;
using EMailProviderClient.LangSupport;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsCore.Controls
{
    public class AttachedFileList
    {
        private readonly ListView fileList;
        private readonly ContextMenuStrip contextMenu;
        private readonly ToolStripMenuItem downloadItem;
        private readonly ToolStripMenuItem removeItem;
        private readonly List<FileViewModel> fileStorage;
        private readonly bool isReadOnly;

        public IReadOnlyList<FileViewModel> Files => fileStorage.AsReadOnly();

        public AttachedFileList(ListView listView, ContextMenuStrip menu, ToolStripMenuItem download, ToolStripMenuItem remove, bool readOnly = false)
        {
            fileList = listView;
            contextMenu = menu;
            downloadItem = download;
            removeItem = remove;
            isReadOnly = readOnly;

            fileStorage = new List<FileViewModel>();

            fileList.ContextMenuStrip = contextMenu;

            if (!contextMenu.Items.Contains(downloadItem))
                contextMenu.Items.Add(downloadItem);

            if (!contextMenu.Items.Contains(removeItem))
                contextMenu.Items.Add(removeItem);

            contextMenu.Opening += ContextMenu_Opening;
            downloadItem.Click += DownloadFile_Click;
            removeItem.Click += RemoveFile_Click;
        }

        public void Enable()
        {
            fileList.Enabled = true;
        }

        public void LoadFiles(IEnumerable<FileViewModel> files)
        {
            fileStorage.Clear();
            fileStorage.AddRange(files);
            RefreshListView();
        }

        public void AddFile(FileViewModel file)
        {
            fileStorage.Add(file);
            var item = new ListViewItem(file.Name) { Tag = file };
            fileList.Items.Add(item);
        }

        private void RefreshListView()
        {
            fileList.Items.Clear();
            foreach (var file in fileStorage)
            {
                var item = new ListViewItem(file.Name) { Tag = file };
                fileList.Items.Add(item);
            }
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (fileList.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            removeItem.Enabled = !isReadOnly;
        }

        private void DownloadFile_Click(object sender, EventArgs e)
        {
            if (fileList.SelectedItems.Count <= 0) return;

            var file = fileList.SelectedItems[0].Tag as FileViewModel;
            if (file == null) return;

            var dialog = new SaveFileDialog { FileName = file.Name };
            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                File.WriteAllBytes(dialog.FileName, file.Content);
                MessageBox.Show(DlgLangSupport.FileDownloadSuccessfully, DlgLangSupport.DownloadFile, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while downloading the file: {ex.Message}", DlgLangSupport.DownloadFile, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveFile_Click(object sender, EventArgs e)
        {
            if (fileList.SelectedItems.Count <= 0) return;

            foreach (ListViewItem item in fileList.SelectedItems)
            {
                if (item.Tag is FileViewModel file)
                {
                    fileStorage.Remove(file);
                    fileList.Items.Remove(item);
                }
            }
        }

        public List<FileViewModel> GetFiles() => new(fileStorage);
    }
}
