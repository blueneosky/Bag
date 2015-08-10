using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SkyrimUserSwitch.Model;
using SkyrimUserSwitch.Properties;

namespace SkyrimUserSwitch.Forms
{
    public partial class MainForm : Form
    {
        #region Members

        private readonly SkusModel _model;
        private AutoResetEvent _autoCloseEvent = new AutoResetEvent(true);
        private User _currentUser;
        private bool _disposed;

        #endregion Members

        #region ctor

        public MainForm(SkusModel model)
        {
            InitializeComponent();
            _invalideReasonLabel.BringToFront();
            this.Icon = Resources.UserSwitch;

            _model = model;

            _model.CurrentUserChanged += _model_CurrentUserChanged;
            _model.IsValideChanged += _model_IsValideChanged;
            _model.InvalideReasonChanged += _model_InvalideReasonChanged;
            _model.ExitRequested += _model_ExitRequested;
        }

        private MainForm()
        {
            InitializeComponent();
        }

        private void PartialDispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;

            if (disposing)
            {
                _model.CurrentUserChanged -= _model_CurrentUserChanged;
                _model.IsValideChanged -= _model_IsValideChanged;
                _model.InvalideReasonChanged -= _model_InvalideReasonChanged;
                _model.ExitRequested -= _model_ExitRequested;
            }
        }

        #endregion ctor

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_model == null)
                return;

            UpdateModel();
        }

        #endregion Overrides

        #region Properties

        private User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != null)
                {
                    _currentUser.NameChanged -= _currentUser_NameChanged;
                    _currentUser.AvatarChanged -= _currentUser_AvatarChanged;
                }
                _currentUser = value;
                if (_currentUser != null)
                {
                    _currentUser.NameChanged += _currentUser_NameChanged;
                    _currentUser.AvatarChanged += _currentUser_AvatarChanged;
                }
                UpdateCurrentUserName();
                UpdateCurrentUserAvatar();
            }
        }

        #endregion Properties

        #region Model events

        private void _currentUser_AvatarChanged(object sender, EventArgs e)
        {
            UpdateCurrentUserAvatar();
        }

        private void _currentUser_NameChanged(object sender, EventArgs e)
        {
            UpdateCurrentUserName();
        }

        private void _model_CurrentUserChanged(object sender, EventArgs e)
        {
            UpdateCurrentUser();
        }

        private void _model_ExitRequested(object sender, EventArgs e)
        {
            ManageExitRequested();
        }

        private void _model_InvalideReasonChanged(object sender, EventArgs e)
        {
            UpdateInvalideReason();
        }

        private void _model_IsValideChanged(object sender, EventArgs e)
        {
            UpdateIsValide();
        }

        #endregion Model events

        #region UI Updates

        private void UpdateCurrentUser()
        {
            CurrentUser = _model.CurrentUser;
        }

        private void UpdateCurrentUserAvatar()
        {
            User currentUser = CurrentUser;

            _avatarPictureBox.Image = currentUser != null ? currentUser.Avatar : null;
        }

        private void UpdateCurrentUserName()
        {
            User currentUser = CurrentUser;

            _nameTextBox.Text = currentUser != null ? currentUser.Name : String.Empty;
        }

        private void UpdateInvalideReason()
        {
            string invalideReason = _model.InvalideReason;

            _invalideReasonLabel.Text = invalideReason;
            _invalideReasonLabel.Visible = false == String.IsNullOrEmpty(invalideReason);
        }

        private void UpdateIsValide()
        {
            bool isValide = _model.IsValide;

            _avatarPictureBox.Enabled = isValide;
            _nameTextBox.Enabled = isValide;
            _changeButton.Enabled = isValide;
        }

        private void UpdateModel()
        {
            UpdateCurrentUser();
            UpdateIsValide();
            UpdateInvalideReason();
        }

        #endregion UI Updates

        #region User input

        private void _changeButton_Click(object sender, EventArgs e)
        {
            _autoCloseEvent.Reset();
            using (UsersForm form = new UsersForm(_model))
            {
                form.ShowDialog();
            }
            _model.SaveUserConfig();
            _autoCloseEvent.Set();
        }

        private void _optionButton_Click(object sender, EventArgs e)
        {
            _autoCloseEvent.Reset();
            using (OptionForm form = new OptionForm(_model))
            {
                form.ShowDialog();
            }
            _autoCloseEvent.Set();
        }

        #endregion User input

        private void ManageExitRequested()
        {
            ThreadStart action = delegate
            {
                _autoCloseEvent.WaitOne();
                Action actionClose = delegate
                {
                    Close();
                };
                Invoke(actionClose);
            };
            new Thread(action).Start();
        }
    }
}