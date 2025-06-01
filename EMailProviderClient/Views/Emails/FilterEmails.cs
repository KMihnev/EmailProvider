// File: EMailProviderClient/Views/Emails/FilterEmails.cs
using EmailProvider.Enums;
using EmailProvider.SearchData;
using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsCore;

namespace EMailProviderClient.Views.Emails
{
    public partial class FilterEmails : SmartDialog
    {
        public SearchData SearchData;

        public FilterEmails() : base(DialogMode.Add) { }

        public FilterEmails(SearchData searchData) : base(DialogMode.Edit, showStandardButtons: false)
        {
            InitializeComponent();
            this.SearchData = searchData;

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
                var condition = new SearchConditionDate(SearchTypeDate.SearchTypeDateAfter, DATE_FROM.Value.ToString("yyyy-MM-dd"));
                SearchData.AddCondition(condition);
            }

            if (DATE_TO.Checked)
            {
                var condition = new SearchConditionDate(SearchTypeDate.SearchTypeDateBefore, DATE_TO.Value.ToString("yyyy-MM-dd"));
                SearchData.AddCondition(condition);
            }
        }

        private void FillEmailSearchCondition()
        {
            if (!string.IsNullOrWhiteSpace(RECEIVER_EMAIL.Text))
            {
                var condition = new SearchConditionEmail(RECEIVER_EMAIL.Text);
                SearchData.AddCondition(condition);
            }
        }

        private void CLEAR_BTN_Click(object sender, EventArgs e)
        {
            SearchData.Clear();

            RECEIVER_EMAIL.Clear();
            DATE_FROM.ResetText(); DATE_FROM.Checked = false;
            DATE_TO.ResetText(); DATE_TO.Checked = false;

            BY_DATE_CHB.Checked = false;
            BY_RECEIVER_CHB.Checked = false;
        }

        private void FillData()
        {
            if (SearchData?.Conditions == null) return;

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
            if (condition == null) return;

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
            if (condition == null) return;

            RECEIVER_EMAIL.Text = condition.SearchValue;
            BY_RECEIVER_CHB.Checked = true;
        }
    }
}
