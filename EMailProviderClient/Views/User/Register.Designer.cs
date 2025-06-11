using WindowsFormsCore;

namespace EMailProviderClient.Views.User
{
    partial class Register
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
            EDC_EMAIL = new TextBox();
            EDC_PASSWORD = new TextBox();
            EDC_RE_PASSWORD = new TextBox();
            StaticRegister = new Label();
            BTN_REGISTER = new SmartButton();
            BTN_CANCEL = new SmartButton();
            STT_GO_TO_LOGIN = new Label();
            SuspendLayout();
            // 
            // EDC_EMAIL
            // 
            EDC_EMAIL.Location = new Point(45, 84);
            EDC_EMAIL.Name = "EDC_EMAIL";
            EDC_EMAIL.PlaceholderText = "Email";
            EDC_EMAIL.Size = new Size(190, 23);
            EDC_EMAIL.TabIndex = 1;
            // 
            // EDC_PASSWORD
            // 
            EDC_PASSWORD.Location = new Point(45, 113);
            EDC_PASSWORD.Name = "EDC_PASSWORD";
            EDC_PASSWORD.PasswordChar = '*';
            EDC_PASSWORD.PlaceholderText = "Password";
            EDC_PASSWORD.Size = new Size(190, 23);
            EDC_PASSWORD.TabIndex = 2;
            // 
            // EDC_RE_PASSWORD
            // 
            EDC_RE_PASSWORD.Location = new Point(45, 142);
            EDC_RE_PASSWORD.Name = "EDC_RE_PASSWORD";
            EDC_RE_PASSWORD.PasswordChar = '*';
            EDC_RE_PASSWORD.PlaceholderText = "Repeat Password";
            EDC_RE_PASSWORD.Size = new Size(190, 23);
            EDC_RE_PASSWORD.TabIndex = 3;
            // 
            // StaticRegister
            // 
            StaticRegister.AutoSize = true;
            StaticRegister.Font = new Font("Verdana", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StaticRegister.Location = new Point(74, 27);
            StaticRegister.Name = "StaticRegister";
            StaticRegister.Size = new Size(133, 35);
            StaticRegister.TabIndex = 0;
            StaticRegister.Text = "Register";
            // 
            // BTN_REGISTER
            // 
            BTN_REGISTER.BackColor = Color.DodgerBlue;
            BTN_REGISTER.FlatStyle = FlatStyle.Flat;
            BTN_REGISTER.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            BTN_REGISTER.ForeColor = Color.White;
            BTN_REGISTER.Location = new Point(160, 209);
            BTN_REGISTER.Name = "BTN_REGISTER";
            BTN_REGISTER.Size = new Size(75, 23);
            BTN_REGISTER.TabIndex = 6;
            BTN_REGISTER.Text = "Register";
            BTN_REGISTER.UseVisualStyleBackColor = true;
            BTN_REGISTER.Click += BTN_REGISTER_Click;
            // 
            // BTN_CANCEL
            // 
            BTN_CANCEL.BackColor = Color.DodgerBlue;
            BTN_CANCEL.FlatStyle = FlatStyle.Flat;
            BTN_CANCEL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            BTN_CANCEL.ForeColor = Color.White;
            BTN_CANCEL.Location = new Point(45, 209);
            BTN_CANCEL.Name = "BTN_CANCEL";
            BTN_CANCEL.Size = new Size(75, 23);
            BTN_CANCEL.TabIndex = 5;
            BTN_CANCEL.Text = "Cancel";
            BTN_CANCEL.UseVisualStyleBackColor = true;
            BTN_CANCEL.Click += BTN_CANCEL_Click;
            // 
            // STT_GO_TO_LOGIN
            // 
            STT_GO_TO_LOGIN.AutoSize = true;
            STT_GO_TO_LOGIN.ForeColor = SystemColors.HotTrack;
            STT_GO_TO_LOGIN.Location = new Point(59, 173);
            STT_GO_TO_LOGIN.Name = "STT_GO_TO_LOGIN";
            STT_GO_TO_LOGIN.Size = new Size(162, 15);
            STT_GO_TO_LOGIN.TabIndex = 4;
            STT_GO_TO_LOGIN.Text = "Already have a profile? LogIn!";
            STT_GO_TO_LOGIN.Click += STT_GO_TO_LOGIN_Click;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 248);
            Controls.Add(STT_GO_TO_LOGIN);
            Controls.Add(BTN_CANCEL);
            Controls.Add(BTN_REGISTER);
            Controls.Add(StaticRegister);
            Controls.Add(EDC_RE_PASSWORD);
            Controls.Add(EDC_PASSWORD);
            Controls.Add(EDC_EMAIL);
            Name = "Register";
            Text = "Register";
            Load += Register_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox EDC_EMAIL;
        private TextBox EDC_PASSWORD;
        private TextBox EDC_RE_PASSWORD;
        private Label StaticRegister;
        private SmartButton BTN_REGISTER;
        private SmartButton BTN_CANCEL;
        private Label STT_GO_TO_LOGIN;
    }
}