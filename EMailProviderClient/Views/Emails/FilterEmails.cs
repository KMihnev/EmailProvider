using EmailProvider.Enums;
using EmailProvider.SearchData;
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
    public partial class FilterEmails : Form
    {
        public SearchData SearchData { get; set; }

        public FilterEmails()
        {
            InitializeComponent();
        }

        private void APPLY_Click(object sender, EventArgs e)
        {
            FillConditions();


        }

        private void CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BY_DATE_CHB_CheckedChanged(object sender, EventArgs e)
        {
            ToggleGroupBox(BY_DATE_GRP, BY_DATE_CHB.Checked);
        }

        private void BY_RECEIVER_CHB_CheckedChanged(object sender, EventArgs e)
        {
            ToggleGroupBox(BY_RECEIVER_GRP, BY_DATE_CHB.Checked);
        }

        private void ToggleGroupBox(GroupBox groupBox, bool enable)
        {
            foreach (Control control in groupBox.Controls)
                control.Enabled = enable;
        }

        private void FillConditions()
        {
            if (BY_DATE_CHB.Checked)
                FillDateSearchCondition();

            if(BY_RECEIVER_CHB.Checked)
                FillReceiverSearchCondition();

        }

        private void FillDateSearchCondition()
        {
            if(DATE_FROM.Checked)
            {
                SearchConditionDate searchConditionDate = new SearchConditionDate(SearchTypeDate.SearchTypeDateAfter, DATE_FROM.Value.ToString());
                SearchData.AddCondition(searchConditionDate);
            }

            if (DATE_TO.Checked)
            {
                SearchConditionDate searchConditionDate = new SearchConditionDate(SearchTypeDate.SearchTypeDateBefore, DATE_TO.Value.ToString());
                SearchData.AddCondition(searchConditionDate);
            }
        }

        private void FillReceiverSearchCondition()
        {
            if (RECEIVER_EMAIL.Text.Length <= 0)
                return;


            SearchConditionEmail searchConditionDate = new SearchConditionEmail(SearchTypeEmail.SearchTypeEmailReceiver, RECEIVER_EMAIL.Text);
            SearchData.AddCondition(searchConditionDate);
        }
    }
}
