using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SkyrimUserSwitch.Model;
using SkyrimUserSwitch.Properties;

namespace SkyrimUserSwitch.Forms
{
    public partial class OptionForm : Form
    {
        private readonly SkusModel _model;

        public OptionForm(SkusModel model)
        {
            InitializeComponent();
            this.Icon = Resources.UserSwitch;

            _model = model;

            _skyrimUserFolderPathTextBox.Path = _model.SkyrimUserFolder;
            _skyrimFolderPathTextBox.Path = _model.SkyrimFolder;
            _skyrimLauncherPathTextBox.Path = _model.SkyrimLauncherPath;
        }

        private OptionForm()
        {
            InitializeComponent();
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            _model.SkyrimUserFolder = _skyrimUserFolderPathTextBox.Path;
            _model.SkyrimFolder = _skyrimFolderPathTextBox.Path;
            _model.SkyrimLauncherPath = _skyrimLauncherPathTextBox.Path;
            
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}