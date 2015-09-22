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
        public EditPhotoForm()
        {
            InitializeComponent();
        }

        private void EditPhotoForm_Resize(object sender, EventArgs e)
        {
            Image.Size = new Size(Width - 40, Height - 200);
            ControlsGroupBox.Size = new Size(Width - 40, ControlsGroupBox.Height);
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
        }
    }
}
