namespace EMailProviderClient.Views.Emails
{
    partial class AddEmail
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
            SEND_BTN = new Button();
            CLOSE_BTN = new Button();
            CONTENT_BOX = new RichTextBox();
            SUBJECT_EDIT = new TextBox();
            CONTENT_LABEL = new Label();
            SUBJECT_LABEL = new Label();
            RECEIVER_EDIT = new TextBox();
            HEADER = new Label();
            RECEIVER_LABEL = new Label();
            FILES_LIST = new ListView();
            FILES_LABEL = new Label();
            UPLOAD_BTN = new Button();
            SuspendLayout();
            // 
            // SEND_BTN
            // 
            SEND_BTN.Location = new Point(916, 693);
            SEND_BTN.Name = "SEND_BTN";
            SEND_BTN.Size = new Size(122, 50);
            SEND_BTN.TabIndex = 0;
            SEND_BTN.Text = "Send";
            SEND_BTN.UseVisualStyleBackColor = true;
            SEND_BTN.Click += SEND_BTN_Click;
            // 
            // CLOSE_BTN
            // 
            CLOSE_BTN.Location = new Point(788, 693);
            CLOSE_BTN.Name = "CLOSE_BTN";
            CLOSE_BTN.Size = new Size(122, 50);
            CLOSE_BTN.TabIndex = 1;
            CLOSE_BTN.Text = "Close";
            CLOSE_BTN.UseVisualStyleBackColor = true;
            CLOSE_BTN.Click += CLOSE_BTN_Click;
            // 
            // CONTENT_BOX
            // 
            CONTENT_BOX.Location = new Point(12, 154);
            CONTENT_BOX.Name = "CONTENT_BOX";
            CONTENT_BOX.Size = new Size(1026, 351);
            CONTENT_BOX.TabIndex = 2;
            CONTENT_BOX.Text = "";
            // 
            // SUBJECT_EDIT
            // 
            SUBJECT_EDIT.Location = new Point(12, 110);
            SUBJECT_EDIT.Name = "SUBJECT_EDIT";
            SUBJECT_EDIT.Size = new Size(1026, 23);
            SUBJECT_EDIT.TabIndex = 3;
            // 
            // CONTENT_LABEL
            // 
            CONTENT_LABEL.AutoSize = true;
            CONTENT_LABEL.Location = new Point(12, 135);
            CONTENT_LABEL.Name = "CONTENT_LABEL";
            CONTENT_LABEL.Size = new Size(50, 15);
            CONTENT_LABEL.TabIndex = 4;
            CONTENT_LABEL.Text = "Content";
            // 
            // SUBJECT_LABEL
            // 
            SUBJECT_LABEL.AutoSize = true;
            SUBJECT_LABEL.Location = new Point(12, 92);
            SUBJECT_LABEL.Name = "SUBJECT_LABEL";
            SUBJECT_LABEL.Size = new Size(46, 15);
            SUBJECT_LABEL.TabIndex = 5;
            SUBJECT_LABEL.Text = "Subject";
            // 
            // RECEIVER_EDIT
            // 
            RECEIVER_EDIT.Location = new Point(12, 67);
            RECEIVER_EDIT.Name = "RECEIVER_EDIT";
            RECEIVER_EDIT.Size = new Size(1026, 23);
            RECEIVER_EDIT.TabIndex = 6;
            // 
            // HEADER
            // 
            HEADER.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            HEADER.Location = new Point(12, 9);
            HEADER.Name = "HEADER";
            HEADER.Size = new Size(140, 39);
            HEADER.TabIndex = 7;
            HEADER.Text = "New Email";
            // 
            // RECEIVER_LABEL
            // 
            RECEIVER_LABEL.AutoSize = true;
            RECEIVER_LABEL.Location = new Point(12, 48);
            RECEIVER_LABEL.Name = "RECEIVER_LABEL";
            RECEIVER_LABEL.Size = new Size(51, 15);
            RECEIVER_LABEL.TabIndex = 8;
            RECEIVER_LABEL.Text = "Receiver";
            // 
            // FILES_LIST
            // 
            FILES_LIST.Location = new Point(12, 534);
            FILES_LIST.Name = "FILES_LIST";
            FILES_LIST.Size = new Size(1026, 97);
            FILES_LIST.TabIndex = 9;
            FILES_LIST.UseCompatibleStateImageBehavior = false;
            // 
            // FILES_LABEL
            // 
            FILES_LABEL.AutoSize = true;
            FILES_LABEL.Location = new Point(12, 516);
            FILES_LABEL.Name = "FILES_LABEL";
            FILES_LABEL.Size = new Size(30, 15);
            FILES_LABEL.TabIndex = 10;
            FILES_LABEL.Text = "Files";
            // 
            // UPLOAD_BTN
            // 
            UPLOAD_BTN.Location = new Point(963, 637);
            UPLOAD_BTN.Name = "UPLOAD_BTN";
            UPLOAD_BTN.Size = new Size(75, 23);
            UPLOAD_BTN.TabIndex = 11;
            UPLOAD_BTN.Text = "Upload";
            UPLOAD_BTN.UseVisualStyleBackColor = true;
            // 
            // AddEmail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 755);
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
            Name = "AddEmail";
            Text = "AddEmail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SEND_BTN;
        private Button CLOSE_BTN;
        private RichTextBox CONTENT_BOX;
        private TextBox SUBJECT_EDIT;
        private Label CONTENT_LABEL;
        private Label SUBJECT_LABEL;
        private TextBox RECEIVER_EDIT;
        private Label HEADER;
        private Label RECEIVER_LABEL;
        private ListView FILES_LIST;
        private Label FILES_LABEL;
        private Button UPLOAD_BTN;
    }
}