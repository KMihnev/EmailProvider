using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMailProviderClient.Views.User
{
    public partial class SetupProfile : Form
    {
        public SetupProfile()
        {
            InitializeComponent();
        }

        private void SetupProfile_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            BTN_CONTINUE.Select();
        }

        private void BTN_UPLOAD_PICTURE_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                PB_PROFILE.ImageLocation = ofd.FileName.ToString();
            }
        }
    }
}
