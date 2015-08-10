using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SkyrimUserSwitch.Controls
{
    public partial class PathTextBox : UserControl
    {
        public PathTextBox()
        {
            InitializeComponent();
        }

        public event EventHandler PathChanged;

        public string Description
        {
            get { return _descriptionLabel.Text; }
            set { _descriptionLabel.Text = value; }
        }

        public bool IsFolderPath { get; set; }

        public string Path
        {
            get { return _pathTextBox.Text; }
            set { _pathTextBox.Text = value; }
        }

        protected virtual void OnPathChanged(object sender, EventArgs e)
        {
            PathChanged.Notify(sender, e);
        }

        private void _browseButton_Click(object sender, EventArgs e)
        {
            if (IsFolderPath)
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.SelectedPath = Path;
                    DialogResult dialogResult = fbd.ShowDialog();
                    if (dialogResult != DialogResult.OK)
                        return;
                    Path = fbd.SelectedPath;
                }
            }
            else
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    string path = Path;
                    try
                    {
                        string name = System.IO.Path.GetFileName(path);
                        string directory = System.IO.Path.GetDirectoryName(path);
                        ofd.FileName = name;
                        ofd.InitialDirectory = directory;
                    }
                    catch (Exception)
                    {
                        ofd.FileName = path;
                    }
                    DialogResult dialogResult = ofd.ShowDialog();
                    if (dialogResult != DialogResult.OK)
                        return;
                    Path = ofd.FileName;
                }
            }
        }

        private void _pathTextBox_TextChanged(object sender, EventArgs e)
        {
            OnPathChanged(this, EventArgs.Empty);
        }
    }
}