namespace EMailProviderClient.Views.User
{
    partial class StartUp
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
            StartUp_LogIn = new Button();
            StartUp_Register = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // StartUp_LogIn
            // 
            StartUp_LogIn.Location = new Point(76, 116);
            StartUp_LogIn.Name = "StartUp_LogIn";
            StartUp_LogIn.Size = new Size(75, 23);
            StartUp_LogIn.TabIndex = 0;
            StartUp_LogIn.Text = "LogIn";
            StartUp_LogIn.UseVisualStyleBackColor = true;
            StartUp_LogIn.Click += StartUp_LogIn_Click;
            // 
            // StartUp_Register
            // 
            StartUp_Register.Location = new Point(176, 116);
            StartUp_Register.Name = "StartUp_Register";
            StartUp_Register.Size = new Size(75, 23);
            StartUp_Register.TabIndex = 1;
            StartUp_Register.Text = "Register";
            StartUp_Register.UseVisualStyleBackColor = true;
            StartUp_Register.Click += StartUp_Register_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(90, 50);
            label1.Name = "label1";
            label1.Size = new Size(147, 35);
            label1.TabIndex = 2;
            label1.Text = "Welcome";
            // 
            // StartUp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 197);
            Controls.Add(label1);
            Controls.Add(StartUp_Register);
            Controls.Add(StartUp_LogIn);
            Name = "StartUp";
            Text = "StartUp";
            Load += StartUp_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartUp_LogIn;
        private Button StartUp_Register;
        private Label label1;
        private Register register1;
    }
}