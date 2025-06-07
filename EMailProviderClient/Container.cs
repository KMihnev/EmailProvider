using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMailProviderClient
{
    public partial class Container : Form
    {
        public Container()
        {
            InitializeComponent();
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;

            ShowEmailList();
        }

        private void ShowEmailList()
        {
            var emailList = new EmailListDialog
            {
                MdiParent = this,
                FormBorderStyle = FormBorderStyle.None,
                ControlBox = false,
                Text = "",
                Dock = DockStyle.Fill
            };

            emailList.Show();
        }
        private void CascadeWindows() => this.LayoutMdi(MdiLayout.Cascade);
        private void TileWindows() => this.LayoutMdi(MdiLayout.TileVertical);
    }

}
