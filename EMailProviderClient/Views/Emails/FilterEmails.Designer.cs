﻿namespace EMailProviderClient.Views.Emails
{
    partial class FilterEmails
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
            Filter = new Label();
            BY_DATE_GRP = new GroupBox();
            BY_DATE_CHB = new CheckBox();
            BY_RECEIVER_GRP = new GroupBox();
            EMAIL_LABEL = new Label();
            RECEIVER_EMAIL = new TextBox();
            BY_RECEIVER_CHB = new CheckBox();
            CANCEL = new Button();
            APPLY = new Button();
            END_DATE_LABEL = new Label();
            BEGIN_DATE_LABEL = new Label();
            DATE_TO = new DateTimePicker();
            DATE_FROM = new DateTimePicker();
            BY_DATE_GRP.SuspendLayout();
            BY_RECEIVER_GRP.SuspendLayout();
            SuspendLayout();
            // 
            // Filter
            // 
            Filter.Font = new Font("Segoe UI", 24F, FontStyle.Underline, GraphicsUnit.Point, 0);
            Filter.Location = new Point(111, 9);
            Filter.Name = "Filter";
            Filter.Size = new Size(129, 56);
            Filter.TabIndex = 0;
            Filter.Text = "Filter";
            // 
            // BY_DATE_GRP
            // 
            BY_DATE_GRP.Controls.Add(END_DATE_LABEL);
            BY_DATE_GRP.Controls.Add(BEGIN_DATE_LABEL);
            BY_DATE_GRP.Controls.Add(DATE_TO);
            BY_DATE_GRP.Controls.Add(DATE_FROM);
            BY_DATE_GRP.Location = new Point(33, 68);
            BY_DATE_GRP.Name = "BY_DATE_GRP";
            BY_DATE_GRP.Size = new Size(283, 99);
            BY_DATE_GRP.TabIndex = 1;
            BY_DATE_GRP.TabStop = false;
            BY_DATE_GRP.Text = "By Date";
            // 
            // BY_DATE_CHB
            // 
            BY_DATE_CHB.AutoSize = true;
            BY_DATE_CHB.Location = new Point(12, 113);
            BY_DATE_CHB.Name = "BY_DATE_CHB";
            BY_DATE_CHB.Size = new Size(15, 14);
            BY_DATE_CHB.TabIndex = 2;
            BY_DATE_CHB.UseVisualStyleBackColor = true;
            BY_DATE_CHB.CheckedChanged += BY_DATE_CHB_CheckedChanged;
            // 
            // BY_RECEIVER_GRP
            // 
            BY_RECEIVER_GRP.Controls.Add(EMAIL_LABEL);
            BY_RECEIVER_GRP.Controls.Add(RECEIVER_EMAIL);
            BY_RECEIVER_GRP.Location = new Point(33, 173);
            BY_RECEIVER_GRP.Name = "BY_RECEIVER_GRP";
            BY_RECEIVER_GRP.Size = new Size(283, 60);
            BY_RECEIVER_GRP.TabIndex = 3;
            BY_RECEIVER_GRP.TabStop = false;
            BY_RECEIVER_GRP.Text = "By Receiver";
            // 
            // EMAIL_LABEL
            // 
            EMAIL_LABEL.AutoSize = true;
            EMAIL_LABEL.Location = new Point(11, 25);
            EMAIL_LABEL.Name = "EMAIL_LABEL";
            EMAIL_LABEL.Size = new Size(36, 15);
            EMAIL_LABEL.TabIndex = 1;
            EMAIL_LABEL.Text = "Email";
            // 
            // RECEIVER_EMAIL
            // 
            RECEIVER_EMAIL.Location = new Point(70, 22);
            RECEIVER_EMAIL.Name = "RECEIVER_EMAIL";
            RECEIVER_EMAIL.Size = new Size(200, 23);
            RECEIVER_EMAIL.TabIndex = 0;
            // 
            // BY_RECEIVER_CHB
            // 
            BY_RECEIVER_CHB.AutoSize = true;
            BY_RECEIVER_CHB.Location = new Point(12, 198);
            BY_RECEIVER_CHB.Name = "BY_RECEIVER_CHB";
            BY_RECEIVER_CHB.Size = new Size(15, 14);
            BY_RECEIVER_CHB.TabIndex = 4;
            BY_RECEIVER_CHB.UseVisualStyleBackColor = true;
            BY_RECEIVER_CHB.CheckedChanged += BY_RECEIVER_CHB_CheckedChanged;
            // 
            // CANCEL
            // 
            CANCEL.Location = new Point(241, 245);
            CANCEL.Name = "CANCEL";
            CANCEL.Size = new Size(75, 23);
            CANCEL.TabIndex = 5;
            CANCEL.Text = "Cancel";
            CANCEL.UseVisualStyleBackColor = true;
            CANCEL.Click += CANCEL_Click;
            // 
            // APPLY
            // 
            APPLY.Location = new Point(160, 245);
            APPLY.Name = "APPLY";
            APPLY.Size = new Size(75, 23);
            APPLY.TabIndex = 6;
            APPLY.Text = "Apply";
            APPLY.UseVisualStyleBackColor = true;
            APPLY.Click += APPLY_Click;
            // 
            // END_DATE_LABEL
            // 
            END_DATE_LABEL.AutoSize = true;
            END_DATE_LABEL.Location = new Point(6, 64);
            END_DATE_LABEL.Name = "END_DATE_LABEL";
            END_DATE_LABEL.Size = new Size(54, 15);
            END_DATE_LABEL.TabIndex = 3;
            END_DATE_LABEL.Text = "End Date";
            // 
            // BEGIN_DATE_LABEL
            // 
            BEGIN_DATE_LABEL.AutoSize = true;
            BEGIN_DATE_LABEL.Location = new Point(6, 26);
            BEGIN_DATE_LABEL.Name = "BEGIN_DATE_LABEL";
            BEGIN_DATE_LABEL.Size = new Size(64, 15);
            BEGIN_DATE_LABEL.TabIndex = 2;
            BEGIN_DATE_LABEL.Text = "Begin Date";
            // 
            // DATE_TO
            // 
            DATE_TO.Location = new Point(70, 60);
            DATE_TO.Name = "DATE_TO";
            DATE_TO.ShowCheckBox = true;
            DATE_TO.Size = new Size(200, 23);
            DATE_TO.TabIndex = 1;
            // 
            // DATE_FROM
            // 
            DATE_FROM.Location = new Point(70, 22);
            DATE_FROM.Name = "DATE_FROM";
            DATE_FROM.ShowCheckBox = true;
            DATE_FROM.Size = new Size(200, 23);
            DATE_FROM.TabIndex = 0;
            // 
            // FilterEmails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(328, 280);
            Controls.Add(APPLY);
            Controls.Add(CANCEL);
            Controls.Add(BY_RECEIVER_CHB);
            Controls.Add(BY_RECEIVER_GRP);
            Controls.Add(BY_DATE_CHB);
            Controls.Add(BY_DATE_GRP);
            Controls.Add(Filter);
            Name = "FilterEmails";
            Text = "FilterEmails";
            BY_DATE_GRP.ResumeLayout(false);
            BY_DATE_GRP.PerformLayout();
            BY_RECEIVER_GRP.ResumeLayout(false);
            BY_RECEIVER_GRP.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Filter;
        private GroupBox BY_DATE_GRP;
        private CheckBox BY_DATE_CHB;
        private GroupBox BY_RECEIVER_GRP;
        private Label EMAIL_LABEL;
        private TextBox RECEIVER_EMAIL;
        private CheckBox BY_RECEIVER_CHB;
        private Button CANCEL;
        private Button APPLY;
        private Label END_DATE_LABEL;
        private Label BEGIN_DATE_LABEL;
        private DateTimePicker DATE_TO;
        private DateTimePicker DATE_FROM;
    }
}