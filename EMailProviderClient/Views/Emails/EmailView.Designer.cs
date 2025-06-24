using WindowsFormsCore;

namespace EMailProviderClient.Views.Emails
{
    partial class EMAIL_VIEW
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SEND_BTN = new SmartButton();
            CLOSE_BTN = new SmartButton();
            CONTENT_BOX = new RichTextBox();
            SUBJECT_EDIT = new TextBox();
            CONTENT_LABEL = new Label();
            SUBJECT_LABEL = new Label();
            RECEIVER_EDIT = new TextBox();
            HEADER = new Label();
            RECEIVER_LABEL = new Label();
            FILES_LIST = new ListView();
            FILES_LABEL = new Label();
            UPLOAD_BTN = new SmartButton();
            FILES_CONTEXT = new ContextMenuStrip(components);
            downloadToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            SuspendLayout();
            // 
            // SEND_BTN
            // 
            SEND_BTN.BackColor = Color.DodgerBlue;
            SEND_BTN.FlatStyle = FlatStyle.Flat;
            SEND_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            SEND_BTN.ForeColor = Color.White;
            SEND_BTN.Location = new Point(788, 785);
            SEND_BTN.Name = "SEND_BTN";
            SEND_BTN.Size = new Size(122, 57);
            SEND_BTN.TabIndex = 11;
            SEND_BTN.Text = "Send";
            SEND_BTN.UseVisualStyleBackColor = true;
            SEND_BTN.Click += SEND_BTN_Click;
            // 
            // CLOSE_BTN
            // 
            CLOSE_BTN.BackColor = Color.DodgerBlue;
            CLOSE_BTN.FlatStyle = FlatStyle.Flat;
            CLOSE_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            CLOSE_BTN.ForeColor = Color.White;
            CLOSE_BTN.Location = new Point(916, 785);
            CLOSE_BTN.Name = "CLOSE_BTN";
            CLOSE_BTN.Size = new Size(122, 57);
            CLOSE_BTN.TabIndex = 10;
            CLOSE_BTN.Text = "Close";
            CLOSE_BTN.UseVisualStyleBackColor = true;
            CLOSE_BTN.Click += CLOSE_BTN_Click;
            // 
            // CONTENT_BOX
            // 
            CONTENT_BOX.Location = new Point(12, 175);
            CONTENT_BOX.Name = "CONTENT_BOX";
            CONTENT_BOX.Size = new Size(1026, 397);
            CONTENT_BOX.TabIndex = 6;
            CONTENT_BOX.Text = "";
            // 
            // SUBJECT_EDIT
            // 
            SUBJECT_EDIT.Location = new Point(12, 125);
            SUBJECT_EDIT.Name = "SUBJECT_EDIT";
            SUBJECT_EDIT.Size = new Size(1026, 25);
            SUBJECT_EDIT.TabIndex = 4;
            // 
            // CONTENT_LABEL
            // 
            CONTENT_LABEL.AutoSize = true;
            CONTENT_LABEL.Location = new Point(12, 153);
            CONTENT_LABEL.Name = "CONTENT_LABEL";
            CONTENT_LABEL.Size = new Size(59, 19);
            CONTENT_LABEL.TabIndex = 5;
            CONTENT_LABEL.Text = "Content";
            // 
            // SUBJECT_LABEL
            // 
            SUBJECT_LABEL.AutoSize = true;
            SUBJECT_LABEL.Location = new Point(12, 104);
            SUBJECT_LABEL.Name = "SUBJECT_LABEL";
            SUBJECT_LABEL.Size = new Size(53, 19);
            SUBJECT_LABEL.TabIndex = 3;
            SUBJECT_LABEL.Text = "Subject";
            // 
            // RECEIVER_EDIT
            // 
            RECEIVER_EDIT.Location = new Point(12, 76);
            RECEIVER_EDIT.Name = "RECEIVER_EDIT";
            RECEIVER_EDIT.Size = new Size(1026, 25);
            RECEIVER_EDIT.TabIndex = 2;
            // 
            // HEADER
            // 
            HEADER.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            HEADER.Location = new Point(12, 10);
            HEADER.Name = "HEADER";
            HEADER.Size = new Size(257, 44);
            HEADER.TabIndex = 0;
            HEADER.Text = "New Email";
            // 
            // RECEIVER_LABEL
            // 
            RECEIVER_LABEL.AutoSize = true;
            RECEIVER_LABEL.Location = new Point(12, 54);
            RECEIVER_LABEL.Name = "RECEIVER_LABEL";
            RECEIVER_LABEL.Size = new Size(59, 19);
            RECEIVER_LABEL.TabIndex = 1;
            RECEIVER_LABEL.Text = "Receiver";
            // 
            // FILES_LIST
            // 
            FILES_LIST.Location = new Point(12, 605);
            FILES_LIST.Name = "FILES_LIST";
            FILES_LIST.Size = new Size(1026, 109);
            FILES_LIST.TabIndex = 8;
            FILES_LIST.UseCompatibleStateImageBehavior = false;
            // 
            // FILES_LABEL
            // 
            FILES_LABEL.AutoSize = true;
            FILES_LABEL.Location = new Point(12, 585);
            FILES_LABEL.Name = "FILES_LABEL";
            FILES_LABEL.Size = new Size(35, 19);
            FILES_LABEL.TabIndex = 7;
            FILES_LABEL.Text = "Files";
            // 
            // UPLOAD_BTN
            // 
            UPLOAD_BTN.BackColor = Color.DodgerBlue;
            UPLOAD_BTN.FlatStyle = FlatStyle.Flat;
            UPLOAD_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            UPLOAD_BTN.ForeColor = Color.White;
            UPLOAD_BTN.Location = new Point(963, 722);
            UPLOAD_BTN.Name = "UPLOAD_BTN";
            UPLOAD_BTN.Size = new Size(75, 26);
            UPLOAD_BTN.TabIndex = 9;
            UPLOAD_BTN.Text = "Upload";
            UPLOAD_BTN.UseVisualStyleBackColor = true;
            UPLOAD_BTN.Click += UPLOAD_BTN_Click;
            // 
            // FILES_CONTEXT
            // 
            FILES_CONTEXT.Name = "FILES_CONTEXT";
            FILES_CONTEXT.Size = new Size(61, 4);
            // 
            // downloadToolStripMenuItem
            // 
            downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            downloadToolStripMenuItem.Size = new Size(180, 22);
            downloadToolStripMenuItem.Text = "Download";
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(180, 22);
            removeToolStripMenuItem.Text = "Remove";
            // 
            // EMAIL_VIEW
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 856);
            Controls.Add(UPLOAD_BTN);
            Controls.Add(FILES_LABEL);
            Controls.Add(FILES_LIST);
            Controls.Add(RECEIVER_LABEL);
            Controls.Add(HEADER);
            Controls.Add(RECEIVER_EDIT);
            Controls.Add(SUBJECT_LABEL);
            Controls.Add(CONTENT_LABEL);
            Controls.Add(SUBJECT_EDIT);
            Controls.Add(CONTENT_BOX);
            Controls.Add(CLOSE_BTN);
            Controls.Add(SEND_BTN);
            Name = "EMAIL_VIEW";
            Text = "Email";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SmartButton SEND_BTN;
        private SmartButton CLOSE_BTN;
        private RichTextBox CONTENT_BOX;
        private TextBox SUBJECT_EDIT;
        private Label CONTENT_LABEL;
        private Label SUBJECT_LABEL;
        private TextBox RECEIVER_EDIT;
        private Label HEADER;
        private Label RECEIVER_LABEL;
        private ListView FILES_LIST;
        private Label FILES_LABEL;
        private SmartButton UPLOAD_BTN;
        private ContextMenuStrip FILES_CONTEXT;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
    }
}