using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoExplosion
{
    public partial class EditPhotoForm : Form
    {
        public string imagePath { private get; set; }

        public EditPhotoForm()
        {
            InitializeComponent();
            ImageToEdit.Image = Image.FromFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures) + @"\Albania_pasture.jpg");
            if(ImageToEdit.Image.Size.Width > 460 && ImageToEdit.Image.Size.Height > 460)
            {
                Size = new Size(ImageToEdit.Image.Size.Width + 40, ImageToEdit.Image.Size.Height + 200);
            }
            else
            {
                Size = new Size(500, 500);
            }
            
        }

        private void EditPhotoForm_Resize(object sender, EventArgs e)
        {
            ImageToEdit.Size = new Size(Width - 40, Height - 200);
            ControlsGroupBox.Size = new Size(Width - 40, ControlsGroupBox.Height);
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //myImage.Save("myohoto.jpg", ImageFormat.Jpeg);
        }

        private void InvertColors()
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
    }
}