using EMailProviderClient.LangSupport;
using WindowsFormsCore;

namespace EMailProviderClient
{
    partial class EmailListDialog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CATEGORIES_LIST = new ListView();
            SEARCH_BOX = new TextBox();
            CMB_SORT = new ComboBox();
            ON_ADD_EMAIL = new SmartButton();
            SELECT_ALL_CHB = new CheckBox();
            FILTER_BTN = new SmartButton();
            EMAILS_LIST = new ListView();
            DATE_HEADER = new ColumnHeader();
            SUBJECT_HEADER = new ColumnHeader();
            CONTENT_HEADER = new ColumnHeader();
            label1 = new Label();
            button1 = new SmartButton();
            smartButton1 = new SmartButton();
            SuspendLayout();
            // 
            // CATEGORIES_LIST
            // 
            CATEGORIES_LIST.Location = new Point(12, 75);
            CATEGORIES_LIST.Name = "CATEGORIES_LIST";
            CATEGORIES_LIST.Size = new Size(167, 773);
            CATEGORIES_LIST.TabIndex = 0;
            CATEGORIES_LIST.UseCompatibleStateImageBehavior = false;
            CATEGORIES_LIST.View = View.Details;
            // 
            // SEARCH_BOX
            // 
            SEARCH_BOX.Location = new Point(217, 43);
            SEARCH_BOX.Name = "SEARCH_BOX";
            SEARCH_BOX.Size = new Size(209, 25);
            SEARCH_BOX.TabIndex = 4;
            SEARCH_BOX.TextChanged += SEARCH_BOX_TextChanged;
            // 
            // CMB_SORT
            // 
            CMB_SORT.DropDownStyle = ComboBoxStyle.DropDownList;
            CMB_SORT.FormattingEnabled = true;
            CMB_SORT.Location = new Point(1781, 42);
            CMB_SORT.Name = "CMB_SORT";
            CMB_SORT.Size = new Size(121, 25);
            CMB_SORT.TabIndex = 10;
            // 
            // ON_ADD_EMAIL
            // 
            ON_ADD_EMAIL.BackColor = Color.DodgerBlue;
            ON_ADD_EMAIL.FlatStyle = FlatStyle.Flat;
            ON_ADD_EMAIL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ON_ADD_EMAIL.ForeColor = Color.White;
            ON_ADD_EMAIL.Location = new Point(12, 42);
            ON_ADD_EMAIL.Name = "ON_ADD_EMAIL";
            ON_ADD_EMAIL.Size = new Size(167, 26);
            ON_ADD_EMAIL.TabIndex = 11;
            ON_ADD_EMAIL.Text = "[WriteNew]";
            ON_ADD_EMAIL.UseVisualStyleBackColor = true;
            ON_ADD_EMAIL.Click += ON_ADD_EMAIL_Click;
            // 
            // SELECT_ALL_CHB
            // 
            SELECT_ALL_CHB.AutoSize = true;
            SELECT_ALL_CHB.Checked = true;
            SELECT_ALL_CHB.CheckState = CheckState.Checked;
            SELECT_ALL_CHB.Enabled = false;
            SELECT_ALL_CHB.Location = new Point(198, 50);
            SELECT_ALL_CHB.MaximumSize = new Size(100, 0);
            SELECT_ALL_CHB.Name = "SELECT_ALL_CHB";
            SELECT_ALL_CHB.Size = new Size(15, 14);
            SELECT_ALL_CHB.TabIndex = 12;
            SELECT_ALL_CHB.UseVisualStyleBackColor = true;
            SELECT_ALL_CHB.CheckedChanged += SELECT_ALL_CHB_CheckedChanged;
            SELECT_ALL_CHB.MouseUp += SELECT_ALL_CHB_MouseUp;
            // 
            // FILTER_BTN
            // 
            FILTER_BTN.BackColor = Color.DodgerBlue;
            FILTER_BTN.FlatStyle = FlatStyle.Flat;
            FILTER_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            FILTER_BTN.ForeColor = Color.White;
            FILTER_BTN.Location = new Point(432, 43);
            FILTER_BTN.Name = "FILTER_BTN";
            FILTER_BTN.Size = new Size(105, 26);
            FILTER_BTN.TabIndex = 16;
            FILTER_BTN.Text = "[Filter]";
            FILTER_BTN.UseVisualStyleBackColor = true;
            FILTER_BTN.Click += FILTER_BTN_Click;
            // 
            // EMAILS_LIST
            // 
            EMAILS_LIST.CheckBoxes = true;
            EMAILS_LIST.FullRowSelect = true;
            EMAILS_LIST.GridLines = true;
            EMAILS_LIST.Location = new Point(196, 75);
            EMAILS_LIST.Name = "EMAILS_LIST";
            EMAILS_LIST.Size = new Size(1706, 773);
            EMAILS_LIST.TabIndex = 17;
            EMAILS_LIST.UseCompatibleStateImageBehavior = false;
            EMAILS_LIST.View = View.Details;
            // 
            // DATE_HEADER
            // 
            DATE_HEADER.Text = "[Date]";
            DATE_HEADER.Width = 150;
            // 
            // SUBJECT_HEADER
            // 
            SUBJECT_HEADER.Text = "[Subject]";
            SUBJECT_HEADER.Width = 200;
            // 
            // CONTENT_HEADER
            // 
            CONTENT_HEADER.Text = "[Content]";
            CONTENT_HEADER.Width = 300;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1658, 46);
            label1.Name = "label1";
            label1.Size = new Size(86, 19);
            label1.TabIndex = 18;
            label1.Text = "[SortByDate]";
            // 
            // button1
            // 
            button1.BackColor = Color.DodgerBlue;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(543, 42);
            button1.Name = "button1";
            button1.Size = new Size(105, 27);
            button1.TabIndex = 19;
            button1.Text = "[ClearFilter]";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // smartButton1
            // 
            smartButton1.BackColor = Color.DodgerBlue;
            smartButton1.FlatStyle = FlatStyle.Flat;
            smartButton1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            smartButton1.ForeColor = Color.White;
            smartButton1.Location = new Point(12, 10);
            smartButton1.Name = "smartButton1";
            smartButton1.Size = new Size(167, 26);
            smartButton1.TabIndex = 20;
            smartButton1.Text = "[MakeAnnouncement]";
            smartButton1.UseVisualStyleBackColor = true;
            smartButton1.Click += smartButton1_Click;
            // 
            // EmailListDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1915, 867);
            Controls.Add(smartButton1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(EMAILS_LIST);
            Controls.Add(FILTER_BTN);
            Controls.Add(SELECT_ALL_CHB);
            Controls.Add(ON_ADD_EMAIL);
            Controls.Add(CMB_SORT);
            Controls.Add(SEARCH_BOX);
            Controls.Add(CATEGORIES_LIST);
            Name = "EmailListDialog";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += EmailProvider_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView CATEGORIES_LIST;
        private TextBox SEARCH_BOX;
        private ComboBox CMB_SORT;
        private SmartButton ON_ADD_EMAIL;
        private CheckBox SELECT_ALL_CHB;
        private SmartButton FILTER_BTN;
        private ListView EMAILS_LIST;
        private ColumnHeader DATE_HEADER;
        private ColumnHeader SUBJECT_HEADER;
        private ColumnHeader CONTENT_HEADER;
        private Label label1;
        private SmartButton button1;
        private SmartButton smartButton1;
    }
}
