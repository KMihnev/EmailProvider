using EMailProviderClient.LangSupport;

namespace EMailProviderClient
{
    partial class Container
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
            STRIP = new MenuStrip();
            SuspendLayout();
            // 
            // STRIP
            // 
            STRIP.Location = new Point(0, 0);
            STRIP.Name = "STRIP";
            STRIP.Size = new Size(800, 24);
            STRIP.TabIndex = 0;
            STRIP.Text = "menuStrip1";
            // 
            // Container
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(STRIP);
            MainMenuStrip = STRIP;
            Name = "Container";
            Text = "TyronMail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip STRIP;
    }
}