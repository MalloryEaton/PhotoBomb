using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoExplosion
{
    public partial class MainForm : Form
    {
        private static string currentUser = Environment.UserName;
        private string currentDirectory = @"C:\Users\" + currentUser + @"\Pictures";

        public MainForm()
        {
            InitializeComponent();
            ListDirectory(treeView, currentDirectory);
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();

            //ImageList imageList = new ImageList();
            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                
                
                /*foreach (var file in directoryInfo.GetFiles())
                    imageList.Images.Add(new Bitmap(file.GetType(), 32x32));
                */
            }
            
            if (!photoLoaderBW.IsBusy)
                photoLoaderBW.RunWorkerAsync();

            treeView.Nodes.Add(node);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void locateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
        }

        private void selectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PhotoLoaderBW_DoWork(object sender, DoWorkEventArgs e)
        {
            string directory = currentDirectory.Substring(3);
            DirectoryInfo homeDir = new DirectoryInfo(currentDirectory);

            ImageList smallimageList = new ImageList();
            smallimageList.ImageSize = new Size(45,40);
            ImageList largeimageList = new ImageList();
            largeimageList.ImageSize = new Size(90, 80);

            SetImageList(smallimageList, largeimageList);

            foreach (FileInfo file in homeDir.GetFiles())
            {
                try
                {
                    if (file.Extension.ToLower() == ".jpg")
                    {
                        byte[] bytes = File.ReadAllBytes(file.FullName);
                        MemoryStream ms = new MemoryStream(bytes);
                        Image img = Image.FromStream(ms);
                        AddToPhotoListView(file.Name, file.FullName, smallimageList, largeimageList);
                    }
                }
                catch
                {
                    Console.WriteLine("This is not an image file");
                }
            }
        }

        private void AddToPhotoListView(string imgName, string imgPath, ImageList smallimageList, ImageList largeimageList)
        {
            // If this function was invoked by DoWork() then the thread
            // cannot change the textbox's text, but the UI thread will
            // be in control when called via Invoke()
            if (InvokeRequired)
            {
                // Use a lambda exp to create a Delegate that calls
                // AddToTextBox on the UI thread
                Invoke(new MethodInvoker(() => AddToPhotoListView(imgName, imgPath, smallimageList, largeimageList)));
            }
            else
            {
                //Add image to the image lists
                smallimageList.Images.Add(Image.FromFile(imgPath));
                largeimageList.Images.Add(Image.FromFile(imgPath));
                
                //create an item with image name and the image (imageIndex is the pointer to the image in the image list)
                ListViewItem item = new ListViewItem(imgName);
                int index = smallimageList.Images.Count - 1;
                item.ImageIndex = index;
                photoList.Items.Add(item);
            }
        }

        private void SetImageList(ImageList smallimageList, ImageList largeimageList)
        {
            if (InvokeRequired)
            {
                // Use a lambda exp to create a Delegate that calls
                // AddToTextBox on the UI thread
                Invoke(new MethodInvoker(() => SetImageList(smallimageList, largeimageList)));
            }
            else
            {
                photoList.SmallImageList = smallimageList;
                photoList.LargeImageList = largeimageList;
            }
        }

        private void PhotoLoaderBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void PhotoLoaderBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) { }
        }
    }

    /*if (backgroundWorker1.WorkerSupportsCancellation)
                {
                    backgroundWorker1.CancelAsync();
                }
   */
}
 