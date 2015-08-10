using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SkyrimUserSwitch.Model;

namespace SkyrimUserSwitch.Controls
{
    public partial class UserItem : UserControl
    {
        private readonly Color _initialBackColor;
        private readonly Color _initialLabelForeColor;
        private bool _disposed;
        private bool _selected;
        private User _user;

        public UserItem()
        {
            InitializeComponent();

            _initialBackColor = BackColor;
            _initialLabelForeColor = _nameLabel.ForeColor;

            this.Click += ItemClickProxy;
            this.DoubleClick += ItemDoubleClickProxy;
            this.MouseClick += ItemRightClickProxy;
            _avatarPictureBox.Click += ItemClickProxy;
            _avatarPictureBox.DoubleClick += ItemDoubleClickProxy;
            _avatarPictureBox.MouseClick += ItemRightClickProxy;
            _nameLabel.Click += ItemClickProxy;
            _nameLabel.DoubleClick += ItemDoubleClickProxy;
            _nameLabel.MouseClick += ItemRightClickProxy;
        }

        public event EventHandler ItemClick;

        public event EventHandler ItemDoubleClick;

        public event EventHandler ItemRightClick;

        public Image Avatar
        {
            get { return _avatarPictureBox.Image; }
            set { _avatarPictureBox.Image = value; }
        }

        [Browsable(false)]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                BackColor = _selected ? SystemColors.Highlight : _initialBackColor;
                _nameLabel.ForeColor = _selected ? SystemColors.HighlightText : _initialLabelForeColor;
            }
        }

        [Browsable(false)]
        public User User
        {
            get { return _user; }
            set
            {
                if (_user != null)
                {
                    _user.NameChanged -= _user_NameChanged;
                    _user.AvatarChanged -= _user_AvatarChanged;
                }
                _user = value;
                if (_user != null)
                {
                    _user.NameChanged += _user_NameChanged;
                    _user.AvatarChanged += _user_AvatarChanged;
                }
                UpdateUserName();
                UpdateUserAvatar();
            }
        }

        public string UserName
        {
            get { return _nameLabel.Text; }
            set { _nameLabel.Text = value; }
        }

        protected virtual void OnItemClick(object sender, EventArgs e)
        {
            ItemClick.Notify(sender, e);
        }

        protected virtual void OnItemDoubleClick(object sender, EventArgs e)
        {
            ItemDoubleClick.Notify(sender, e);
        }

        protected virtual void OnItemRightClick(object sender, EventArgs e)
        {
            ItemRightClick.Notify(sender, e);
        }

        private void _user_AvatarChanged(object sender, EventArgs e)
        {
            UpdateUserAvatar();
        }

        private void _user_NameChanged(object sender, EventArgs e)
        {
            UpdateUserName();
        }

        private void ItemClickProxy(object sender, EventArgs e)
        {
            OnItemClick(this, EventArgs.Empty);
        }

        private void ItemDoubleClickProxy(object sender, EventArgs e)
        {
            OnItemDoubleClick(this, EventArgs.Empty);
        }

        private void ItemRightClickProxy(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                OnItemRightClick(this, EventArgs.Empty);
        }

        private void PartialDispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;

            if (disposing)
            {
                this.Click -= ItemClickProxy;
                this.DoubleClick -= ItemDoubleClickProxy;
                this.MouseClick -= ItemRightClickProxy;
                _avatarPictureBox.Click -= ItemClickProxy;
                _avatarPictureBox.DoubleClick -= ItemDoubleClickProxy;
                _avatarPictureBox.MouseClick -= ItemRightClickProxy;
                _nameLabel.Click -= ItemClickProxy;
                _nameLabel.DoubleClick -= ItemDoubleClickProxy;
                _nameLabel.MouseClick -= ItemRightClickProxy;
            }
        }

        private void UpdateUserAvatar()
        {
            User user = User;

            Avatar = (user != null) ? user.Avatar : null;
        }

        private void UpdateUserName()
        {
            User user = User;

            UserName = (user != null) ? user.Name : String.Empty;
        }
    }
}