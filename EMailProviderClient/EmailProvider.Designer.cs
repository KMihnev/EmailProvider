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
            menuStrip1 = new MenuStrip();
            изходToolStripMenuItem = new ToolStripMenuItem();
            minimizeToolStripMenuItem = new ToolStripMenuItem();
            затовриИИзлезToolStripMenuItem = new ToolStripMenuItem();
            затвориToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1 = new ContextMenuStrip(components);
            SEARCH_BOX = new TextBox();
            comboBox1 = new ComboBox();
            DATE_PICK_FROM = new DateTimePicker();
            DATE_PICK_TO = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            CMB_SORT = new ComboBox();
            ON_ADD_EMAIL = new Button();
            checkBox1 = new CheckBox();
            EMAILS_LIST = new CheckedListBox();
            ON_ACCOUNT = new Button();
            pictureBox1 = new PictureBox();
            menuStrip1.SuspendLayout();
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
            SEARCH_BOX.Location = new Point(342, 110);
            SEARCH_BOX.Name = "SEARCH_BOX";
            SEARCH_BOX.Size = new Size(197, 23);
            SEARCH_BOX.TabIndex = 4;
            SEARCH_BOX.Text = "Key word";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(215, 110);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 5;
            comboBox1.Text = "Search by";
            // 
            // DATE_PICK_FROM
            // 
            DATE_PICK_FROM.Location = new Point(589, 110);
            DATE_PICK_FROM.Name = "DATE_PICK_FROM";
            DATE_PICK_FROM.Size = new Size(200, 23);
            DATE_PICK_FROM.TabIndex = 6;
            // 
            // DATE_PICK_TO
            // 
            DATE_PICK_TO.Location = new Point(823, 110);
            DATE_PICK_TO.Name = "DATE_PICK_TO";
            DATE_PICK_TO.Size = new Size(200, 23);
            DATE_PICK_TO.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(548, 114);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 8;
            label1.Text = "From:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(795, 114);
            label2.Name = "label2";
            label2.Size = new Size(23, 15);
            label2.TabIndex = 9;
            label2.Text = "To:";
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
            checkBox1.Location = new Point(195, 115);
            checkBox1.MaximumSize = new Size(100, 0);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 12;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // EMAILS_LIST
            // 
            EMAILS_LIST.FormattingEnabled = true;
            EMAILS_LIST.Location = new Point(193, 139);
            EMAILS_LIST.Name = "EMAILS_LIST";
            EMAILS_LIST.Size = new Size(2358, 814);
            EMAILS_LIST.TabIndex = 13;
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
            // EmailProvider
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2560, 973);
            Controls.Add(pictureBox1);
            Controls.Add(ON_ACCOUNT);
            Controls.Add(EMAILS_LIST);
            Controls.Add(checkBox1);
            Controls.Add(ON_ADD_EMAIL);
            Controls.Add(CMB_SORT);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DATE_PICK_TO);
            Controls.Add(DATE_PICK_FROM);
            Controls.Add(comboBox1);
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
        private ComboBox comboBox1;
        private DateTimePicker DATE_PICK_FROM;
        private DateTimePicker DATE_PICK_TO;
        private Label label1;
        private Label label2;
        private ComboBox CMB_SORT;
        private Button ON_ADD_EMAIL;
        private CheckBox checkBox1;
        private CheckedListBox EMAILS_LIST;
        private Button ON_ACCOUNT;
        private PictureBox pictureBox1;
    }
}
