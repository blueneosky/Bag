using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SkyrimUserSwitch.Config;
using SkyrimUserSwitch.Properties;

namespace SkyrimUserSwitch.Model
{
    public class SkusModel
    {
        #region Constante

        private const int ConstInvalideCodeInvalideSkyrimFolder = 303;
        private const int ConstInvalideCodeReserved = 301;
        private const int ConstInvalideCodeSkyrimFolderDoNotExist = 302;
        private const int ConstInvalideCodeSkyrimUserFolderDoNotExist = 304;
        private const int ConstInvalideCodeSavesFolderDoNotExist = 305;
        private const int ConstInvalideCodeX6 = 306;
        private const int ConstInvalideCodeX7 = 307;
        private const int ConstInvalideCodeX8 = 308;

        #endregion Constante

        #region Members

        private User _currentUser;
        private string _initialSkyrimFolder;
        private string _initialSkyrimLauncherPath;
        private string _initialSkyrimUserFolder;
        private string _invalideReason;
        private Dictionary<int, string> _invalideReasons = new Dictionary<int, string> { };
        private volatile bool _isApplicationClose;
        private bool _isValide;
        private string _skyrimFolder;
        private string _skyrimLauncherPath;
        private string _skyrimUserFolder;
        private User[] _users;

        #endregion Members

        #region ctor

        public SkusModel()
        {
        }

        #endregion ctor

        #region Events

        public event EventHandler CurrentUserChanged;

        public event EventHandler ExitRequested;

        public event EventHandler InvalideReasonChanged;

        public event EventHandler IsValideChanged;

        public event EventHandler SkyrimFolderChanged;

        public event EventHandler SkyrimLauncherPathChanged;

        public event EventHandler SkyrimUserFolderChanged;

        public event EventHandler UsersChanged;

        protected virtual void OnCurrentUserChanged(object sender, EventArgs e)
        {
            CurrentUserChanged.Notify(sender, e);
        }

        protected virtual void OnExitRequested(object sender, EventArgs e)
        {
            ExitRequested.Notify(sender, e);
        }

        protected virtual void OnInvalideReasonChanged(object sender, EventArgs e)
        {
            InvalideReasonChanged.Notify(sender, e);
        }

        protected virtual void OnIsValideChanged(object sender, EventArgs e)
        {
            IsValideChanged.Notify(sender, e);
        }

        protected virtual void OnSkyrimFolderChanged(object sender, EventArgs e)
        {
            SkyrimFolderChanged.Notify(sender, e);
        }

        protected virtual void OnSkyrimLauncherPathChanged(object sender, EventArgs e)
        {
            SkyrimLauncherPathChanged.Notify(sender, e);
        }

        protected virtual void OnSkyrimUserFolderChanged(object sender, EventArgs e)
        {
            SkyrimUserFolderChanged.Notify(sender, e);
        }

        protected virtual void OnUsersChanged(object sender, EventArgs e)
        {
            UsersChanged.Notify(sender, e);
        }

        #endregion Events

        #region Properties

        public User CurrentUser
        {
            get { return _currentUser; }
            private set
            {
                if (_currentUser == value)
                    return;
                _currentUser = value;
                OnCurrentUserChanged(this, EventArgs.Empty);
            }
        }

        public string InvalideReason
        {
            get { return _invalideReason; }
            set
            {
                if (_invalideReason == value)
                    return;
                _invalideReason = value;
                OnInvalideReasonChanged(this, EventArgs.Empty);
            }
        }

        public bool IsConfigModified
        {
            get
            {
                bool modified =
                    (_initialSkyrimUserFolder != _skyrimUserFolder)
                    || (_initialSkyrimFolder != _skyrimFolder)
                    || (_initialSkyrimLauncherPath != _skyrimLauncherPath)
                    ;
                return modified;
            }
        }

        public bool IsValide
        {
            get { return _isValide; }
            set
            {
                if (_isValide == value)
                    return;
                _isValide = value;
                OnIsValideChanged(this, EventArgs.Empty);
            }
        }

        public string SkyrimFolder
        {
            get { return _skyrimFolder; }
            set
            {
                if (_skyrimFolder == value)
                    return;
                _skyrimFolder = value;
                OnSkyrimFolderChanged(this, EventArgs.Empty);
                CheckSkyrimFolder();
            }
        }

        public string SkyrimLauncherPath
        {
            get { return _skyrimLauncherPath; }
            set
            {
                if (_skyrimLauncherPath == value)
                    return;
                _skyrimLauncherPath = value;
                OnSkyrimLauncherPathChanged(this, EventArgs.Empty);
                CheckSkyrimLauncherPath();
            }
        }

        public string SkyrimUserFolder
        {
            get { return _skyrimUserFolder; }
            set
            {
                if (_skyrimUserFolder == value)
                    return;
                _skyrimUserFolder = value;
                OnSkyrimUserFolderChanged(this, EventArgs.Empty);
                LoadUsersAndSaves();
            }
        }

        public IEnumerable<User> Users
        {
            get { return _users; }
            private set
            {
                _users = value.ToArray();
                string currentUserName = _currentUser != null ? _currentUser.Name : null;
                User newCurrentUser = _users.FirstOrDefault(u => u.Name == currentUserName);
                User oldCurrentUser = _currentUser;
                _currentUser = newCurrentUser;  // notified can read this value
                OnUsersChanged(this, EventArgs.Empty);
                _currentUser = oldCurrentUser;
                CurrentUser = newCurrentUser;   // force refresh if needed
            }
        }

        #endregion Properties

        public void AddUser(User newUser)
        {
            UserXml userXml = new UserXml(newUser);
            string userFileFolderPath = GetUnusedUserFileFolderPath(newUser.Name);
            try
            {
                Directory.CreateDirectory(userFileFolderPath);
            }
            catch (Exception)
            {
                string message = String.Format(Resources.FMessageFailedToCreateDirectory, userFileFolderPath);
                MessageBox.Show(message);
                return;
            }

            string userFileName = Resources.UserFileName;
            string userFilePath = Path.Combine(userFileFolderPath, userFileName);

            bool success = XmlService.TrySave(userFilePath, userXml);
            if (false == success)
            {
                string message = String.Format(Resources.FMessageFailedToSaveUserFile, userFilePath);
                MessageBox.Show(message);
                return;
            }

            newUser.ConfigFileFolderPath = userFileFolderPath;
            newUser.InitialConfig = userXml;
            IEnumerable<User> users = Users
                .Concat(new[] { newUser });
            Users = users;
        }

        public void CheckSkyrimFolder()
        {
            string skyrimFolder = SkyrimFolder;
            do
            {
                if (false == Directory.Exists(skyrimFolder))
                {
                    string invalideReason = String.Format(Resources.FMessageSkyrimFolderDoNotExist, skyrimFolder);
                    SetInvalideReason(ConstInvalideCodeSkyrimFolderDoNotExist, invalideReason);
                    break;
                }
                SetInvalideReason(ConstInvalideCodeSkyrimFolderDoNotExist, null);

                string skyrimLauncherExe = Resources.SkyrimLauncherExe;
                string tesvExe = Resources.TESVExe;
                string skyrimLauncherPath = Path.Combine(skyrimFolder, skyrimLauncherExe);
                string tesvPath = Path.Combine(skyrimFolder, tesvExe);
                bool isSkyrimFolderWithBinaries =
                    File.Exists(skyrimLauncherPath)
                    && File.Exists(tesvPath);
                if (false == isSkyrimFolderWithBinaries)
                {
                    string invalideReason = String.Format(Resources.FMessageInvalideSkyrimFolder, skyrimFolder, skyrimLauncherExe, tesvExe);
                    SetInvalideReason(ConstInvalideCodeInvalideSkyrimFolder, invalideReason);
                    break;
                }
                SetInvalideReason(ConstInvalideCodeInvalideSkyrimFolder, null);
            } while (false);
        }

        public void LoadUsersAndSaves()
        {
            User[] users = new User[] { };
            User currentUser = null;

            do
            {
                string skyrimUserFolder = SkyrimUserFolder;
                if (false == Directory.Exists(skyrimUserFolder))
                {
                    string invalideReason = String.Format(Resources.FMessageSkyrimUserFolderDoNotExist, skyrimUserFolder);
                    SetInvalideReason(ConstInvalideCodeSkyrimUserFolderDoNotExist, invalideReason);
                    break;
                }
                SetInvalideReason(ConstInvalideCodeSkyrimUserFolderDoNotExist, null);

                // default Saves directory
                string savesRelativePath = Resources.SavesRelativePath;
                string savesFolder = Path.Combine(skyrimUserFolder, savesRelativePath);
                savesFolder = Path.GetFullPath(savesFolder);
                if (false == Directory.Exists(savesFolder))
                {
                    string invalideReason = String.Format(Resources.FMessageSavesFolderDoNotExist, savesFolder);
                    SetInvalideReason(ConstInvalideCodeSavesFolderDoNotExist, invalideReason);
                    break;
                }
                SetInvalideReason(ConstInvalideCodeSavesFolderDoNotExist, null);

                currentUser = TryLoadUser(savesFolder);
                if (currentUser == null)
                {
                    currentUser = new User()
                    {
                        Name = User.ConstDefaultName,
                        Avatar = User.ConstDefaultAvatar,
                        ConfigFileFolderPath = savesFolder,
                    };
                }

                IEnumerable<User> usersLoaded = Directory.EnumerateDirectories(skyrimUserFolder)
                    .Select(Path.GetFullPath)
                    .Where(path => path != savesFolder)
                    .Select(TryLoadUser)
                    .Where(user => user != null);
                usersLoaded = new[] { currentUser }
                    .Concat(usersLoaded)
                    .ToArray();

                IEnumerable<IGrouping<string, User>> usersByName = usersLoaded
                    .GroupBy(user => user.Name)
                    .ToArray();

                IEnumerable<User> duplicatedUsers = usersByName
                    .SelectMany(grp => grp.Skip(1))
                    .ToArray();
                if (duplicatedUsers.Any())
                {
                    HashSet<string> names = usersByName
                        .Select(grp => grp.Key)
                        .ToHashSet();
                    List<User> list = usersByName.Select(grp => grp.First()).ToList();
                    foreach (User user in duplicatedUsers)
                    {
                        string newName = user.Name.NewUnique(names);
                        user.Name = newName;
                        list.Add(user);
                        names.Add(newName);
                    }
                    users = list.ToArray();
                }
                else
                {
                    users = usersLoaded.ToArray();
                }
            } while (false);

            CurrentUser = null;
            Users = users;
            CurrentUser = currentUser;
        }

        public void RemoveUser(User user)
        {
            if (CurrentUser == user)
            {
                // change the user
                User newSelectedUser = Users.FirstOrDefault(u => u != user);
                if (newSelectedUser == null)
                {
                    Debug.Fail("Failed to switch user before removing.");
                    return;
                }
                SetCurrentUser(newSelectedUser);
            }

            string userFileFolderPath = user.ConfigFileFolderPath;
            string userFileName = Resources.UserFileName;
            string userFilePath = Path.Combine(userFileFolderPath, userFileName);
            try
            {
                File.Delete(userFilePath);
                IEnumerable<User> users = Users
                    .Where(u => u != user);
                Users = users;
            }
            catch (Exception)
            {
                string message = String.Format(Resources.FMessageRemoveFailed, user.Name);
                MessageBox.Show(message);
            }
        }

        public void RunSkyrimLauncher()
        {
            string skyrimLauncherPath = SkyrimLauncherPath;
            if (String.IsNullOrWhiteSpace(skyrimLauncherPath))
                return;

            Process process;
            try
            {
                process = Process.Start(skyrimLauncherPath);

                ThreadStart action = delegate
                {
                    bool exited = false;
                    while ((false == exited) && (false == _isApplicationClose))
                    {
                        exited = process.WaitForExit(100);
                    }
                    OnExitRequested(this, EventArgs.Empty);
                };
                new Thread(action).Start();
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format(Resources.FMessageFailedToStartSkyrimLauncher, skyrimLauncherPath));
            }
        }

        public void SaveUserConfig()
        {
            IEnumerable<User> users = Users.ToArray();
            SaveUserConfig(users, false);
        }

        public void SetApplicationClose()
        {
            _isApplicationClose = true;
        }

        public void SetConfig(Config.ConfigXml configXml)
        {
            SetInvalideReason(ConstInvalideCodeReserved, null);

            if (configXml != null)
            {
                string skyrimUserFolder = configXml.SkyrimUserFolder;
                string skyrimFolder = configXml.SkyrimFolder;
                string skyrimLauncherPath = configXml.SkyrimLauncherPath;
                _initialSkyrimUserFolder = skyrimUserFolder;
                _initialSkyrimFolder = skyrimFolder;
                _initialSkyrimLauncherPath = skyrimLauncherPath;
                SkyrimUserFolder = skyrimUserFolder;
                SkyrimFolder = skyrimFolder;
                SkyrimLauncherPath = skyrimLauncherPath;
            }
            else
            {
                string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string skyrimUserRelativePath = Resources.DefaultSkyrimUserRelativePath;
                string skyrimUserFolder = Path.Combine(myDocumentsFolder, skyrimUserRelativePath);
                skyrimUserFolder = Path.GetFullPath(skyrimUserFolder);

                string programFilesX86Folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                string skyrimRelativePath = Resources.DefaultSkyrimRelativePath;
                string skyrimFolder = Path.Combine(programFilesX86Folder, skyrimRelativePath);
                skyrimFolder = Path.GetFullPath(skyrimFolder);

                string skyrimLauncherRelativePath = Resources.SkyrimLauncherExe;
                string skyrimLauncherPath = Path.Combine(skyrimFolder, skyrimLauncherRelativePath);
                skyrimLauncherPath = Path.GetFullPath(skyrimLauncherPath);

                _initialSkyrimUserFolder = null;    // force save
                _initialSkyrimFolder = null;        // force save
                _initialSkyrimLauncherPath = null;  // force save
                SkyrimUserFolder = skyrimUserFolder;
                SkyrimFolder = skyrimFolder;
                SkyrimLauncherPath = skyrimLauncherPath;
            }
        }

        public void SetCurrentUser(User user)
        {
            if (user == null)
                return;

            PackCurrentUser();

            Deploy(user);

            CurrentUser = user;
        }

        private void CheckSkyrimLauncherPath()
        {
            // nothing
            // already check in CheckSkyrimPath()
        }

        private void Deploy(User user)
        {
            UserXml userXml = user.InitialConfig;
            DeployControlMap_CustomTxt(userXml.ControlMap_CustomTxtContents);
            DeploySkyrimIni(userXml.SkyrimIniContents);
            DeploySkyrimPrefsIni(userXml.SkyrimPrefsIniContents);

            string skyrimUserFolder = SkyrimUserFolder;
            string savesRelativePath = Resources.SavesRelativePath;
            string savesFolder = Path.Combine(skyrimUserFolder, savesRelativePath);

            string oldUserFileFolderPath = user.ConfigFileFolderPath;
            string newUserFileFolderPath = savesFolder;
            Directory.Move(oldUserFileFolderPath, newUserFileFolderPath);
            user.ConfigFileFolderPath = newUserFileFolderPath;
        }

        private void DeployControlMap_CustomTxt(MemoryStream stream)
        {
            string controlMap_CustomTxtFilePath = GetControlMap_CustomTxtFilePath();

            DeployFile(controlMap_CustomTxtFilePath, stream);
        }

        private void DeployFile(string filePath, MemoryStream stream)
        {
            if (stream == null)
                return;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }

        private void DeploySkyrimIni(MemoryStream stream)
        {
            string skyrimIniFilePath = GetSkyrimIniFilePath();

            DeployFile(skyrimIniFilePath, stream);
        }

        private void DeploySkyrimPrefsIni(MemoryStream stream)
        {
            string skyrimPrefsIniFilePath = GetSkyrimPrefsIniFilePath();

            DeployFile(skyrimPrefsIniFilePath, stream);
        }

        private MemoryStream GetContentsFromFile(string filePath)
        {
            if (false == File.Exists(filePath))
                return null;

            MemoryStream result = new MemoryStream();

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(result);
                }
            }
            catch (Exception)
            {
                Debug.Fail("Failed to open " + filePath);
                return null;
            }

            return result;
        }

        private MemoryStream GetControlMap_CustomTxtContents()
        {
            string controlMap_CustomTxtFilePath = GetControlMap_CustomTxtFilePath();

            MemoryStream result = GetContentsFromFile(controlMap_CustomTxtFilePath);

            return result;
        }

        private string GetControlMap_CustomTxtFilePath()
        {
            string skyrimFolder = SkyrimFolder;
            string controlMap_CustomTxtFileName = Resources.ControlMap_CustomTxt;
            string controlMap_CustomTxtFilePath = Path.Combine(skyrimFolder, controlMap_CustomTxtFileName);

            return controlMap_CustomTxtFilePath;
        }

        private MemoryStream GetSkyrimIniContents()
        {
            string skyrimIniFilePath = GetSkyrimIniFilePath();

            MemoryStream result = GetContentsFromFile(skyrimIniFilePath);

            return result;
        }

        private string GetSkyrimIniFilePath()
        {
            string skyrimUserFolder = SkyrimUserFolder;
            string skyrimIniFileName = Resources.SkyrimIni;
            string skyrimIniFilePath = Path.Combine(skyrimUserFolder, skyrimIniFileName);

            return skyrimIniFilePath;
        }

        private MemoryStream GetSkyrimPrefsIniContents()
        {
            string skyrimPrefsIniFilePath = GetSkyrimPrefsIniFilePath();

            MemoryStream result = GetContentsFromFile(skyrimPrefsIniFilePath);

            return result;
        }

        private string GetSkyrimPrefsIniFilePath()
        {
            string skyrimUserFolder = SkyrimUserFolder;
            string skyrimPrefsIniFileName = Resources.SkyrimPrefsIni;
            string skyrimPrefsIniFilePath = Path.Combine(skyrimUserFolder, skyrimPrefsIniFileName);

            return skyrimPrefsIniFilePath;
        }

        private string GetUnusedUserFileFolderPath(string baseName)
        {
            string skyrimUserFolder = SkyrimUserFolder;
            IEnumerable<string> existingEntries = Directory.EnumerateFileSystemEntries(skyrimUserFolder)
                .Select(Path.GetFileName)
                .ToHashSet();

            string userFileFolderName = String.Concat(Resources.SavesRelativePath, "_", baseName);
            userFileFolderName = userFileFolderName.Unique(existingEntries);

            string userFileFolderPath = Path.Combine(skyrimUserFolder, userFileFolderName);

            return userFileFolderPath;
        }

        private void PackCurrentUser()
        {
            User user = CurrentUser;
            Debug.Assert(user != null);
            SaveUserConfig(user, true);

            RemoveControlMap_CustomTxt();
            RemoveSkyrimIni();
            RemoveSkyrimPrefsIni();

            string oldUserFileFolderPath = user.ConfigFileFolderPath;
            string newUserFileFolderPath = GetUnusedUserFileFolderPath(user.Name);
            Directory.Move(oldUserFileFolderPath, newUserFileFolderPath);
            user.ConfigFileFolderPath = newUserFileFolderPath;
        }

        private void RemoveControlMap_CustomTxt()
        {
            string controlMap_CustomTxtFilePath = GetControlMap_CustomTxtFilePath();

            RemoveFile(controlMap_CustomTxtFilePath);
        }

        private void RemoveFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private void RemoveSkyrimIni()
        {
            string skyrimIniFilePath = GetSkyrimIniFilePath();

            RemoveFile(skyrimIniFilePath);
        }

        private void RemoveSkyrimPrefsIni()
        {
            string skyrimPrefsIniFilePath = GetSkyrimPrefsIniFilePath();

            RemoveFile(skyrimPrefsIniFilePath);
        }

        private void SaveUserConfig(IEnumerable<User> users, bool withContents)
        {
            foreach (User user in users)
            {
                SaveUserConfig(user, withContents);
            }
        }

        private bool SaveUserConfig(User user, bool withContents)
        {
            bool success = true;

            UserXml userXml = new UserXml(user);
            if (withContents)
            {
                bool isCurrentUser = (user == CurrentUser);
                Debug.Assert(isCurrentUser == (Path.GetFileName(user.ConfigFileFolderPath) == Resources.SavesRelativePath));
                if (isCurrentUser)
                {
                    userXml.ControlMap_CustomTxtContents = GetControlMap_CustomTxtContents();
                    userXml.SkyrimIniContents = GetSkyrimIniContents();
                    userXml.SkyrimPrefsIniContents = GetSkyrimPrefsIniContents();
                }
            }

            bool isUnchanged = userXml.Equals(user.InitialConfig, withContents);
            if (false == isUnchanged)
            {
                string userFolderPath = user.ConfigFileFolderPath;
                string userFileName = Resources.UserFileName;
                string userFilePath = Path.Combine(userFolderPath, userFileName);
                success = XmlService.TrySave(userFilePath, userXml);
                if (success)
                {
                    user.InitialConfig = userXml;
                }
                else
                {
                    string message = String.Format(Resources.FMessageFailedToSaveUserFile, userFilePath);
                    MessageBox.Show(message);
                }
            }

            return success;
        }

        private void SetInvalideReason(int code, string reason)
        {
            _invalideReasons[code] = reason;
            string firstReason = _invalideReasons
                .Values
                .FirstOrDefault(r => r != null);
            bool isValide = (firstReason == null);

            IsValide = isValide;
            InvalideReason = reason;
        }

        private User TryLoadUser(string folder)
        {
            if (false == Directory.Exists(folder))
                return null;

            string userFileName = Resources.UserFileName;
            string userFilePath = Path.Combine(folder, userFileName);

            if (false == File.Exists(userFilePath))
                return null;

            UserXml userXml;
            bool success = XmlService.TryLoad<UserXml>(userFilePath, out userXml);
            if (false == success)
                return null;

            User result = new User(userXml);
            result.ConfigFileFolderPath = folder;

            return result;
        }
    }
}