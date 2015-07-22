using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SkyrimUserSwitch.Controls;
using SkyrimUserSwitch.Model;
using SkyrimUserSwitch.Properties;

namespace SkyrimUserSwitch.Forms
{
    public partial class UsersForm : Form
    {
        #region Members

        private readonly SkusModel _model;
        private readonly List<UserItem> _userItems;
        private bool _disposed = false;

        #endregion Members

        #region ctor

        public UsersForm(SkusModel model)
        {
            InitializeComponent();
            this.Icon = Resources.UserSwitch;

            _userItems = new List<UserItem> { };
            _model = model;
            CurrentUser = _model.CurrentUser;

            _model.IsValideChanged += _model_IsValideChanged;
            _model.UsersChanged += _model_UsersChanged;

            _userItemsPanel.MouseClick += _userItemsPanel_MouseClick;
        }

        private UsersForm()
        {
            InitializeComponent();
        }

        private void PartialDispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _userItems.Clear();
                _userItemsPanel.Controls.Clear();

                _model.IsValideChanged -= _model_IsValideChanged;
                _model.UsersChanged -= _model_UsersChanged;
            }
        }

        #endregion ctor

        #region Properties

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser == value)
                    return;
                _currentUser = value;
                UpdateCurrentUser();
            }
        }

        #endregion Properties

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UpdateModel();
            UpdateUi();
        }

        #endregion Overrides

        #region Model events

        private void _model_IsValideChanged(object sender, EventArgs e)
        {
            UpdateIsValide();
        }

        private void _model_UsersChanged(object sender, EventArgs e)
        {
            UpdateUsers();
        }

        #endregion Model events

        #region UI Updates

        private void RefreshOkButtonEnabled()
        {
            _closeButton.Enabled = (CurrentUser != null) && _model.IsValide;
        }

        private void UpdateCurrentUser()
        {
            User currentUser = CurrentUser;
            _userItems.ForEach(ui => ui.Selected = (ui.User == currentUser));

            RefreshOkButtonEnabled();
        }

        private void UpdateIsValide()
        {
            bool isValide = _model.IsValide;

            _userItemsPanel.Enabled = isValide;
            RefreshOkButtonEnabled();
        }

        private void UpdateModel()
        {
            UpdateIsValide();
            UpdateUsers();
        }

        private void UpdateUi()
        {
            UpdateCurrentUser();
        }

        private void UpdateUsers()
        {
            _userItemsPanel.SuspendLayout();

            _userItemsPanel.Controls.Clear();
            _userItems.ForEach(ui => ui.Dispose());
            _userItems.Clear();

            IEnumerable<User> users = _model.Users
                .OrderBy(u => u.Name)
                .ToArray();
            int top = 0;
            int width = _userItemsPanel.ClientSize.Width;
            foreach (User user in users)
            {
                UserItem userItem = new UserItem();
                _userItemsPanel.Controls.Add(userItem);
                _userItems.Add(userItem);
                userItem.Top = top;
                userItem.Width = _userItemsPanel.ClientSize.Width;
                userItem.Anchor |= AnchorStyles.Right;
                userItem.User = user;
                userItem.ItemClick += userItem_ItemClick;
                userItem.ItemDoubleClick += userItem_ItemDoubleClick;
                userItem.ItemRightClick += userItem_ItemRightClick;

                top += userItem.MinimumSize.Height;
            }
            _userItemsPanel.ResumeLayout();

            User oldCurrentUser = CurrentUser;
            string oldCurrentUserName = oldCurrentUser != null ? oldCurrentUser.Name : null;
            User currentUser = users.FirstOrDefault(u => u.Name == oldCurrentUserName);
            currentUser = currentUser ?? _model.CurrentUser;
            CurrentUser = currentUser;
        }

        #endregion UI Updates

        #region User inputs

        private void _addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = User.ConstDefaultName;
            HashSet<string> names = _model.Users
                .Select(u => u.Name)
                .ToHashSet();
            bool isUsed = true;
            while (isUsed)
            {
                DialogResult dialogResult = InputBox.ShowDialog(Resources.TitleUserName, Resources.MessageUserName, ref name);
                if (dialogResult != DialogResult.OK)
                    return;

                isUsed = names.Contains(name);
                if (isUsed)
                    MessageBox.Show(String.Format(Resources.FMessageNameAlreadyUsed, name));
            }

            User newUser = new User
            {
                Name = name,
                Avatar = User.ConstDefaultAvatar,
            };
            _model.AddUser(newUser);
            CurrentUser = newUser;
        }

        private void _changeAvatarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Assert(CurrentUser != null);
            if (CurrentUser == null)
                return;

            Image image = null;
            while (image == null)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.TIFF|All files (*.*)|*.*";
                    DialogResult dialogResult = ofd.ShowDialog();
                    if (dialogResult != DialogResult.OK)
                        return;
                    try
                    {
                        image = Image.FromFile(ofd.FileName);
                    }
                    catch { }
                }
            }
            if (image.Width > 42 || image.Height > 42)
            {
                image = image.GetThumbnailImage(42, 42, () => false, IntPtr.Zero);
            }

            CurrentUser.Avatar = image;
        }

        private void _changeNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Assert(CurrentUser != null);
            if (CurrentUser == null)
                return;

            string oldName = CurrentUser.Name;
            HashSet<string> names = _model.Users
                .Select(u => u.Name)
                .Where(n => n != oldName)
                .ToHashSet();
            string newName = oldName;
            bool isNotValide = true;
            while (isNotValide)
            {
                DialogResult dialogResult = InputBox.ShowDialog(Resources.TitleUserName, Resources.MessageUserName, ref newName);
                if (dialogResult != DialogResult.OK)
                    return;
                isNotValide = names.Contains(newName);
            }

            CurrentUser.Name = newName;
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Assert(CurrentUser != null);
            if (CurrentUser == null)
                return;

            DialogResult dialogResult = MessageBox.Show(
                String.Format(Resources.FMessageSuppression, CurrentUser.Name)
                , Resources.TitleSuppression
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.None
                , MessageBoxDefaultButton.Button2);
            if (dialogResult != DialogResult.Yes)
                return;

            _model.RemoveUser(CurrentUser);
        }

        private void _userItemsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            CurrentUser = null;
            if (e.Button == MouseButtons.Right)
                ShowPopupMenu();
        }

        private void ShowPopupMenu()
        {
            //_addToolStripMenuItem.Enabled = true; // always
            _removeToolStripMenuItem.Enabled = (CurrentUser != null) && (_model.Users.Count() > 0);
            _changeNameToolStripMenuItem.Enabled = (CurrentUser != null);
            _changeAvatarToolStripMenuItem.Enabled = (CurrentUser != null);

            _contextMenuStrip.Show(System.Windows.Forms.Cursor.Position);
        }

        private void userItem_ItemClick(object sender, EventArgs e)
        {
            UserItem userItem = sender as UserItem;
            if (userItem == null)
                return;

            CurrentUser = userItem.User;
        }

        private void userItem_ItemDoubleClick(object sender, EventArgs e)
        {
            UserItem userItem = sender as UserItem;
            if (userItem == null)
                return;

            User user = userItem.User;
            _model.SetCurrentUser(user);

            _closeButton.PerformClick();
        }

        private void userItem_ItemRightClick(object sender, EventArgs e)
        {
            UserItem userItem = sender as UserItem;
            if (userItem == null)
                return;

            CurrentUser = userItem.User;
            ShowPopupMenu();
        }

        #endregion User inputs
    }
}