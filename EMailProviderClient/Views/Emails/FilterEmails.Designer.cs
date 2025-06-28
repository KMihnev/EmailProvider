using EMailProviderClient.LangSupport;
using WindowsFormsCore;

namespace EMailProviderClient.Views.Emails
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
            END_DATE_LABEL = new Label();
            BEGIN_DATE_LABEL = new Label();
            DATE_TO = new DateTimePicker();
            DATE_FROM = new DateTimePicker();
            BY_DATE_CHB = new CheckBox();
            BY_RECEIVER_GRP = new GroupBox();
            EMAIL_LABEL = new Label();
            RECEIVER_EMAIL = new TextBox();
            BY_RECEIVER_CHB = new CheckBox();
            CANCEL = new SmartButton();
            APPLY = new SmartButton();
            CLEAR_BTN = new SmartButton();
            BY_DATE_GRP.SuspendLayout();
            BY_RECEIVER_GRP.SuspendLayout();
            SuspendLayout();
            // 
            // Filter
            // 
            Filter.Font = new Font("Segoe UI", 24F, FontStyle.Underline, GraphicsUnit.Point, 0);
            Filter.Location = new Point(123, 10);
            Filter.Name = "Filter";
            Filter.Size = new Size(129, 63);
            Filter.TabIndex = 0;
            Filter.Text = DlgLangSupport.Filter;
            // 
            // BY_DATE_GRP
            // 
            BY_DATE_GRP.Controls.Add(END_DATE_LABEL);
            BY_DATE_GRP.Controls.Add(BEGIN_DATE_LABEL);
            BY_DATE_GRP.Controls.Add(DATE_TO);
            BY_DATE_GRP.Controls.Add(DATE_FROM);
            BY_DATE_GRP.Location = new Point(33, 77);
            BY_DATE_GRP.Name = "BY_DATE_GRP";
            BY_DATE_GRP.Size = new Size(302, 112);
            BY_DATE_GRP.TabIndex = 2;
            BY_DATE_GRP.TabStop = false;
            BY_DATE_GRP.Text = DlgLangSupport.ByDate;
            // 
            // END_DATE_LABEL
            // 
            END_DATE_LABEL.AutoSize = true;
            END_DATE_LABEL.Location = new Point(6, 73);
            END_DATE_LABEL.Name = "END_DATE_LABEL";
            END_DATE_LABEL.Size = new Size(65, 19);
            END_DATE_LABEL.TabIndex = 2;
            END_DATE_LABEL.Text = DlgLangSupport.EndDate;
            // 
            // BEGIN_DATE_LABEL
            // 
            BEGIN_DATE_LABEL.AutoSize = true;
            BEGIN_DATE_LABEL.Location = new Point(6, 29);
            BEGIN_DATE_LABEL.Name = "BEGIN_DATE_LABEL";
            BEGIN_DATE_LABEL.Size = new Size(76, 19);
            BEGIN_DATE_LABEL.TabIndex = 0;
            BEGIN_DATE_LABEL.Text =DlgLangSupport.BeginDate;
            // 
            // DATE_TO
            // 
            DATE_TO.Location = new Point(87, 68);
            DATE_TO.Name = "DATE_TO";
            DATE_TO.ShowCheckBox = true;
            DATE_TO.Size = new Size(200, 25);
            DATE_TO.TabIndex = 3;
            // 
            // DATE_FROM
            // 
            DATE_FROM.Location = new Point(87, 24);
            DATE_FROM.Name = "DATE_FROM";
            DATE_FROM.ShowCheckBox = true;
            DATE_FROM.Size = new Size(200, 25);
            DATE_FROM.TabIndex = 1;
            // 
            // BY_DATE_CHB
            // 
            BY_DATE_CHB.AutoSize = true;
            BY_DATE_CHB.Location = new Point(12, 128);
            BY_DATE_CHB.Name = "BY_DATE_CHB";
            BY_DATE_CHB.Size = new Size(15, 14);
            BY_DATE_CHB.TabIndex = 1;
            BY_DATE_CHB.UseVisualStyleBackColor = true;
            BY_DATE_CHB.CheckedChanged += BY_DATE_CHB_CheckedChanged;
            // 
            // BY_RECEIVER_GRP
            // 
            BY_RECEIVER_GRP.Controls.Add(EMAIL_LABEL);
            BY_RECEIVER_GRP.Controls.Add(RECEIVER_EMAIL);
            BY_RECEIVER_GRP.Location = new Point(33, 196);
            BY_RECEIVER_GRP.Name = "BY_RECEIVER_GRP";
            BY_RECEIVER_GRP.Size = new Size(302, 68);
            BY_RECEIVER_GRP.TabIndex = 4;
            BY_RECEIVER_GRP.TabStop = false;
            BY_RECEIVER_GRP.Text = DlgLangSupport.ByEmail;
            // 
            // EMAIL_LABEL
            // 
            EMAIL_LABEL.AutoSize = true;
            EMAIL_LABEL.Location = new Point(11, 28);
            EMAIL_LABEL.Name = "EMAIL_LABEL";
            EMAIL_LABEL.Size = new Size(41, 19);
            EMAIL_LABEL.TabIndex = 0;
            EMAIL_LABEL.Text = DlgLangSupport.Email;
            // 
            // RECEIVER_EMAIL
            // 
            RECEIVER_EMAIL.Location = new Point(87, 25);
            RECEIVER_EMAIL.Name = "RECEIVER_EMAIL";
            RECEIVER_EMAIL.Size = new Size(200, 25);
            RECEIVER_EMAIL.TabIndex = 1;
            // 
            // BY_RECEIVER_CHB
            // 
            BY_RECEIVER_CHB.AutoSize = true;
            BY_RECEIVER_CHB.Location = new Point(12, 224);
            BY_RECEIVER_CHB.Name = "BY_RECEIVER_CHB";
            BY_RECEIVER_CHB.Size = new Size(15, 14);
            BY_RECEIVER_CHB.TabIndex = 3;
            BY_RECEIVER_CHB.UseVisualStyleBackColor = true;
            BY_RECEIVER_CHB.CheckedChanged += BY_RECEIVER_CHB_CheckedChanged;
            // 
            // CANCEL
            // 
            CANCEL.BackColor = Color.DodgerBlue;
            CANCEL.FlatStyle = FlatStyle.Flat;
            CANCEL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            CANCEL.ForeColor = Color.White;
            CANCEL.Location = new Point(258, 271);
            CANCEL.Name = "CANCEL";
            CANCEL.Size = new Size(75, 26);
            CANCEL.TabIndex = 7;
            CANCEL.Text = "Cancel";
            CANCEL.UseVisualStyleBackColor = true;
            CANCEL.Click += CANCEL_Click;
            // 
            // APPLY
            // 
            APPLY.BackColor = Color.DodgerBlue;
            APPLY.FlatStyle = FlatStyle.Flat;
            APPLY.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            APPLY.ForeColor = Color.White;
            APPLY.Location = new Point(167, 271);
            APPLY.Name = "APPLY";
            APPLY.Size = new Size(75, 26);
            APPLY.TabIndex = 6;
            APPLY.Text = DlgLangSupport.Apply;
            APPLY.UseVisualStyleBackColor = true;
            APPLY.Click += APPLY_Click;
            // 
            // CLEAR_BTN
            // 
            CLEAR_BTN.BackColor = Color.DodgerBlue;
            CLEAR_BTN.FlatStyle = FlatStyle.Flat;
            CLEAR_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            CLEAR_BTN.ForeColor = Color.White;
            CLEAR_BTN.Location = new Point(33, 271);
            CLEAR_BTN.Name = "CLEAR_BTN";
            CLEAR_BTN.Size = new Size(75, 26);
            CLEAR_BTN.TabIndex = 5;
            CLEAR_BTN.Text = DlgLangSupport.Clear;
            CLEAR_BTN.UseVisualStyleBackColor = true;
            CLEAR_BTN.Click += CLEAR_BTN_Click;
            // 
            // FilterEmails
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 307);
            Controls.Add(CLEAR_BTN);
            Controls.Add(APPLY);
            Controls.Add(CANCEL);
            Controls.Add(BY_RECEIVER_CHB);
            Controls.Add(BY_RECEIVER_GRP);
            Controls.Add(BY_DATE_CHB);
            Controls.Add(BY_DATE_GRP);
            Controls.Add(Filter);
            Name = "FilterEmails";
            Text = DlgLangSupport.ExtendedFilters;
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
        private SmartButton CANCEL;
        private SmartButton APPLY;
        private Label END_DATE_LABEL;
        private Label BEGIN_DATE_LABEL;
        private DateTimePicker DATE_TO;
        private DateTimePicker DATE_FROM;
        private SmartButton CLEAR_BTN;
    }
}