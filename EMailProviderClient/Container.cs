using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Views.Emails;
using EMailProviderClient.Views.User;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsCore;

namespace EMailProviderClient
{
    public partial class Container : Form
    {
        private Panel headerPanel;
        private SmartButton onAccountButton;

        public Container()
        {
            InitializeComponent();
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;

            ConfigureVisuals();
            InitializeHeaderBar();
            ShowEmailList();
            STRIP.Hide();
        }

        private void ShowEmailList()
        {
            var emailList = new EmailListDialog();
            emailList.MdiParent = this;
            emailList.FormBorderStyle = FormBorderStyle.None;
            emailList.ControlBox = false;
            emailList.Text = "";
            emailList.Dock = DockStyle.Fill;
            emailList.TopLevel = false;
            emailList.TopMost = false;
            emailList.ShowInTaskbar = false;
            emailList.Show();
        }

        private void CascadeWindows() => this.LayoutMdi(MdiLayout.Cascade);
        private void TileWindows() => this.LayoutMdi(MdiLayout.TileVertical);

        public void ConfigureVisuals()
        {
            Font = new Font("Segoe UI", 10);
            BackColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeHeaderBar()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = this.BackColor
            };
            this.Controls.Add(headerPanel);

            onAccountButton = new SmartButton
            {
                Text = "My Account ",
                Size = new Size(180, 50),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                ImageAlign = ContentAlignment.MiddleRight,
                TextAlign = ContentAlignment.MiddleLeft
            };
            onAccountButton.Click += OnAccountButton_Click;

            int imgSize = 40;
            var user = UserController.GetCurrentUser();
            if (user?.Photo.GetLength(0) > 0)
            {
                onAccountButton.Image = LoadCircularImage(user.Photo, imgSize);
            }
            else
            {
                Bitmap fallback = new Bitmap(imgSize, imgSize);
                using (Graphics g = Graphics.FromImage(fallback))
                {
                    g.FillEllipse(Brushes.Gray, 0, 0, imgSize, imgSize);
                }
                onAccountButton.Image = fallback;
            }

            headerPanel.Controls.Add(onAccountButton);

            this.Load += (s, e) =>
            {
                onAccountButton.Location = new Point(
                    headerPanel.Width - onAccountButton.Width ,
                    (headerPanel.Height - onAccountButton.Height) / 2
                );
            };

            headerPanel.Resize += (s, e) =>
            {
                onAccountButton.Location = new Point(
                    headerPanel.Width - onAccountButton.Width,
                    (headerPanel.Height - onAccountButton.Height) / 2
                );
            };
        }


        private async void OnAccountButton_Click(object sender, EventArgs e)
        {
            using var userAccountForm = new Views.User.UserAccount();
            userAccountForm.StartPosition = FormStartPosition.CenterParent;
            var result = userAccountForm.ShowDialog(this);

            var closeTasks = new List<Task>();
            if (result == DialogResult.Abort)
            {
                foreach (Form child in this.MdiChildren)
                {
                    if (child is EmailListDialog dialog)
                    {
                        dialog.IsLogOutInvoked = true;
                        closeTasks.Add(dialog.SafeClose());
                    }
                    else if (child is EMAIL_VIEW viewDlg)
                    {
                        viewDlg.IsLogOutInvoked = true;
                        closeTasks.Add(viewDlg.SafeClose());
                    }
                    else
                        child.Close();
                }

                await Task.WhenAll(closeTasks);

                UserController.SetCurrentUser(null);
                SessionControllerC.Clear();

                this.Hide();
                var startUp = new StartUp();
                if (startUp.ShowDialog() == DialogResult.OK)
                {
                    RefreshAccountButtonImage();
                    ShowEmailList();
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }


        private Image LoadCircularImage(byte[] photoBytes, int size = 30)
        {
            using (MemoryStream ms = new MemoryStream(photoBytes))
            {
                Bitmap original = new Bitmap(ms);
                Bitmap resized = new Bitmap(original, new Size(size, size));

                Bitmap circularImage = new Bitmap(size, size);
                using (Graphics g = Graphics.FromImage(circularImage))
                {
                    using (Brush brush = new TextureBrush(resized))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.FillEllipse(brush, 0, 0, size, size);
                    }
                }

                return circularImage;
            }
        }

        private void RefreshAccountButtonImage()
        {
            int imgSize = 40;
            var user = UserController.GetCurrentUser();
            if (user?.Photo.GetLength(0) > 0)
            {
                onAccountButton.Image = LoadCircularImage(user.Photo, imgSize);
            }
            else
            {
                Bitmap fallback = new Bitmap(imgSize, imgSize);
                using (Graphics g = Graphics.FromImage(fallback))
                {
                    g.FillEllipse(Brushes.Gray, 0, 0, imgSize, imgSize);
                }
                onAccountButton.Image = fallback;
            }
        }
    }
}
