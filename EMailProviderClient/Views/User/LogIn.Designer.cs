﻿using WindowsFormsCore;

namespace EMailProviderClient.Views.User
{
    partial class LogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            STT_Login = new Label();
            EDC_NAME = new TextBox();
            EDC_PASSWORD = new TextBox();
            STT_SUGGESTION = new Label();
            BTN_LOGIN = new SmartButton();
            BTN_CANCEL = new SmartButton();
            SuspendLayout();
            // 
            // STT_Login
            // 
            STT_Login.AutoSize = true;
            STT_Login.Font = new Font("Verdana", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            STT_Login.Location = new Point(69, 34);
            STT_Login.Name = "STT_Login";
            STT_Login.Size = new Size(100, 32);
            STT_Login.TabIndex = 0;
            STT_Login.Text = "Log In";
            // 
            // EDC_NAME
            // 
            EDC_NAME.Location = new Point(34, 90);
            EDC_NAME.Name = "EDC_NAME";
            EDC_NAME.PlaceholderText = "Email / Username";
            EDC_NAME.Size = new Size(180, 23);
            EDC_NAME.TabIndex = 1;
            // 
            // EDC_PASSWORD
            // 
            EDC_PASSWORD.Location = new Point(34, 123);
            EDC_PASSWORD.Name = "EDC_PASSWORD";
            EDC_PASSWORD.PasswordChar = '*';
            EDC_PASSWORD.PlaceholderText = "Password";
            EDC_PASSWORD.Size = new Size(180, 23);
            EDC_PASSWORD.TabIndex = 2;
            // 
            // STT_SUGGESTION
            // 
            STT_SUGGESTION.AutoSize = true;
            STT_SUGGESTION.ForeColor = SystemColors.Highlight;
            STT_SUGGESTION.Location = new Point(37, 151);
            STT_SUGGESTION.Name = "STT_SUGGESTION";
            STT_SUGGESTION.Size = new Size(176, 15);
            STT_SUGGESTION.TabIndex = 3;
            STT_SUGGESTION.Text = "Dont have an account? Register!";
            STT_SUGGESTION.Click += STT_SUGGESTION_Click;
            // 
            // BTN_LOGIN
            // 
            BTN_LOGIN.BackColor = Color.DodgerBlue;
            BTN_LOGIN.FlatStyle = FlatStyle.Flat;
            BTN_LOGIN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            BTN_LOGIN.ForeColor = Color.White;
            BTN_LOGIN.Location = new Point(138, 186);
            BTN_LOGIN.Name = "BTN_LOGIN";
            BTN_LOGIN.Size = new Size(75, 22);
            BTN_LOGIN.TabIndex = 5;
            BTN_LOGIN.Text = "Log In";
            BTN_LOGIN.UseVisualStyleBackColor = true;
            BTN_LOGIN.Click += BTN_LOGIN_Click;
            // 
            // BTN_CANCEL
            // 
            BTN_CANCEL.BackColor = Color.DodgerBlue;
            BTN_CANCEL.FlatStyle = FlatStyle.Flat;
            BTN_CANCEL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            BTN_CANCEL.ForeColor = Color.White;
            BTN_CANCEL.Location = new Point(34, 185);
            BTN_CANCEL.Name = "BTN_CANCEL";
            BTN_CANCEL.Size = new Size(75, 23);
            BTN_CANCEL.TabIndex = 4;
            BTN_CANCEL.Text = "Cancel";
            BTN_CANCEL.UseVisualStyleBackColor = true;
            BTN_CANCEL.Click += BTN_CANCEL_Click;
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(249, 221);
            Controls.Add(BTN_CANCEL);
            Controls.Add(BTN_LOGIN);
            Controls.Add(STT_SUGGESTION);
            Controls.Add(EDC_PASSWORD);
            Controls.Add(EDC_NAME);
            Controls.Add(STT_Login);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogIn";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "LogIn";
            Load += LogIn_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label STT_Login;
        private TextBox EDC_NAME;
        private TextBox EDC_PASSWORD;
        private Label STT_SUGGESTION;
        private SmartButton BTN_LOGIN;
        private SmartButton BTN_CANCEL;
    }
}
