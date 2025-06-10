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
            ON_ACCOUNT = new SmartButton();
            pictureBox1 = new PictureBox();
            FILTER_BTN = new SmartButton();
            EMAILS_LIST = new ListView();
            DATE_HEADER = new ColumnHeader();
            SUBJECT_HEADER = new ColumnHeader();
            CONTENT_HEADER = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // CATEGORIES_LIST
            // 
            CATEGORIES_LIST.Location = new Point(12, 158);
            CATEGORIES_LIST.Name = "CATEGORIES_LIST";
            CATEGORIES_LIST.Size = new Size(167, 773);
            CATEGORIES_LIST.TabIndex = 0;
            CATEGORIES_LIST.UseCompatibleStateImageBehavior = false;
            CATEGORIES_LIST.View = View.Details;
            // 
            // SEARCH_BOX
            // 
            SEARCH_BOX.Location = new Point(217, 126);
            SEARCH_BOX.Name = "SEARCH_BOX";
            SEARCH_BOX.Size = new Size(209, 25);
            SEARCH_BOX.TabIndex = 4;
            SEARCH_BOX.TextChanged += SEARCH_BOX_TextChanged;
            // 
            // CMB_SORT
            // 
            CMB_SORT.FormattingEnabled = true;
            CMB_SORT.Location = new Point(1781, 119);
            CMB_SORT.Name = "CMB_SORT";
            CMB_SORT.Size = new Size(121, 25);
            CMB_SORT.TabIndex = 10;
            CMB_SORT.Text = "Sort by";
            // 
            // ON_ADD_EMAIL
            // 
            ON_ADD_EMAIL.BackColor = Color.DodgerBlue;
            ON_ADD_EMAIL.FlatStyle = FlatStyle.Flat;
            ON_ADD_EMAIL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ON_ADD_EMAIL.ForeColor = Color.White;
            ON_ADD_EMAIL.Location = new Point(12, 92);
            ON_ADD_EMAIL.Name = "ON_ADD_EMAIL";
            ON_ADD_EMAIL.Size = new Size(167, 59);
            ON_ADD_EMAIL.TabIndex = 11;
            ON_ADD_EMAIL.Text = "Write New";
            ON_ADD_EMAIL.UseVisualStyleBackColor = true;
            ON_ADD_EMAIL.Click += ON_ADD_EMAIL_Click;
            // 
            // SELECT_ALL_CHB
            // 
            SELECT_ALL_CHB.AutoSize = true;
            SELECT_ALL_CHB.Location = new Point(196, 130);
            SELECT_ALL_CHB.MaximumSize = new Size(100, 0);
            SELECT_ALL_CHB.Name = "SELECT_ALL_CHB";
            SELECT_ALL_CHB.Size = new Size(15, 14);
            SELECT_ALL_CHB.TabIndex = 12;
            SELECT_ALL_CHB.UseVisualStyleBackColor = true;
            SELECT_ALL_CHB.CheckedChanged += SELECT_ALL_CHB_CheckedChanged;
            // 
            // ON_ACCOUNT
            // 
            ON_ACCOUNT.BackColor = Color.DodgerBlue;
            ON_ACCOUNT.FlatStyle = FlatStyle.Flat;
            ON_ACCOUNT.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ON_ACCOUNT.ForeColor = Color.White;
            ON_ACCOUNT.Location = new Point(1664, 33);
            ON_ACCOUNT.Name = "ON_ACCOUNT";
            ON_ACCOUNT.Size = new Size(238, 79);
            ON_ACCOUNT.TabIndex = 14;
            ON_ACCOUNT.Text = "Account";
            ON_ACCOUNT.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(1839, 43);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(52, 57);
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // FILTER_BTN
            // 
            FILTER_BTN.BackColor = Color.DodgerBlue;
            FILTER_BTN.FlatStyle = FlatStyle.Flat;
            FILTER_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            FILTER_BTN.ForeColor = Color.White;
            FILTER_BTN.Location = new Point(432, 126);
            FILTER_BTN.Name = "FILTER_BTN";
            FILTER_BTN.Size = new Size(105, 26);
            FILTER_BTN.TabIndex = 16;
            FILTER_BTN.Text = "Filter";
            FILTER_BTN.UseVisualStyleBackColor = true;
            FILTER_BTN.Click += FILTER_BTN_Click;
            // 
            // EMAILS_LIST
            // 
            EMAILS_LIST.CheckBoxes = true;
            EMAILS_LIST.FullRowSelect = true;
            EMAILS_LIST.GridLines = true;
            EMAILS_LIST.Location = new Point(196, 158);
            EMAILS_LIST.Name = "EMAILS_LIST";
            EMAILS_LIST.Size = new Size(1706, 773);
            EMAILS_LIST.TabIndex = 17;
            EMAILS_LIST.UseCompatibleStateImageBehavior = false;
            EMAILS_LIST.View = View.Details;
            // 
            // DATE_HEADER
            // 
            DATE_HEADER.Text = "Date";
            DATE_HEADER.Width = 150;
            // 
            // SUBJECT_HEADER
            // 
            SUBJECT_HEADER.Text = "Subject";
            SUBJECT_HEADER.Width = 200;
            // 
            // CONTENT_HEADER
            // 
            CONTENT_HEADER.Text = "Content";
            CONTENT_HEADER.Width = 300;
            // 
            // EmailListDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1915, 961);
            Controls.Add(EMAILS_LIST);
            Controls.Add(FILTER_BTN);
            Controls.Add(pictureBox1);
            Controls.Add(ON_ACCOUNT);
            Controls.Add(SELECT_ALL_CHB);
            Controls.Add(ON_ADD_EMAIL);
            Controls.Add(CMB_SORT);
            Controls.Add(SEARCH_BOX);
            Controls.Add(CATEGORIES_LIST);
            Name = "EmailListDialog";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += EmailProvider_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView CATEGORIES_LIST;
        private TextBox SEARCH_BOX;
        private ComboBox CMB_SORT;
        private SmartButton ON_ADD_EMAIL;
        private CheckBox SELECT_ALL_CHB;
        private SmartButton ON_ACCOUNT;
        private PictureBox pictureBox1;
        private SmartButton FILTER_BTN;
        private ListView EMAILS_LIST;
        private ColumnHeader DATE_HEADER;
        private ColumnHeader SUBJECT_HEADER;
        private ColumnHeader CONTENT_HEADER;
    }
}
