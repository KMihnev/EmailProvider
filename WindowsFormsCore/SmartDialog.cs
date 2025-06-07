// File: WindowsFormsCore/SmartDialog.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCore
{
    public enum DialogMode
    {
        Preview,
        Edit,
        Add
    }

    public class SmartDialog : Form
    {
        private Button okButton;
        private Button cancelButton;
        private Button closeButton;

        private bool _safeCloseInvoked = false;
        public bool IsMdiEmbedded { get; private set; }

        public DialogMode Mode { get; private set; }

        public SmartDialog() : this(DialogMode.Edit, showStandardButtons: true, isMdiEmbedded: false) { }

        public SmartDialog(DialogMode mode, bool showStandardButtons = true, bool isMdiEmbedded = false)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            Mode = mode;

            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = true;

            if (isMdiEmbedded)
            {
                IsMdiEmbedded = true;
                MaximizeBox = false;
                ShowInTaskbar = false;
            }
        }

        private void ApplyDialogMode()
        {
            switch (Mode)
            {
                case DialogMode.Preview:
                {
                    SetAllControlsEnabled(false);
                    break;
                }
                case DialogMode.Add:
                case DialogMode.Edit:
                {
                    SetAllControlsEnabled(true);
                    break;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ApplyDialogMode();
        }

        private void SetAllControlsEnabled(bool enabled)
        {
            foreach (Control control in Controls)
            {
                if (control is Button) continue;
                control.Enabled = enabled;
            }
        }

        protected virtual void FillData() { }

        protected bool Confirm(string message, string title = "Confirm")
        {
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        public async Task<bool> SafeClose()
        {
            return await OnBeforeClose();
        }

        protected virtual Task<bool> OnBeforeClose()
        {
            return Task.FromResult(true);
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_safeCloseInvoked)
                return;

            bool bResult =await SafeClose();
            _safeCloseInvoked = bResult;
            e.Cancel = !bResult;
        }

        protected async void RunSafe(Func<Task> asyncAction, string errorMessage = "An unexpected error occurred.")
        {
            try
            {
                await asyncAction();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{errorMessage}\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
