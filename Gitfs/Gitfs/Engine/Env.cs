﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Gitfs.Util;

namespace Gitfs.Engine
{
    internal static class Env
    {
        private static bool? _verboseMode = null;
        private static bool? _deepMode = null;
        private static bool? _withTag = null;
        private static string _projectcollection = null;
        private static string _serverpath = null;
        private static bool? _dryRune = null;

        private static TfsTeamProjectCollection _tfsProjectCollection;
        private static VersionControlServer _versionControlServer;

        public static bool DryRunMode
        {
            get { return _dryRune.Value; }
            set{ _dryRune = value;}
        }

        public static bool VerboseMode
        {
            get { return _verboseMode.Value; }
            set { _verboseMode = value; }
        }

        public static bool DeepMode
        {
            get { return _deepMode.Value; }
            set { _deepMode = value; }
        }

        public static bool WithTag
        {
            get { return _withTag.Value; }
            set { _withTag = value; }
        }

        public static string Projectcollection
        {
            get { return _projectcollection; }
            set { _projectcollection = value; }
        }

        public static string Serverpath
        {
            get { return _serverpath; }
            set { _serverpath = value; }
        }

        public static int LastChangeset { get; set; }

        public static string VirtualServerPath
        {
            get { return String.Concat(Projectcollection, Serverpath); }
        }

        public static TfsTeamProjectCollection TfsProjectCollection
        {
            get
            {
                if (_tfsProjectCollection == null)
                {
                    Uri uriProjectcollection = new Uri(Env.Projectcollection);
                    _tfsProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uriProjectcollection);
                }
                return _tfsProjectCollection;
            }
        }

        public static VersionControlServer VersionControlServer
        {
            get
            {
                if (_versionControlServer == null)
                {
                    _versionControlServer = TfsProjectCollection.GetService<VersionControlServer>();
                }
                return _versionControlServer;
            }
        }

        public static void LoadFromGitConfig()
        {
            Projectcollection = GitHelper.ConfigGetProjectCollection();
            Serverpath = GitHelper.ConfigGetServerPath();
            WithTag = GitHelper.ConfigGetTagChangeset();
            DeepMode = GitHelper.ConfigGetDeepMode();
            LastChangeset = GitHelper.ConfigGetLastChangeset();
        }
    }
}