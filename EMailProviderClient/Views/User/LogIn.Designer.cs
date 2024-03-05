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
            BTN_LOGIN = new Button();
            BTN_CANCEL = new Button();
            SuspendLayout();
            // 
            // STT_Login
            // 
            STT_Login.AutoSize = true;
            STT_Login.Font = new Font("Verdana", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            STT_Login.Location = new Point(56, 34);
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
            EDC_NAME.Size = new Size(142, 23);
            EDC_NAME.TabIndex = 3;
            // 
            // EDC_PASSWORD
            // 
            EDC_PASSWORD.Location = new Point(34, 123);
            EDC_PASSWORD.Name = "EDC_PASSWORD";
            EDC_PASSWORD.PlaceholderText = "Password";
            EDC_PASSWORD.Size = new Size(142, 23);
            EDC_PASSWORD.TabIndex = 4;
            // 
            // STT_SUGGESTION
            // 
            STT_SUGGESTION.AutoSize = true;
            STT_SUGGESTION.ForeColor = SystemColors.Highlight;
            STT_SUGGESTION.Location = new Point(18, 151);
            STT_SUGGESTION.Name = "STT_SUGGESTION";
            STT_SUGGESTION.Size = new Size(176, 15);
            STT_SUGGESTION.TabIndex = 5;
            STT_SUGGESTION.Text = "Dont have an account? Register!";
            STT_SUGGESTION.Click += STT_SUGGESTION_Click;
            // 
            // BTN_LOGIN
            // 
            BTN_LOGIN.Location = new Point(119, 197);
            BTN_LOGIN.Name = "BTN_LOGIN";
            BTN_LOGIN.Size = new Size(75, 23);
            BTN_LOGIN.TabIndex = 6;
            BTN_LOGIN.Text = "Log In";
            BTN_LOGIN.UseVisualStyleBackColor = true;
            // 
            // BTN_CANCEL
            // 
            BTN_CANCEL.Location = new Point(20, 197);
            BTN_CANCEL.Name = "BTN_CANCEL";
            BTN_CANCEL.Size = new Size(75, 23);
            BTN_CANCEL.TabIndex = 7;
            BTN_CANCEL.Text = "Cancel";
            BTN_CANCEL.UseVisualStyleBackColor = true;
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(213, 233);
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
        private Button BTN_LOGIN;
        private Button BTN_CANCEL;
    }
}
