using EmailProvider.Models.Serializables;
using EMailProviderClient.Dispatches.Users;
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

namespace EMailProviderClient.Views.User
{
    public partial class Statistics : SmartDialog
    {
        public Statistics()
        {
            InitializeComponent();
            LoadStatistics();
        }

        public async void LoadStatistics()
        {
            StatisticsViewModel statistics = await UserDispatchesC.LoadStatistics();
            if (statistics != null)
            {
                button1.Text = statistics.numberOfUsers.ToString();
                button2.Text = statistics.numberOfOutgoingEmails.ToString();
                button3.Text = statistics.numberOfIncomingEmails.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
