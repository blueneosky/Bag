using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gitfs.Util
{
    internal static class GitHelper
    {
        private const string ConstGit = "git";
        private const string ConstStatus = "status";
        private const string ConstConfig = "config";
        private const string ConstInit = "init";
        private const string ConstLog = "log";
        private const string ConstRevParse = "rev-parse";

        private const string ConstBaseConfigName = "tfs.";
        private const string ConstConfigLastChangeset = "lastchangeset";
        private const string ConstLastCommit = "lastcommit";
        private const string ConstConfigTagChangeset = "tagchangeset";
        private const string ConstConfigDeepMode = "deepmode";
        private const string ConstConfigProjectCollection = "projectcollection";
        private const string ConstConfigServerPath = "serverpath";

        public static string LastOutput { get; private set; }

        public static int Launch(out string standarOutput, string command, params string[] args)
        {
            int success = ProcessHelper.Launch(out standarOutput, command, args);

            LastOutput = standarOutput;
            return success;
        }

        private static string ReadFirstLine(string text)
        {
            using (StringReader reader = new StringReader(text))
            {
                return reader.ReadLine();
            }
        }

        public static bool IsCleanHead()
        {
            string output;
            int success = Launch(out output, ConstGit, ConstStatus, "-s");

            if (success != 0)
                return false;
            if (output.Length > 0)
                return false;

            return true;
        }

        public static bool GitInit(string directory)
        {
            string output;
            int success = Launch(out output, ConstGit, ConstInit, "-q", directory);

            if (success != 0)
                return false;
            if (output.Length > 0)
                return false;

            return true;
        }

        private static void ConfigSetLocal(string key, string value)
        {
            string output;
            Launch(out output, ConstGit, ConstConfig, "--local", "--replace-all", ConstBaseConfigName + key, value);
        }

        private static void ConfigSetLocal(string key, int value)
        {
            ConfigSetLocal(key, Convert.ToString(value));
        }

        private static void ConfigSetLocal(string key, bool value)
        {
            ConfigSetLocal(key, Convert.ToString(value));
        }

        private static string ConfigGetLocal(string key)
        {
            string output;
            int success = Launch(out output, ConstGit, ConstConfig, "--local", "--get", ConstBaseConfigName + key);
            if (success != 0)
                return null;

            return ReadFirstLine(output);
        }

        private static int ConfigGetLocalInt(string key)
        {
            string value = ConfigGetLocal(key);
            if (value == null)
                return -1;

            int result = Convert.ToInt32(value);
            return result;
        }

        private static bool ConfigGetLocalBool(string key)
        {
            string value = ConfigGetLocal(key);
            if (value == null)
                return false;

            bool result = Convert.ToBoolean(value);
            return result;
        }

        public static void ConfigSetLastChangeset(int lastChangeset)
        {
            ConfigSetLocal(ConstConfigLastChangeset, lastChangeset);
        }

        public static int ConfigGetLastChangeset()
        {
            return ConfigGetLocalInt(ConstConfigLastChangeset);
        }

        public static void ConfigSetTagChangeset(bool tagChangeset)
        {
            ConfigSetLocal(ConstConfigTagChangeset, tagChangeset);
        }

        public static bool ConfigGetTagChangeset()
        {
            return ConfigGetLocalBool(ConstConfigTagChangeset);
        }

        public static void ConfigSetDeepMode(bool deepMode)
        {
            ConfigSetLocal(ConstConfigDeepMode, deepMode);
        }

        public static bool ConfigGetDeepMode()
        {
            return ConfigGetLocalBool(ConstConfigDeepMode);
        }

        public static void ConfigSetProjectCollection(string projectCollection)
        {
            ConfigSetLocal(ConstConfigProjectCollection, projectCollection);
        }

        public static string ConfigGetProjectCollection()
        {
            return ConfigGetLocal(ConstConfigProjectCollection);
        }

        public static void ConfigSetServerPath(string serverPath)
        {
            ConfigSetLocal(ConstConfigServerPath, serverPath);
        }

        public static string ConfigGetServerPath()
        {
            return ConfigGetLocal(ConstConfigServerPath);
        }

        public static string GetLastCommit()
        {
            string value;
            int success = Launch(out value, ConstGit, ConstLog, "--format=\"%H\"", "-n", "1");
            if (success != 0)
                return null;

            return ReadFirstLine(value);
        }

        public static void ConfigSetLastCommit(string commit)
        {
            ConfigSetLocal(ConstLastCommit, commit);
        }

        public static string ConfigGetLastCommit()
        {
            return ConfigGetLocal(ConstLastCommit);
        }

        public static bool GetRootPath(out string rootPath)
        {
            int succes = Launch(out rootPath, ConstGit, ConstRevParse, "--show-toplevel");

            return succes == 0;
        }
    }
}