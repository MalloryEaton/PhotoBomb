namespace PhotoExplosion
{
    partial class TransformationProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformationProgressForm));
            this.CancelTransformationButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TransformationProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // CancelTransformationButton
            // 
            this.CancelTransformationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelTransformationButton.Location = new System.Drawing.Point(102, 139);
            this.CancelTransformationButton.Name = "CancelTransformationButton";
            this.CancelTransformationButton.Size = new System.Drawing.Size(75, 23);
            this.CancelTransformationButton.TabIndex = 0;
            this.CancelTransformationButton.Text = "Cancel";
            this.CancelTransformationButton.UseVisualStyleBackColor = true;
            this.CancelTransformationButton.Click += new System.EventHandler(this.CancelTransformationButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please wait...";
            // 
            // TransformationProgressBar
            // 
            this.TransformationProgressBar.Location = new System.Drawing.Point(48, 69);
            this.TransformationProgressBar.Name = "TransformationProgressBar";
            this.TransformationProgressBar.Size = new System.Drawing.Size(185, 35);
            this.TransformationProgressBar.TabIndex = 2;
            // 
            // TransformationProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 181);
            this.ControlBox = false;
            this.Controls.Add(this.TransformationProgressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelTransformationButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransformationProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transforming";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelTransformationButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar TransformationProgressBar;
    }
}