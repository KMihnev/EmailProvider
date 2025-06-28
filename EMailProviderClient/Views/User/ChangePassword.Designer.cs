using EMailProviderClient.LangSupport;
using WindowsFormsCore;

namespace EMailProviderClient.Views.User
{
    partial class ChangePassword
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
            STT_CHANGE_PASSWORD = new Label();
            OLD_PASSWORD_EDIT = new TextBox();
            NEW_PASSWORD_EDIT = new TextBox();
            REPEAT_PASSWORD_EDIT = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            CLOSE_BTN = new SmartButton();
            button1 = new SmartButton();
            SuspendLayout();
            // 
            // STT_CHANGE_PASSWORD
            // 
            STT_CHANGE_PASSWORD.AutoSize = true;
            STT_CHANGE_PASSWORD.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            STT_CHANGE_PASSWORD.Location = new Point(17, 9);
            STT_CHANGE_PASSWORD.Name = "STT_CHANGE_PASSWORD";
            STT_CHANGE_PASSWORD.Size = new Size(204, 26);
            STT_CHANGE_PASSWORD.TabIndex = 1;
            STT_CHANGE_PASSWORD.Text = DlgLangSupport.ChangePassword;
            // 
            // OLD_PASSWORD_EDIT
            // 
            OLD_PASSWORD_EDIT.Location = new Point(111, 65);
            OLD_PASSWORD_EDIT.Name = "OLD_PASSWORD_EDIT";
            OLD_PASSWORD_EDIT.Size = new Size(120, 23);
            OLD_PASSWORD_EDIT.TabIndex = 2;
            OLD_PASSWORD_EDIT.UseSystemPasswordChar = true;
            // 
            // NEW_PASSWORD_EDIT
            // 
            NEW_PASSWORD_EDIT.Location = new Point(111, 94);
            NEW_PASSWORD_EDIT.Name = "NEW_PASSWORD_EDIT";
            NEW_PASSWORD_EDIT.Size = new Size(120, 23);
            NEW_PASSWORD_EDIT.TabIndex = 3;
            NEW_PASSWORD_EDIT.UseSystemPasswordChar = true;
            // 
            // REPEAT_PASSWORD_EDIT
            // 
            REPEAT_PASSWORD_EDIT.Location = new Point(111, 123);
            REPEAT_PASSWORD_EDIT.Name = "REPEAT_PASSWORD_EDIT";
            REPEAT_PASSWORD_EDIT.Size = new Size(120, 23);
            REPEAT_PASSWORD_EDIT.TabIndex = 4;
            REPEAT_PASSWORD_EDIT.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 68);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 5;
            label1.Text = DlgLangSupport.OldPassWord;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 99);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 6;
            label2.Text = DlgLangSupport.NewPassword;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 127);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 7;
            label3.Text = DlgLangSupport.RepeatPassword;
            // 
            // CLOSE_BTN
            // 
            CLOSE_BTN.BackColor = Color.DodgerBlue;
            CLOSE_BTN.FlatStyle = FlatStyle.Flat;
            CLOSE_BTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            CLOSE_BTN.ForeColor = Color.White;
            CLOSE_BTN.Location = new Point(143, 181);
            CLOSE_BTN.Name = "CLOSE_BTN";
            CLOSE_BTN.Size = new Size(88, 23);
            CLOSE_BTN.TabIndex = 8;
            CLOSE_BTN.Text = DlgLangSupport.Close;
            CLOSE_BTN.UseVisualStyleBackColor = true;
            CLOSE_BTN.Click += CLOSE_BTN_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.DodgerBlue;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(12, 181);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = DlgLangSupport.OK;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ChangePassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(244, 216);
            Controls.Add(button1);
            Controls.Add(CLOSE_BTN);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(REPEAT_PASSWORD_EDIT);
            Controls.Add(NEW_PASSWORD_EDIT);
            Controls.Add(OLD_PASSWORD_EDIT);
            Controls.Add(STT_CHANGE_PASSWORD);
            Name = "ChangePassword";
            Text = DlgLangSupport.ChangePassword;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label STT_CHANGE_PASSWORD;
        private TextBox OLD_PASSWORD_EDIT;
        private TextBox NEW_PASSWORD_EDIT;
        private TextBox REPEAT_PASSWORD_EDIT;
        private Label label1;
        private Label label2;
        private Label label3;
        private SmartButton CLOSE_BTN;
        private SmartButton button1;
    }
}