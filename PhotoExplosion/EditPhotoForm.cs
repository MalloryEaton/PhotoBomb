using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PhotoExplosion
{
    public partial class EditPhotoForm : Form
    {
        public string imagePath { private get; set; }
        private int imageWidth;
        private int imageHeight;
        private enum Transformation { Invert, ChangeColor, ChangeBrightness};
        private Transformation selectedTransformation { get; set; }
        private Bitmap myBitmap;

        public EditPhotoForm()
        {
            InitializeComponent();

            ImageToEdit.Image = Image.FromFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures) + @"\soup.jpg");
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
            
        }

        private void EditPhotoForm_Resize(object sender, EventArgs e)
        {
            ImageToEdit.Size = new Size(Width - 40, Height - 200);
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

        private void ChangeColor()
        {
            Color color = colorDialog.Color;
            //for (int y = 0; y < imageWidth; y++)
            //{
            //    for (int x = 0; x < imageWidth; x++)
            //    {
            //        Color color = ImageToEdit.Image.GetPixel(x, y);
            //        int newRed = Math.Abs(color.R - 255);
            //        int newGreen = Math.Abs(color.G - 255);
            //        int newBlue = Math.Abs(color.B - 255);
            //        Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
            //        ImageToEdit.Image.SetPixel(x, y, newColor);
            //    }
            //}
        }

        private void InvertColorsButton_Click(object sender, EventArgs e)
        {
            selectedTransformation = Transformation.Invert;
            if (!backgroundWorker.IsBusy)
            {
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
            selectedTransformation = Transformation.ChangeBrightness;
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void ChangeBrightness()
        {
            //for (int y = 0; y < transformedBitmap.Height; y++)
            //{
            //    for (int x = 0; x < transformedBitmap.Width; x++)
            //    {
            //        Color color = transformedBitmap.GetPixel(x, y);
            //        int newRed = Math.Abs(color.R - 255);
            //        int newGreen = Math.Abs(color.G - 255);
            //        int newBlue = Math.Abs(color.B - 255);
            //        Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
            //        transformedBitmap.SetPixel(x, y, newColor);
            //    }
            //}
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
                    InvertColors();
                }
                else if(selectedTransformation == Transformation.ChangeColor)
                {
                    ChangeColor();
                }
                else if(selectedTransformation == Transformation.ChangeBrightness)
                {
                    ChangeBrightness();
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