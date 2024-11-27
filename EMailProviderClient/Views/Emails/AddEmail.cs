using EmailProvider.Models.Serializables;
using EMailProviderClient.UserControl;
using EmailServiceIntermediate.Models;
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
        private OutgoingMessageSerializable emailSerializable { get; set; }

        public AddEmail()
        {
            InitializeComponent();
            emailSerializable.SenderId = UserController.GetCurrentUserID();
            emailSerializable.IsDraft = true;
        }
    }
}
