namespace QRCoder_zpkd1_link
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxModels = new System.Windows.Forms.TextBox();
            this.radioButton_TypeWatchFace = new System.Windows.Forms.RadioButton();
            this.radioButton_TypeApp = new System.Windows.Forms.RadioButton();
            this.radioButton_TypeNone = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(50, 318);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(240, 20);
            this.textBoxUrl.TabIndex = 0;
            this.textBoxUrl.TextChanged += new System.EventHandler(this.textBoxUrl_TextChanged);
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Image = global::QRCoder_zpkd1_link.Properties.Resources.logo_qr;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxQRCode.TabIndex = 1;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(92, 422);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(140, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save QR code";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(50, 344);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(140, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 347);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 399);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Models";
            // 
            // textBoxModels
            // 
            this.textBoxModels.Location = new System.Drawing.Point(50, 396);
            this.textBoxModels.Name = "textBoxModels";
            this.textBoxModels.Size = new System.Drawing.Size(262, 20);
            this.textBoxModels.TabIndex = 8;
            // 
            // radioButton_TypeWatchFace
            // 
            this.radioButton_TypeWatchFace.AutoSize = true;
            this.radioButton_TypeWatchFace.Location = new System.Drawing.Point(50, 373);
            this.radioButton_TypeWatchFace.Name = "radioButton_TypeWatchFace";
            this.radioButton_TypeWatchFace.Size = new System.Drawing.Size(81, 17);
            this.radioButton_TypeWatchFace.TabIndex = 10;
            this.radioButton_TypeWatchFace.Text = "Watch face";
            this.radioButton_TypeWatchFace.UseVisualStyleBackColor = true;
            // 
            // radioButton_TypeApp
            // 
            this.radioButton_TypeApp.AutoSize = true;
            this.radioButton_TypeApp.Location = new System.Drawing.Point(146, 373);
            this.radioButton_TypeApp.Name = "radioButton_TypeApp";
            this.radioButton_TypeApp.Size = new System.Drawing.Size(44, 17);
            this.radioButton_TypeApp.TabIndex = 11;
            this.radioButton_TypeApp.Text = "App";
            this.radioButton_TypeApp.UseVisualStyleBackColor = true;
            // 
            // radioButton_TypeNone
            // 
            this.radioButton_TypeNone.AutoSize = true;
            this.radioButton_TypeNone.Checked = true;
            this.radioButton_TypeNone.Location = new System.Drawing.Point(261, 373);
            this.radioButton_TypeNone.Name = "radioButton_TypeNone";
            this.radioButton_TypeNone.Size = new System.Drawing.Size(51, 17);
            this.radioButton_TypeNone.TabIndex = 12;
            this.radioButton_TypeNone.TabStop = true;
            this.radioButton_TypeNone.Text = "None";
            this.radioButton_TypeNone.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Version";
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(234, 344);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(78, 20);
            this.textBoxVersion.TabIndex = 13;
            // 
            // labelStatus
            // 
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(292, 318);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(20, 20);
            this.labelStatus.TabIndex = 15;
            this.labelStatus.Text = "❌";
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F);
            this.label_version.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_version.Location = new System.Drawing.Point(295, 438);
            this.label_version.Margin = new System.Windows.Forms.Padding(0);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(20, 7);
            this.label_version.TabIndex = 109;
            this.label_version.Text = "v 0.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 451);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxVersion);
            this.Controls.Add(this.radioButton_TypeNone);
            this.Controls.Add(this.radioButton_TypeApp);
            this.Controls.Add(this.radioButton_TypeWatchFace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxModels);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.pictureBoxQRCode);
            this.Controls.Add(this.textBoxUrl);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::QRCoder_zpkd1_link.Properties.Settings.Default, "FormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::QRCoder_zpkd1_link.Properties.Settings.Default.FormLocation;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "QRCoder zpkd1-link";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxModels;
        private System.Windows.Forms.RadioButton radioButton_TypeWatchFace;
        private System.Windows.Forms.RadioButton radioButton_TypeApp;
        private System.Windows.Forms.RadioButton radioButton_TypeNone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label_version;
    }
}

