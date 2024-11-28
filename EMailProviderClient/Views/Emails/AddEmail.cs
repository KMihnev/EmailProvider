using EmailProvider.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Users;
using EmailServiceIntermediate.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMailProviderClient.Views.Emails
{
    public partial class AddEmail : Form
    {
       // private MessageSerializable emailSerializable { get; set; }

        public AddEmail()
        {
            InitializeComponent();

            //emailSerializable = new MessageSerializable();
            //emailSerializable.SenderId = UserController.GetCurrentUserID();
        }

        private void SEND_BTN_Click(object sender, EventArgs e)
        {
           //if (!await EmailDispatchesC.Send(emailSerializable))
           //{
           //    this.Show();
           //    return;
           //}

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
