using WindowsFormsCore;

namespace EMailProviderClient.Views.User
{
    partial class UserAccount
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
            pictureBox1 = new PictureBox();
            EMAIL_EDIT = new TextBox();
            NAME_EDIT = new TextBox();
            COUNTRY_CMB = new ComboBox();
            PHONE_NUMBER_EDIT = new TextBox();
            LOG_OUT_BTN = new SmartButton();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SAVE_EDIT_BTN = new SmartButton();
            CHANGE_PASSWORD_BTN = new SmartButton();
            CLOSE_BTN = new SmartButton();
            UPLOAD_BTN = new SmartButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(11, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(181, 176);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // EMAIL_EDIT
            // 
            EMAIL_EDIT.Location = new Point(298, 12);
            EMAIL_EDIT.Name = "EMAIL_EDIT";
            EMAIL_EDIT.Size = new Size(172, 23);
            EMAIL_EDIT.TabIndex = 1;
            // 
            // NAME_EDIT
            // 
            NAME_EDIT.Location = new Point(298, 57);
            NAME_EDIT.Name = "NAME_EDIT";
            NAME_EDIT.Size = new Size(172, 23);
            NAME_EDIT.TabIndex = 2;
            // 
            // COUNTRY_CMB
            // 
            COUNTRY_CMB.DropDownStyle = ComboBoxStyle.DropDownList;
            COUNTRY_CMB.FormattingEnabled = true;
            COUNTRY_CMB.Location = new Point(298, 104);
            COUNTRY_CMB.Name = "COUNTRY_CMB";
            COUNTRY_CMB.Size = new Size(171, 23);
            COUNTRY_CMB.TabIndex = 3;
            COUNTRY_CMB.SelectedIndexChanged += COUNTRY_CMB_SelectedIndexChanged;
            // 
            // PHONE_NUMBER_EDIT
            // 
            PHONE_NUMBER_EDIT.Location = new Point(298, 147);
            PHONE_NUMBER_EDIT.Name = "PHONE_NUMBER_EDIT";
            PHONE_NUMBER_EDIT.Size = new Size(171, 23);
            PHONE_NUMBER_EDIT.TabIndex = 4;
            // 
            // LOG_OUT_BTN
            // 
            LOG_OUT_BTN.BackColor = Color.DodgerBlue;
            LOG_OUT_BTN.FlatStyle = FlatStyle.Flat;
            LOG_OUT_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LOG_OUT_BTN.ForeColor = Color.White;
            LOG_OUT_BTN.Location = new Point(12, 235);
            LOG_OUT_BTN.Name = "LOG_OUT_BTN";
            LOG_OUT_BTN.Size = new Size(180, 25);
            LOG_OUT_BTN.TabIndex = 7;
            LOG_OUT_BTN.Text = "LogOut";
            LOG_OUT_BTN.UseVisualStyleBackColor = true;
            LOG_OUT_BTN.Click += LOG_OUT_BTN_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(200, 15);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 8;
            label1.Text = "Email:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(200, 60);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 9;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(200, 107);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 10;
            label3.Text = "Country:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(199, 150);
            label4.Name = "label4";
            label4.Size = new Size(91, 15);
            label4.TabIndex = 11;
            label4.Text = "Phone Number:";
            // 
            // SAVE_EDIT_BTN
            // 
            SAVE_EDIT_BTN.BackColor = Color.DodgerBlue;
            SAVE_EDIT_BTN.FlatStyle = FlatStyle.Flat;
            SAVE_EDIT_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            SAVE_EDIT_BTN.ForeColor = Color.White;
            SAVE_EDIT_BTN.Location = new Point(297, 235);
            SAVE_EDIT_BTN.Name = "SAVE_EDIT_BTN";
            SAVE_EDIT_BTN.Size = new Size(75, 25);
            SAVE_EDIT_BTN.TabIndex = 13;
            SAVE_EDIT_BTN.Text = "Save";
            SAVE_EDIT_BTN.UseVisualStyleBackColor = true;
            SAVE_EDIT_BTN.Click += SAVE_EDIT_BTN_Click;
            // 
            // CHANGE_PASSWORD_BTN
            // 
            CHANGE_PASSWORD_BTN.BackColor = Color.DodgerBlue;
            CHANGE_PASSWORD_BTN.FlatStyle = FlatStyle.Flat;
            CHANGE_PASSWORD_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            CHANGE_PASSWORD_BTN.ForeColor = Color.White;
            CHANGE_PASSWORD_BTN.Location = new Point(297, 186);
            CHANGE_PASSWORD_BTN.Name = "CHANGE_PASSWORD_BTN";
            CHANGE_PASSWORD_BTN.Size = new Size(171, 25);
            CHANGE_PASSWORD_BTN.TabIndex = 14;
            CHANGE_PASSWORD_BTN.Text = "Change Password";
            CHANGE_PASSWORD_BTN.UseVisualStyleBackColor = true;
            CHANGE_PASSWORD_BTN.Click += CHANGE_PASSWORD_BTN_Click;
            // 
            // CLOSE_BTN
            // 
            CLOSE_BTN.BackColor = Color.DodgerBlue;
            CLOSE_BTN.FlatStyle = FlatStyle.Flat;
            CLOSE_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            CLOSE_BTN.ForeColor = Color.White;
            CLOSE_BTN.Location = new Point(394, 235);
            CLOSE_BTN.Name = "CLOSE_BTN";
            CLOSE_BTN.Size = new Size(75, 25);
            CLOSE_BTN.TabIndex = 12;
            CLOSE_BTN.Text = "Close";
            CLOSE_BTN.UseVisualStyleBackColor = true;
            CLOSE_BTN.Click += CLOSE_BTN_Click;
            // 
            // UPLOAD_BTN
            // 
            UPLOAD_BTN.BackColor = Color.DodgerBlue;
            UPLOAD_BTN.FlatStyle = FlatStyle.Flat;
            UPLOAD_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            UPLOAD_BTN.ForeColor = Color.White;
            UPLOAD_BTN.Location = new Point(11, 187);
            UPLOAD_BTN.Name = "UPLOAD_BTN";
            UPLOAD_BTN.Size = new Size(181, 23);
            UPLOAD_BTN.TabIndex = 15;
            UPLOAD_BTN.Text = "Upload";
            UPLOAD_BTN.UseVisualStyleBackColor = true;
            UPLOAD_BTN.Click += UPLOAD_BTN_Click;
            // 
            // UserAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(481, 272);
            Controls.Add(UPLOAD_BTN);
            Controls.Add(CHANGE_PASSWORD_BTN);
            Controls.Add(SAVE_EDIT_BTN);
            Controls.Add(CLOSE_BTN);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LOG_OUT_BTN);
            Controls.Add(PHONE_NUMBER_EDIT);
            Controls.Add(COUNTRY_CMB);
            Controls.Add(NAME_EDIT);
            Controls.Add(EMAIL_EDIT);
            Controls.Add(pictureBox1);
            Name = "UserAccount";
            Text = "My Account";
            Load += UserAccount_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox EMAIL_EDIT;
        private TextBox NAME_EDIT;
        private ComboBox COUNTRY_CMB;
        private TextBox PHONE_NUMBER_EDIT;
        private SmartButton LOG_OUT_BTN;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private SmartButton SAVE_EDIT_BTN;
        private SmartButton CHANGE_PASSWORD_BTN;
        private SmartButton CLOSE_BTN;
        private Button button5;
        private SmartButton UPLOAD_BTN;
    }
}