﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PhotoExplosion
{
    public partial class MainForm : Form
    {
        private static string currentUser = Environment.UserName;
        private string currentDirectory = @"C:\Users\" + currentUser + @"\Pictures";
        private string rootDirectory = @"C:\Users\" + currentUser + @"\Pictures";
        //This boolean is used to determine if the root folder was just changed
        private bool rootChanged = false;

        public MainForm()
        {
            InitializeComponent();
            ListDirectory(treeView, true);
            photoProgressBar.Visible = false;
        }

        private void ListDirectory(TreeView treeView, bool loadImages)
        {
            treeView.Nodes.Clear();
            var stack = new Stack<TreeNode>();
            var rootDir = new DirectoryInfo(currentDirectory);
            var node = new TreeNode(rootDir.Name) { Tag = rootDir };
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
            }
            
            if (loadImages && !photoLoaderBW.IsBusy)
                photoLoaderBW.RunWorkerAsync();

            treeView.Nodes.Add(node);
        }

        private void SetUp(ImageList smallimageList, ImageList largeimageList)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => SetUp(smallimageList, largeimageList)));
            else
            {
                photoList.SmallImageList = smallimageList;
                photoList.LargeImageList = largeimageList;
                //Empty the item list in photoList view
                photoList.Items.Clear();
                photoList.Columns.Clear();

                photoList.Columns.Add("Name", 235, HorizontalAlignment.Left);
                photoList.Columns.Add("Date", 150, HorizontalAlignment.Left);
                photoList.Columns.Add("Size", 60, HorizontalAlignment.Left);
            }
        }

        private bool IsClickOnText(TreeView treeView, TreeNode node, Point location)
        {
            var hitTest = treeView.HitTest(location);

            return hitTest.Node == node
                && hitTest.Location == TreeViewHitTestLocations.Label;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void locateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Get the selected image
                ListViewItem item = photoList.SelectedItems[0];
                try
                {
                    Process.Start("explorer.exe", string.Format("/select,\"{0}\"", item.Tag.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Oops! There was a problem trying to locate the image.\n\n Error: " + ex, "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Please select a picture.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void selectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                rootDirectory = folderBrowserDialog.SelectedPath;
                currentDirectory = rootDirectory;
                rootChanged = true;
                //pass false as second parameter so images do not load
                ListDirectory(treeView, false);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!detailToolStripMenuItem.Checked)
            {
                //set list item to display the details view
                photoList.View = View.Details;
                smallToolStripMenuItem.Checked = false;
                largeToolStripMenuItem.Checked = false;
                detailToolStripMenuItem.Checked = true;
            }
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!smallToolStripMenuItem.Checked)
            {
                //set list item to display the small view
                photoList.View = View.SmallIcon;
                smallToolStripMenuItem.Checked = true;
                largeToolStripMenuItem.Checked = false;
                detailToolStripMenuItem.Checked = false;
            }
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!largeToolStripMenuItem.Checked)
            {
                //set list item to display the large view
                photoList.View = View.LargeIcon;
                largeToolStripMenuItem.Checked = true;
                smallToolStripMenuItem.Checked = false;
                detailToolStripMenuItem.Checked = false;
            }
        }

        private void DisplayProgressBar(int max)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => DisplayProgressBar(max)));
            }
            else
            {
                photoProgressBar.Maximum = max;
                photoProgressBar.Minimum = 0;
                photoProgressBar.Visible = true;
                photoProgressBar.Value = 0;
            }
        }

        private void PhotoLoaderBW_DoWork(object sender, DoWorkEventArgs e)
        {
            string directory = currentDirectory.Substring(3);
            DirectoryInfo homeDir = new DirectoryInfo(currentDirectory);

            ImageList smallimageList = new ImageList();
            smallimageList.ImageSize = new Size(43,40);
            ImageList largeimageList = new ImageList();
            largeimageList.ImageSize = new Size(85, 80);

            //Got rid of the listview's flickering issue (see class method below for definition)
            photoList.DoubleBuffered(true);

            //Reset the listview imagelist
            SetUp(smallimageList, largeimageList);
            DisplayProgressBar(homeDir.GetFiles().Length);

            foreach (FileInfo file in homeDir.GetFiles())
            {
                if (photoLoaderBW.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    if (file.Extension.ToLower() == ".jpg")
                    {
                        byte[] bytes = File.ReadAllBytes(file.FullName);
                        MemoryStream ms = new MemoryStream(bytes);
                        Image img = Image.FromStream(ms);
                        AddToPhotoListView(file, img, smallimageList, largeimageList);
                    }
                }
                catch
                {
                    Console.WriteLine("This is not an image file");
                }
                photoLoaderBW.ReportProgress(1);
            }
        }

        private void AddToPhotoListView(FileInfo file, Image img, ImageList smallimageList, ImageList largeimageList)
        {
            // If this function was invoked by DoWork() then the thread cannot add images to the listview, but the UI thread will
            // be in control when called via Invoke()
            if (InvokeRequired)
            {
                // Use a lambda exp to create a Delegate that calls
                // AddToPhotoListView on the UI thread
                Invoke(new MethodInvoker(() => AddToPhotoListView(file, img, smallimageList, largeimageList)));
            }
            else
            {
                //Add image to the image lists
                smallimageList.Images.Add(img);
                largeimageList.Images.Add(img);

                //create an item with image name and the image (imageIndex is the pointer to the image in the image list)
                ListViewItem item = new ListViewItem(file.Name);
                int index = smallimageList.Images.Count - 1;
                item.ImageIndex = index;
                //Last modification date and time
                item.SubItems.Add(file.LastWriteTime.ToShortDateString() + " " + file.LastWriteTime.ToShortTimeString());

                //File Size
                if (file.Length < 1024)
                    item.SubItems.Add(file.Length.ToString() + " KB");
                else if (file.Length >= 1024)
                    item.SubItems.Add((file.Length / 1000).ToString() + " MB");

                //Set the item's tag to the img's path for later use with the 'locate on disk' functionality
                item.Tag = file.FullName;
                photoList.Items.Add(item);
            }
        }     

        private void PhotoLoaderBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            photoProgressBar.Value += e.ProgressPercentage;
        }

        private void PhotoLoaderBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            photoProgressBar.Visible = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            splitContainer.Size = new Size(Width - 40, Height - 100);
        }

        private void photoList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditPhotoForm editForm = new EditPhotoForm();
            ListViewItem item = photoList.SelectedItems[0];

            editForm.SetPhotoInfo(item.Tag.ToString());
            DialogResult result = editForm.ShowDialog();
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Was the click on the folder name?
            if (IsClickOnText(treeView, e.Node, e.Location))
            {
                /* Display selected folder's images if it is not the current folder 
                or if the root folder was selected after changing the root folder */
                if (rootChanged || !(Path.GetFullPath(e.Node.Tag.ToString()).Equals(Path.GetFullPath(currentDirectory))))
                {
                    rootChanged = false;

                    //Cancel background worker if it is running
                    if (photoLoaderBW.IsBusy)
                        if (photoLoaderBW.WorkerSupportsCancellation)
                            photoLoaderBW.CancelAsync();

                    //Hault code execution until background worker is cancelled
                    while (photoLoaderBW.IsBusy)
                        Application.DoEvents();

                    //Edit full path if folder could be more than one subfolder from root folder and set currentDirectory
                    currentDirectory = Path.GetFullPath(rootDirectory + "\\" + e.Node.FullPath.Substring(treeView.Nodes[0].Text.Length));

                    if (!photoLoaderBW.IsBusy)
                        photoLoaderBW.RunWorkerAsync();
                }
            }
        }
    }

    /*****************************************************************************************************************************
        http://stackoverflow.com/questions/87795/how-to-prevent-flickering-in-listview-when-updating-a-single-listviewitems-text
        Found this code online that gets rid of the flicking of the listview while loading images into it.
    *****************************************************************************************************************************/
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
    }
}