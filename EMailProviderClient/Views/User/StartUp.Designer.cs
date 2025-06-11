using WindowsFormsCore;

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
            StartUp_LogIn = new SmartButton();
            StartUp_Register = new SmartButton();
            label1 = new Label();
            SuspendLayout();
            // 
            // StartUp_LogIn
            // 
            StartUp_LogIn.BackColor = Color.DodgerBlue;
            StartUp_LogIn.FlatStyle = FlatStyle.Flat;
            StartUp_LogIn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            StartUp_LogIn.ForeColor = Color.White;
            StartUp_LogIn.Location = new Point(60, 98);
            StartUp_LogIn.Name = "StartUp_LogIn";
            StartUp_LogIn.Size = new Size(75, 30);
            StartUp_LogIn.TabIndex = 1;
            StartUp_LogIn.Text = "LogIn";
            StartUp_LogIn.UseVisualStyleBackColor = true;
            StartUp_LogIn.Click += StartUp_LogIn_Click;
            // 
            // StartUp_Register
            // 
            StartUp_Register.BackColor = Color.DodgerBlue;
            StartUp_Register.FlatStyle = FlatStyle.Flat;
            StartUp_Register.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            StartUp_Register.ForeColor = Color.White;
            StartUp_Register.Location = new Point(160, 98);
            StartUp_Register.Name = "StartUp_Register";
            StartUp_Register.Size = new Size(75, 30);
            StartUp_Register.TabIndex = 2;
            StartUp_Register.Text = "Register";
            StartUp_Register.UseVisualStyleBackColor = true;
            StartUp_Register.Click += StartUp_Register_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(74, 32);
            label1.Name = "label1";
            label1.Size = new Size(147, 35);
            label1.TabIndex = 0;
            label1.Text = "Welcome";
            // 
            // StartUp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(299, 165);
            Controls.Add(label1);
            Controls.Add(StartUp_Register);
            Controls.Add(StartUp_LogIn);
            Name = "StartUp";
            Text = "Welcome";
            Load += StartUp_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SmartButton StartUp_LogIn;
        private SmartButton StartUp_Register;
        private Label label1;
        private Register register1;
    }
}