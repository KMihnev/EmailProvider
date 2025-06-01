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
            ON_ADD_EMAIL = new Button();
            checkBox1 = new CheckBox();
            ON_ACCOUNT = new Button();
            pictureBox1 = new PictureBox();
            FILTER_BTN = new Button();
            EMAILS_LIST = new ListView();
            DATE_HEADER = new ColumnHeader();
            SUBJECT_HEADER = new ColumnHeader();
            CONTENT_HEADER = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // CATEGORIES_LIST
            // 
            CATEGORIES_LIST.Location = new Point(12, 139);
            CATEGORIES_LIST.Name = "CATEGORIES_LIST";
            CATEGORIES_LIST.Size = new Size(167, 814);
            CATEGORIES_LIST.TabIndex = 0;
            CATEGORIES_LIST.UseCompatibleStateImageBehavior = false;
            CATEGORIES_LIST.View = View.Details;
            // 
            // SEARCH_BOX
            // 
            SEARCH_BOX.Location = new Point(217, 111);
            SEARCH_BOX.Name = "SEARCH_BOX";
            SEARCH_BOX.Size = new Size(209, 23);
            SEARCH_BOX.TabIndex = 4;
            SEARCH_BOX.Text = "Key word";
            // 
            // CMB_SORT
            // 
            CMB_SORT.FormattingEnabled = true;
            CMB_SORT.Location = new Point(2430, 110);
            CMB_SORT.Name = "CMB_SORT";
            CMB_SORT.Size = new Size(121, 23);
            CMB_SORT.TabIndex = 10;
            CMB_SORT.Text = "Sort by";
            // 
            // ON_ADD_EMAIL
            // 
            ON_ADD_EMAIL.Location = new Point(12, 81);
            ON_ADD_EMAIL.Name = "ON_ADD_EMAIL";
            ON_ADD_EMAIL.Size = new Size(167, 52);
            ON_ADD_EMAIL.TabIndex = 11;
            ON_ADD_EMAIL.Text = "Write New";
            ON_ADD_EMAIL.UseVisualStyleBackColor = true;
            ON_ADD_EMAIL.Click += ON_ADD_EMAIL_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(196, 115);
            checkBox1.MaximumSize = new Size(100, 0);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 12;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // ON_ACCOUNT
            // 
            ON_ACCOUNT.Location = new Point(2313, 34);
            ON_ACCOUNT.Name = "ON_ACCOUNT";
            ON_ACCOUNT.Size = new Size(238, 70);
            ON_ACCOUNT.TabIndex = 14;
            ON_ACCOUNT.Text = "Account";
            ON_ACCOUNT.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(2489, 43);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(52, 50);
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // FILTER_BTN
            // 
            FILTER_BTN.Location = new Point(432, 111);
            FILTER_BTN.Name = "FILTER_BTN";
            FILTER_BTN.Size = new Size(105, 23);
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
            EMAILS_LIST.Location = new Point(196, 139);
            EMAILS_LIST.Name = "EMAILS_LIST";
            EMAILS_LIST.Size = new Size(2355, 814);
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
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2560, 973);
            Controls.Add(EMAILS_LIST);
            Controls.Add(FILTER_BTN);
            Controls.Add(pictureBox1);
            Controls.Add(ON_ACCOUNT);
            Controls.Add(checkBox1);
            Controls.Add(ON_ADD_EMAIL);
            Controls.Add(CMB_SORT);
            Controls.Add(SEARCH_BOX);
            Controls.Add(CATEGORIES_LIST);
            Name = "EmailListDialog";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += EmailProvider_Load;
            Controls.SetChildIndex(CATEGORIES_LIST, 0);
            Controls.SetChildIndex(SEARCH_BOX, 0);
            Controls.SetChildIndex(CMB_SORT, 0);
            Controls.SetChildIndex(ON_ADD_EMAIL, 0);
            Controls.SetChildIndex(checkBox1, 0);
            Controls.SetChildIndex(ON_ACCOUNT, 0);
            Controls.SetChildIndex(pictureBox1, 0);
            Controls.SetChildIndex(FILTER_BTN, 0);
            Controls.SetChildIndex(EMAILS_LIST, 0);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView CATEGORIES_LIST;
        private TextBox SEARCH_BOX;
        private ComboBox CMB_SORT;
        private Button ON_ADD_EMAIL;
        private CheckBox checkBox1;
        private Button ON_ACCOUNT;
        private PictureBox pictureBox1;
        private Button FILTER_BTN;
        private ListView EMAILS_LIST;
        private ColumnHeader DATE_HEADER;
        private ColumnHeader SUBJECT_HEADER;
        private ColumnHeader CONTENT_HEADER;
    }
}
