namespace PhotoExplosion
{
    partial class EditPhotoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPhotoForm));
            this.ControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BrightnessSlider = new System.Windows.Forms.TrackBar();
            this.ColorButton = new System.Windows.Forms.Button();
            this.InvertColorsButton = new System.Windows.Forms.Button();
            this.EditCancelButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ImageToEdit = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ControlsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // ControlsGroupBox
            // 
            this.ControlsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ControlsGroupBox.AutoSize = true;
            this.ControlsGroupBox.Controls.Add(this.label3);
            this.ControlsGroupBox.Controls.Add(this.label2);
            this.ControlsGroupBox.Controls.Add(this.label1);
            this.ControlsGroupBox.Controls.Add(this.BrightnessSlider);
            this.ControlsGroupBox.Controls.Add(this.ColorButton);
            this.ControlsGroupBox.Controls.Add(this.InvertColorsButton);
            this.ControlsGroupBox.Location = new System.Drawing.Point(12, 472);
            this.ControlsGroupBox.Name = "ControlsGroupBox";
            this.ControlsGroupBox.Size = new System.Drawing.Size(1010, 100);
            this.ControlsGroupBox.TabIndex = 0;
            this.ControlsGroupBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Dark";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Light";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brightness";
            // 
            // BrightnessSlider
            // 
            this.BrightnessSlider.Location = new System.Drawing.Point(6, 36);
            this.BrightnessSlider.Maximum = 100;
            this.BrightnessSlider.Name = "BrightnessSlider";
            this.BrightnessSlider.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BrightnessSlider.Size = new System.Drawing.Size(248, 45);
            this.BrightnessSlider.TabIndex = 2;
            this.BrightnessSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.BrightnessSlider.Value = 50;
            this.BrightnessSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BrightnessSlider_MouseUp);
            // 
            // ColorButton
            // 
            this.ColorButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ColorButton.Location = new System.Drawing.Point(273, 36);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(75, 23);
            this.ColorButton.TabIndex = 1;
            this.ColorButton.Text = "Color...";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // InvertColorsButton
            // 
            this.InvertColorsButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.InvertColorsButton.Location = new System.Drawing.Point(372, 36);
            this.InvertColorsButton.Name = "InvertColorsButton";
            this.InvertColorsButton.Size = new System.Drawing.Size(75, 23);
            this.InvertColorsButton.TabIndex = 0;
            this.InvertColorsButton.Text = "Invert";
            this.InvertColorsButton.UseVisualStyleBackColor = true;
            this.InvertColorsButton.Click += new System.EventHandler(this.InvertColorsButton_Click);
            // 
            // EditCancelButton
            // 
            this.EditCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.EditCancelButton.Location = new System.Drawing.Point(947, 582);
            this.EditCancelButton.Name = "EditCancelButton";
            this.EditCancelButton.Size = new System.Drawing.Size(75, 23);
            this.EditCancelButton.TabIndex = 1;
            this.EditCancelButton.Text = "Cancel";
            this.EditCancelButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(849, 582);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ImageToEdit
            // 
            this.ImageToEdit.Location = new System.Drawing.Point(12, 12);
            this.ImageToEdit.Name = "ImageToEdit";
            this.ImageToEdit.Size = new System.Drawing.Size(1010, 454);
            this.ImageToEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageToEdit.TabIndex = 3;
            this.ImageToEdit.TabStop = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // EditPhotoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 617);
            this.Controls.Add(this.ImageToEdit);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.EditCancelButton);
            this.Controls.Add(this.ControlsGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "EditPhotoForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Photo";
            this.ResizeEnd += new System.EventHandler(this.EditPhotoForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.EditPhotoForm_Resize);
            this.ControlsGroupBox.ResumeLayout(false);
            this.ControlsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ControlsGroupBox;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.Button InvertColorsButton;
        private System.Windows.Forms.Button EditCancelButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar BrightnessSlider;
        private System.Windows.Forms.PictureBox ImageToEdit;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}