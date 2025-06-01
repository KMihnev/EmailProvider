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
        public bool ShowStandardButtons { get; private set; } = true;

        public string OkButtonText { get; set; } = "OK";
        public string CancelButtonText { get; set; } = "Cancel";

        public SmartDialog() : this(DialogMode.Edit, showStandardButtons: true, isMdiEmbedded: false) { }

        public SmartDialog(DialogMode mode, bool showStandardButtons = true, bool isMdiEmbedded = false)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            Mode = mode;
            ShowStandardButtons = showStandardButtons;

            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Size = new Size(600, 400);
            ControlBox = true;

            if (isMdiEmbedded)
            {
                IsMdiEmbedded = true;
                FormBorderStyle = FormBorderStyle.None;
                MaximizeBox = false;
                MinimizeBox = false;
                ControlBox = false;
                ShowIcon = false;
                ShowInTaskbar = false;
                Dock = DockStyle.Fill;
            }


            if (showStandardButtons)
                CreateStandardButtons();
        }

        private void CreateStandardButtons()
        {
            okButton = new Button { Text = OkButtonText, DialogResult = DialogResult.OK, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            cancelButton = new Button { Text = CancelButtonText, DialogResult = DialogResult.Cancel, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            closeButton = new Button { Text = "Close", Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            closeButton.Click += (s, e) => this.Close();

            okButton.SetBounds(ClientSize.Width - 180, ClientSize.Height - 40, 75, 30);
            cancelButton.SetBounds(ClientSize.Width - 90, ClientSize.Height - 40, 75, 30);
            closeButton.SetBounds(ClientSize.Width - 90, ClientSize.Height - 40, 75, 30);

            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(closeButton);

            okButton.BringToFront();
            cancelButton.BringToFront();
            closeButton.BringToFront();
        }

        private void ApplyDialogMode()
        {
            switch (Mode)
            {
                case DialogMode.Preview:
                {
                    SetAllControlsEnabled(false);
                    if (ShowStandardButtons)
                    {
                        okButton.Visible = false;
                        cancelButton.Visible = false;
                        closeButton.Visible = true;
                    }
                    else
                    {
                        closeButton?.Hide();
                    }
                    break;
                }
                case DialogMode.Add:
                case DialogMode.Edit:
                {
                    SetAllControlsEnabled(true);
                    if (ShowStandardButtons)
                    {
                        okButton.Visible = true;
                        cancelButton.Visible = true;
                        closeButton.Visible = false;
                    }
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
