using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SkyrimUserSwitch.Properties;

namespace SkyrimUserSwitch.Model
{
    public class User
    {
        public const string ConstDefaultName = "default";
        public static readonly Image ConstDefaultAvatar = Resources.UnnknowGuy;

        private Image _avatar;

        private string _name;

        public User()
        {
            InitialConfig = null;
        }

        public User(Config.UserXml user)
        {
            InitialConfig = user;
            Name = user.Name;
            Avatar = user.Avatar;
        }

        public event EventHandler AvatarChanged;

        public event EventHandler NameChanged;

        public Image Avatar
        {
            get { return _avatar; }
            set
            {
                if (_avatar == value)
                    return;
                _avatar = value;
                OnAvatarChanged(this, EventArgs.Empty);
            }
        }

        public string ConfigFileFolderPath { get; set; }

        public Config.UserXml InitialConfig { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnNameChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void OnAvatarChanged(object sender, EventArgs e)
        {
            AvatarChanged.Notify(sender, e);
        }

        protected virtual void OnNameChanged(object sender, EventArgs e)
        {
            NameChanged.Notify(sender, e);
        }
    }
}