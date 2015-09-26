using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PhotoExplosion
{
    public partial class EditPhotoForm : Form
    {
        private int imageWidth;
        private int imageHeight;
        private enum Transformation { Invert, ChangeColor, ChangeBrightness};
        private Transformation selectedTransformation { get; set; }
        private Bitmap myBitmap;
        private string originalImagePath;
        private TransformationProgressForm transformationProgressForm;
        private FormWindowState lastWindowState = FormWindowState.Minimized;

        public EditPhotoForm()
        {
            InitializeComponent();
        }

        public void SetPhotoInfo(string imagePath)
        {
            originalImagePath = imagePath;

            byte[] bytes = File.ReadAllBytes(imagePath);
            MemoryStream ms = new MemoryStream(bytes);
            ImageToEdit.Image = Image.FromStream(ms);

            imageWidth = ImageToEdit.Image.Size.Width;
            imageHeight = ImageToEdit.Image.Size.Height;

            Size = new Size(500, 500);
            if (imageWidth > 460)
            {
                Size = new Size(imageWidth + 40, Size.Height);
            }
            if (imageHeight > 460)
            {
                Size = new Size(Size.Width, imageHeight + 200);
            }

            //if small image, make not resizeable
            if(imageWidth < 460 || imageHeight < 460)
            {
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                ImageToEdit.Size = new Size(imageWidth, imageHeight);
            }
        }

        private void EditPhotoForm_Resize(object sender, EventArgs e)
        {
            // When window state changes
            if (WindowState == lastWindowState)
            {
                ImageToEdit.Size = new Size(Width - 40, Height - 200);
            }
            else
            {
                lastWindowState = WindowState;
            }
            
            ControlsGroupBox.Size = new Size(Width - 40, ControlsGroupBox.Height);
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            selectedTransformation = Transformation.ChangeColor;
            DialogResult result = colorDialog.ShowDialog();
            if(result != DialogResult.Cancel)
            {
                Enabled = false;
                transformationProgressForm = new TransformationProgressForm();
                transformationProgressForm.Canceled += new EventHandler<EventArgs>(CancelTransformationButton_Click);
                transformationProgressForm.Show(this);
                if (!backgroundWorker.IsBusy)
                {
                    backgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void ChangeColor()
        {
            Color colorToAdd = colorDialog.Color;
            myBitmap = new Bitmap(ImageToEdit.Image);
            for (int y = 0; y < imageHeight; y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    Color color = myBitmap.GetPixel(x, y);
                    double percentage = (((color.R + color.G + color.B) / 3.0) / 255.0);
                    double newRed = Math.Abs(colorToAdd.R * percentage);
                    double newGreen = Math.Abs(colorToAdd.G * percentage);
                    double newBlue = Math.Abs(colorToAdd.B * percentage);
                    Color newColor = Color.FromArgb((int)newRed, (int)newGreen, (int)newBlue);
                    myBitmap.SetPixel(x, y, newColor);
                }
            }
        }

        private void InvertColorsButton_Click(object sender, EventArgs e)
        {
            transformationProgressForm = new TransformationProgressForm();
            transformationProgressForm.Canceled += new EventHandler<EventArgs>(CancelTransformationButton_Click);
            transformationProgressForm.StartPosition = FormStartPosition.CenterParent;
            transformationProgressForm.Show(this);
            selectedTransformation = Transformation.Invert;
            if (!backgroundWorker.IsBusy)
            {
                Enabled = false;
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void InvertColors()
        {
            myBitmap = new Bitmap(ImageToEdit.Image);
            for (int y = 0; y < imageHeight; y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    Color color = myBitmap.GetPixel(x, y);
                    int newRed = Math.Abs(color.R - 255);
                    int newGreen = Math.Abs(color.G - 255);
                    int newBlue = Math.Abs(color.B - 255);
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    myBitmap.SetPixel(x, y, newColor);
                }
            }
        }

        private void BrightnessSlider_MouseUp(object sender, MouseEventArgs e)
        {
            transformationProgressForm = new TransformationProgressForm();
            transformationProgressForm.Canceled += new EventHandler<EventArgs>(CancelTransformationButton_Click);
            transformationProgressForm.Show(this);
            selectedTransformation = Transformation.ChangeBrightness;
            if (!backgroundWorker.IsBusy)
            {
                Enabled = false;
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void ChangeBrightness()
        {
            int value = -1;
            int newRed;
            int newGreen;
            int newBlue;
            if (BrightnessSlider.InvokeRequired)
            {
                BrightnessSlider.Invoke(new MethodInvoker(delegate { value = BrightnessSlider.Value; }));
            }
            if(value != -1)
            {
                int amount = Convert.ToInt32(2 * (50 - value) * 0.01 * 255);
                myBitmap = new Bitmap(ImageToEdit.Image);
                for (int y = 0; y < imageHeight; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        Color color = myBitmap.GetPixel(x, y);
                        newRed = color.R - amount;
                        newGreen = color.G - amount;
                        newBlue = color.B - amount;
                        if (newRed > 255)
                        {
                            newRed = 255;
                        }
                        if (newRed < 0)
                        {
                            newRed = 0;
                        }
                        if (newGreen > 255)
                        {
                            newGreen = 255;
                        }
                        if (newGreen < 0)
                        {
                            newGreen = 0;
                        }
                        if (newBlue > 255)
                        {
                            newBlue = 255;
                        }
                        if (newBlue < 0)
                        {
                            newBlue = 0;
                        }
                        Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                        myBitmap.SetPixel(x, y, newColor);
                    }
                }
            }
        }

        private void CancelTransformationButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker.CancelAsync();
                // Close the AlertForm
                transformationProgressForm.Close();
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending)
                {
                    // Inform the UI thread that we quit early
                    // because we were told to cancel
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (selectedTransformation == Transformation.Invert)
                    {
                        InvertColors();
                    }
                    else if (selectedTransformation == Transformation.ChangeColor)
                    {
                        ChangeColor();
                    }
                    else if (selectedTransformation == Transformation.ChangeBrightness)
                    {
                        ChangeBrightness();
                    }
                    
                    worker.ReportProgress(i * 10);
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            transformationProgressForm.ProgressValue = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            transformationProgressForm.Close();
            Enabled = true;
            BringToFront();
            if (!e.Cancelled)
            {
                ImageToEdit.Image = myBitmap;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string path = originalImagePath;
            ImageToEdit.Image.Save(originalImagePath, ImageFormat.Jpeg);
            Close();
        }

        private void EditPhotoForm_ResizeEnd(object sender, EventArgs e)
        {
            //do extra credit here
        }
    }
}