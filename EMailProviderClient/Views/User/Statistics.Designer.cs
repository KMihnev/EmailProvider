namespace EMailProviderClient.Views.User
{
    partial class Statistics
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
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            button3 = new Button();
            label3 = new Label();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(16, 41);
            button1.Name = "button1";
            button1.Size = new Size(173, 102);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(211, 42);
            button2.Name = "button2";
            button2.Size = new Size(173, 102);
            button2.TabIndex = 1;
            button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 20);
            label1.Name = "label1";
            label1.Size = new Size(111, 19);
            label1.TabIndex = 2;
            label1.Text = "Number of users";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(207, 20);
            label2.Name = "label2";
            label2.Size = new Size(177, 19);
            label2.TabIndex = 3;
            label2.Text = "Number of outgoing emails";
            // 
            // button3
            // 
            button3.Location = new Point(405, 42);
            button3.Name = "button3";
            button3.Size = new Size(173, 102);
            button3.TabIndex = 4;
            button3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(401, 20);
            label3.Name = "label3";
            label3.Size = new Size(177, 19);
            label3.TabIndex = 5;
            label3.Text = "Number of incoming emails";
            // 
            // button4
            // 
            button4.Location = new Point(503, 167);
            button4.Name = "button4";
            button4.Size = new Size(75, 26);
            button4.TabIndex = 6;
            button4.Text = "Close";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Statistics
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(593, 202);
            Controls.Add(button4);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Statistics";
            Text = "Statistics";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Button button3;
        private Label label3;
        private Button button4;
    }
}