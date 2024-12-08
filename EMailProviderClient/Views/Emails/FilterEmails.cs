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
        public SearchData SearchData;

        public FilterEmails(SearchData SearchData)
        {
            InitializeComponent();

            this.SearchData = SearchData;

            DATE_TO.Checked = false;
            DATE_FROM.Checked = false;

            BY_RECEIVER_CHB.Checked = false;
            BY_DATE_CHB.Checked = false;

            ToggleGroupBox(BY_DATE_GRP, BY_DATE_CHB.Checked);
            ToggleGroupBox(BY_RECEIVER_GRP, BY_RECEIVER_CHB.Checked);

            FillData();
        }

        private void APPLY_Click(object sender, EventArgs e)
        {
            FillConditions();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BY_DATE_CHB_CheckedChanged(object sender, EventArgs e)
        {
            ToggleGroupBox(BY_DATE_GRP, BY_DATE_CHB.Checked);
        }

        private void BY_RECEIVER_CHB_CheckedChanged(object sender, EventArgs e)
        {
            ToggleGroupBox(BY_RECEIVER_GRP, BY_RECEIVER_CHB.Checked);
        }

        private void ToggleGroupBox(GroupBox groupBox, bool enable)
        {
            foreach (Control control in groupBox.Controls)
                control.Enabled = enable;
        }

        private void FillConditions()
        {
            SearchData.Clear();
            if (BY_DATE_CHB.Checked)
                FillDateSearchCondition();

            if (BY_RECEIVER_CHB.Checked)
                FillEmailSearchCondition();

        }

        private void FillDateSearchCondition()
        {
            if (DATE_FROM.Checked)
            {
                SearchConditionDate searchConditionDate = new SearchConditionDate(SearchTypeDate.SearchTypeDateAfter, DATE_FROM.Value.ToString("yyyy-MM-dd"));
                SearchData.AddCondition(searchConditionDate);
            }

            if (DATE_TO.Checked)
            {
                SearchConditionDate searchConditionDate = new SearchConditionDate(SearchTypeDate.SearchTypeDateBefore, DATE_TO.Value.ToString("yyyy-MM-dd"));
                SearchData.AddCondition(searchConditionDate);
            }
        }

        private void FillEmailSearchCondition()
        {
            if (RECEIVER_EMAIL.Text.Length <= 0)
                return;

            SearchConditionEmail searchConditionReceiver = new SearchConditionEmail(SearchTypeEmail.SearchTypeEmailReceiver, RECEIVER_EMAIL.Text);
            SearchData.AddCondition(searchConditionReceiver);


            if (SENDER_EMAIL.Text.Length <= 0)
                return;

            SearchConditionEmail searchConditionSender = new SearchConditionEmail(SearchTypeEmail.SearchTypeEmailSender, SENDER_EMAIL.Text);
            SearchData.AddCondition(searchConditionSender);
        }

        private void CLEAR_BTN_Click(object sender, EventArgs e)
        {
            SearchData.Clear();

            SENDER_EMAIL.Clear();
            RECEIVER_EMAIL.Clear();
            DATE_FROM.ResetText();
            DATE_FROM.Checked = false;
            DATE_TO.ResetText();
            DATE_TO.Checked = false;

            BY_DATE_CHB.Checked = false;
            BY_RECEIVER_CHB.Checked = false;
        }

        private void FillData()
        {
            if (SearchData == null || SearchData.Conditions == null)
                return;

            foreach (var condition in SearchData.Conditions)
            {
                switch (condition.SearchType)
                {
                    case SearchType.SearchTypeDate:
                        FillDateFields(condition as SearchConditionDate);
                        break;

                    case SearchType.SearchTypeEmail:
                        FillEmailFields(condition as SearchConditionEmail);
                        break;
                }
            }
        }

        private void FillDateFields(SearchConditionDate condition)
        {
            if (condition == null)
                return;

            switch ((SearchTypeDate)condition.SearchSubType)
            {
                case SearchTypeDate.SearchTypeDateAfter:
                    DATE_FROM.Value = DateTime.Parse(condition.SearchValue);
                    DATE_FROM.Checked = true;
                    BY_DATE_CHB.Checked = true;
                    break;

                case SearchTypeDate.SearchTypeDateBefore:
                    DATE_TO.Value = DateTime.Parse(condition.SearchValue);
                    DATE_TO.Checked = true;
                    BY_DATE_CHB.Checked = true;
                    break;
            }
        }

        private void FillEmailFields(SearchConditionEmail condition)
        {
            if (condition == null)
                return;

            switch ((SearchTypeEmail)condition.SearchSubType)
            {
                case SearchTypeEmail.SearchTypeEmailReceiver:
                    RECEIVER_EMAIL.Text = condition.SearchValue;
                    BY_RECEIVER_CHB.Checked = true;
                    break;

                case SearchTypeEmail.SearchTypeEmailSender:
                    SENDER_EMAIL.Text = condition.SearchValue;
                    BY_RECEIVER_CHB.Checked = true;
                    break;
            }
        }
    }
}
