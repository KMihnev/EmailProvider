using WindowsFormsCore;

namespace EMailProviderClient.Views.User
{
    partial class SetupProfile
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
            EDC_NAME = new TextBox();
            EDC_PHONE_NUMBER = new TextBox();
            CMB_COUNTRY = new ComboBox();
            STT_SETUP_PROFILE = new Label();
            PB_PROFILE = new PictureBox();
            BTN_UPLOAD_PICTURE = new SmartButton();
            BTN_CONTINUE = new SmartButton();
            BTN_SKIP = new SmartButton();
            ((System.ComponentModel.ISupportInitialize)PB_PROFILE).BeginInit();
            SuspendLayout();
            // 
            // EDC_NAME
            // 
            EDC_NAME.Location = new Point(51, 217);
            EDC_NAME.Name = "EDC_NAME";
            EDC_NAME.PlaceholderText = "Name";
            EDC_NAME.Size = new Size(168, 23);
            EDC_NAME.TabIndex = 2;
            // 
            // EDC_PHONE_NUMBER
            // 
            EDC_PHONE_NUMBER.Location = new Point(51, 275);
            EDC_PHONE_NUMBER.Name = "EDC_PHONE_NUMBER";
            EDC_PHONE_NUMBER.PlaceholderText = "Phone Number";
            EDC_PHONE_NUMBER.Size = new Size(168, 23);
            EDC_PHONE_NUMBER.TabIndex = 4;
            // 
            // CMB_COUNTRY
            // 
            CMB_COUNTRY.FormattingEnabled = true;
            CMB_COUNTRY.Location = new Point(51, 246);
            CMB_COUNTRY.Name = "CMB_COUNTRY";
            CMB_COUNTRY.Size = new Size(168, 23);
            CMB_COUNTRY.TabIndex = 3;
            CMB_COUNTRY.Text = "Country";
            CMB_COUNTRY.SelectedIndexChanged += ON_COUNTRIES_CHANGE;
            // 
            // STT_SETUP_PROFILE
            // 
            STT_SETUP_PROFILE.AutoSize = true;
            STT_SETUP_PROFILE.Font = new Font("Verdana", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            STT_SETUP_PROFILE.Location = new Point(34, 22);
            STT_SETUP_PROFILE.Name = "STT_SETUP_PROFILE";
            STT_SETUP_PROFILE.Size = new Size(212, 35);
            STT_SETUP_PROFILE.TabIndex = 0;
            STT_SETUP_PROFILE.Text = "Set up profile";
            // 
            // PB_PROFILE
            // 
            PB_PROFILE.Location = new Point(82, 76);
            PB_PROFILE.Name = "PB_PROFILE";
            PB_PROFILE.Size = new Size(109, 104);
            PB_PROFILE.TabIndex = 4;
            PB_PROFILE.TabStop = false;
            // 
            // BTN_UPLOAD_PICTURE
            // 
            BTN_UPLOAD_PICTURE.Location = new Point(77, 188);
            BTN_UPLOAD_PICTURE.Name = "BTN_UPLOAD_PICTURE";
            BTN_UPLOAD_PICTURE.Size = new Size(121, 23);
            BTN_UPLOAD_PICTURE.TabIndex = 1;
            BTN_UPLOAD_PICTURE.Text = "Profile Picture";
            BTN_UPLOAD_PICTURE.UseVisualStyleBackColor = true;
            BTN_UPLOAD_PICTURE.Click += BTN_UPLOAD_PICTURE_Click;
            // 
            // BTN_CONTINUE
            // 
            BTN_CONTINUE.Location = new Point(144, 320);
            BTN_CONTINUE.Name = "BTN_CONTINUE";
            BTN_CONTINUE.Size = new Size(75, 23);
            BTN_CONTINUE.TabIndex = 6;
            BTN_CONTINUE.Text = "Continue";
            BTN_CONTINUE.UseVisualStyleBackColor = true;
            BTN_CONTINUE.Click += BTN_CONTINUE_Click;
            // 
            // BTN_SKIP
            // 
            BTN_SKIP.Location = new Point(51, 320);
            BTN_SKIP.Name = "BTN_SKIP";
            BTN_SKIP.Size = new Size(75, 23);
            BTN_SKIP.TabIndex = 5;
            BTN_SKIP.Text = "Skip";
            BTN_SKIP.UseVisualStyleBackColor = true;
            BTN_SKIP.Click += BTN_SKIP_Click;
            // 
            // SetupProfile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(277, 355);
            Controls.Add(BTN_SKIP);
            Controls.Add(BTN_CONTINUE);
            Controls.Add(BTN_UPLOAD_PICTURE);
            Controls.Add(PB_PROFILE);
            Controls.Add(STT_SETUP_PROFILE);
            Controls.Add(CMB_COUNTRY);
            Controls.Add(EDC_PHONE_NUMBER);
            Controls.Add(EDC_NAME);
            Name = "SetupProfile";
            Text = "SetupProfile";
            Load += SetupProfile_Load;
            ((System.ComponentModel.ISupportInitialize)PB_PROFILE).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox EDC_NAME;
        private TextBox EDC_PHONE_NUMBER;
        private ComboBox CMB_COUNTRY;
        private Label STT_SETUP_PROFILE;
        private PictureBox PB_PROFILE;
        private SmartButton BTN_UPLOAD_PICTURE;
        private SmartButton BTN_CONTINUE;
        private SmartButton BTN_SKIP;
    }
}