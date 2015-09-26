using System;
using System.ComponentModel;
using System.Drawing;
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
        private FormWindowState LastWindowState = FormWindowState.Minimized;

        public EditPhotoForm()
        {
            InitializeComponent();
        }

        public void SetPhotoInfo(string imagePath)
        {
            ImageToEdit.Image = Image.FromFile(imagePath);
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
            if (WindowState == LastWindowState)
            {
                ImageToEdit.Size = new Size(Width - 40, Height - 200);
            }
            else
            {
                LastWindowState = WindowState;
            }
            
            ControlsGroupBox.Size = new Size(Width - 40, ControlsGroupBox.Height);
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            selectedTransformation = Transformation.ChangeColor;
            DialogResult result = colorDialog.ShowDialog();
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void ChangeColor(BackgroundWorker worker)
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
                worker.ReportProgress(0);
            }
        }

        private void InvertColorsButton_Click(object sender, EventArgs e)
        {
            selectedTransformation = Transformation.Invert;
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void InvertColors(BackgroundWorker worker)
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
            selectedTransformation = Transformation.ChangeBrightness;
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void ChangeBrightness(BackgroundWorker worker)
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

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            if (worker.CancellationPending)
            {
                // Inform the UI thread that we quit early
                // because we were told to cancel
                e.Cancel = true;
            }
            else
            {
                if(selectedTransformation == Transformation.Invert)
                {
                    InvertColors(worker);
                }
                else if(selectedTransformation == Transformation.ChangeColor)
                {
                    ChangeColor(worker);
                }
                else if(selectedTransformation == Transformation.ChangeBrightness)
                {
                    ChangeBrightness(worker);
                }
                

                // Report progress % back to the UI thread
                //worker.ReportProgress(10);
            }

            // Report my answer back to the UI thread
            //e.Result = 6;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // In BW.WorkerCompleted method:
            //Close the dialog box
            //Enable the edit form and bring it to the front
            //If the BW wasn’t cancelled then pictureBox’s image = transformedBitmap
            if (!e.Cancelled)
            {
                ImageToEdit.Image = myBitmap;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //myImage.Save("myphoto.jpg", ImageFormat.Jpeg);
        }
    }
}