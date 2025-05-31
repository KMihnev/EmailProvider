namespace EMailProviderClient
{
    partial class EmailProvider
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
            components = new System.ComponentModel.Container();
            CATEGORIES_LIST = new ListView();
            SYSTEM_FOLDERS_HEADER = new ColumnHeader();
            menuStrip1 = new MenuStrip();
            изходToolStripMenuItem = new ToolStripMenuItem();
            minimizeToolStripMenuItem = new ToolStripMenuItem();
            затовриИИзлезToolStripMenuItem = new ToolStripMenuItem();
            затвориToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1 = new ContextMenuStrip(components);
            SEARCH_BOX = new TextBox();
            CMB_SORT = new ComboBox();
            ON_ADD_EMAIL = new Button();
            checkBox1 = new CheckBox();
            ON_ACCOUNT = new Button();
            pictureBox1 = new PictureBox();
            FILTER_BTN = new Button();
            EMAILS_LIST = new ListView();
            contextMenuStrip2 = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            DATE_HEADER = new ColumnHeader();
            SUBJECT_HEADER = new ColumnHeader();
            CONTENT_HEADER = new ColumnHeader();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // CATEGORIES_LIST
            // 
            CATEGORIES_LIST.Columns.AddRange(new ColumnHeader[] { SYSTEM_FOLDERS_HEADER });
            CATEGORIES_LIST.Location = new Point(12, 139);
            CATEGORIES_LIST.Name = "CATEGORIES_LIST";
            CATEGORIES_LIST.Size = new Size(167, 814);
            CATEGORIES_LIST.TabIndex = 0;
            CATEGORIES_LIST.UseCompatibleStateImageBehavior = false;
            CATEGORIES_LIST.View = View.Details;
            // 
            // SYSTEM_FOLDERS_HEADER
            // 
            SYSTEM_FOLDERS_HEADER.Text = "Folders";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { изходToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(2560, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // изходToolStripMenuItem
            // 
            изходToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { minimizeToolStripMenuItem, затовриИИзлезToolStripMenuItem, затвориToolStripMenuItem });
            изходToolStripMenuItem.Name = "изходToolStripMenuItem";
            изходToolStripMenuItem.Size = new Size(37, 20);
            изходToolStripMenuItem.Text = "Exit";
            // 
            // minimizeToolStripMenuItem
            // 
            minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            minimizeToolStripMenuItem.Size = new Size(147, 22);
            minimizeToolStripMenuItem.Text = "Minimize";
            // 
            // затовриИИзлезToolStripMenuItem
            // 
            затовриИИзлезToolStripMenuItem.Name = "затовриИИзлезToolStripMenuItem";
            затовриИИзлезToolStripMenuItem.Size = new Size(147, 22);
            затовриИИзлезToolStripMenuItem.Text = "Close";
            // 
            // затвориToolStripMenuItem
            // 
            затвориToolStripMenuItem.Name = "затвориToolStripMenuItem";
            затвориToolStripMenuItem.Size = new Size(147, 22);
            затвориToolStripMenuItem.Text = "Close and exit";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
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
            EMAILS_LIST.ContextMenuStrip = contextMenuStrip2;
            EMAILS_LIST.FullRowSelect = true;
            EMAILS_LIST.GridLines = true;
            EMAILS_LIST.Location = new Point(196, 139);
            EMAILS_LIST.Name = "EMAILS_LIST";
            EMAILS_LIST.Size = new Size(2355, 814);
            EMAILS_LIST.TabIndex = 17;
            EMAILS_LIST.UseCompatibleStateImageBehavior = false;
            EMAILS_LIST.View = View.Details;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(107, 22);
            deleteToolStripMenuItem.Text = "Delete";
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
            // EmailProvider
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
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "EmailProvider";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += EmailProvider_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView CATEGORIES_LIST;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem изходToolStripMenuItem;
        private ToolStripMenuItem затовриИИзлезToolStripMenuItem;
        private ToolStripMenuItem затвориToolStripMenuItem;
        private ToolStripMenuItem minimizeToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
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
        private ColumnHeader SYSTEM_FOLDERS_HEADER;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem deleteToolStripMenuItem;
    }
}
