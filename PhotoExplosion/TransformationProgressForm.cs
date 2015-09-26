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
    public partial class TransformationProgressForm : Form
    {
        public int ProgressValue
        {
            set { TransformationProgressBar.Value = value; }
        }
        public event EventHandler<EventArgs> Canceled;

        public TransformationProgressForm()
        {
            InitializeComponent();
        }

        private void CancelTransformationButton_Click(object sender, EventArgs e)
        {
            // Create a copy of the event to work with
            EventHandler<EventArgs> ea = Canceled;
            /* If there are no subscribers, eh will be null so we need to check
             * to avoid a NullReferenceException. */
            if (ea != null)
                ea(this, e);
        }

        private void TransformationProgressForm_Load(object sender, EventArgs e)
        {
            //http://stackoverflow.com/a/13463841/5086965
            //This is to make the dialog be in the center of the parent
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }
    }
}
