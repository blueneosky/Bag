using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Gitfs.Util;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Gitfs.Engine
{
    internal class Clone : Command
    {

        public string _directory = null;


        private bool VerboseMode { get { return Env.VerboseMode; } }

        public override bool Proceed(string[] args)
        {
            Debug.Assert(args.Any() && String.Equals(args[0], Commands.ConstClone, StringComparison.OrdinalIgnoreCase));
            args = args.Skip(1).ToArray();

            bool success = ExtractArgs(ref args);
            if (false == success)
            {
                ShowHelp();
                return false;
            }

            try
            {
                if (false == Proceed())
                    return false;
            }
            catch (Exception e)
            {
                Output.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        private bool Proceed()
        {
            // connection test
            Output.WriteLine("Get info from " + Env.VirtualServerPath);
            VersionControlServer server = Env.VersionControlServer;
            Output.WriteLine("Ok");  // it's ok (otherwize something throw an exception)

            // init git repository
            bool succes = GitHelper.GitInit(_directory);
            if (false == succes)
            {
                Output.WriteLine("Failed to init git repo...");
                Output.WriteLine(GitHelper.LastOutput);
                return false;
            }
            if (VerboseMode)
                Output.WriteLine(GitHelper.LastOutput);

            Directory.SetCurrentDirectory(_directory);   // must be set before local tfs config writting

            // write local tfs config
            GitHelper.ConfigSetProjectCollection(Env.Projectcollection);
            GitHelper.ConfigSetServerPath(Env.Serverpath);
            GitHelper.ConfigSetTagChangeset(Env.WithTag);
            GitHelper.ConfigSetDeepMode(Env.DeepMode);

            Pull pullCommand = Commands.ObtenirCommand(Commands.ConstPull) as Pull;
            Debug.Assert(pullCommand != null);

            return pullCommand.Proceed();
        }

        private bool ExtractArgs(ref string[] args)
        {
            bool? verboseMode = null;
            bool? deepMode = null;
            bool? withTag = null;
            string projectcollection = null;
            string serverpath = null;
            string directory = null;

            int targetCount = 0;
            while (args.Any())
            {
                string arg = args[0];
                if (arg.StartsWith("-", StringComparison.OrdinalIgnoreCase))
                {
                    switch (arg)
                    {
                        case "-v":
                        case "--verbose":
                            verboseMode = verboseMode ?? true;
                            break;

                        case "--deep":
                            deepMode = deepMode ?? true;
                            break;

                        case "--shallow":
                            deepMode = deepMode ?? false;
                            break;

                        case "--tag":
                            withTag = withTag ?? true;
                            break;

                        case "--no-tag":
                            withTag = withTag ?? false;
                            break;

                        default:
                            return false;
                    }
                }
                else
                {
                    switch (targetCount)
                    {
                        case 0:
                            projectcollection = arg;
                            break;

                        case 1:
                            serverpath = arg;
                            break;

                        case 2:
                            directory = arg;
                            break;

                        default:
                            return false;
                    }
                    targetCount++;
                }
                args = args.Skip(1).ToArray();
            }

            if (String.IsNullOrEmpty(projectcollection)
                || String.IsNullOrEmpty(serverpath))
            {
                return false;
            }

            directory = directory ?? "./" + serverpath.Split('/', '\\').LastOrDefault();

            Env.VerboseMode = verboseMode ?? false;   // default value
            Env.DeepMode = deepMode ?? true;          // default value
            Env.WithTag = withTag ?? true;            // default value
            Env.Projectcollection = projectcollection;
            Env.Serverpath = serverpath;
            _directory = Path.GetFullPath(directory);

            return true;
        }

        public override void ShowHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine("usage: git-tfs clone [--verbose|-v] [--deep|--shallow] <projectcollection> <serverpath> [<directory>]")
                .AppendLine("")
                .AppendLine("Arguments:")
                .AppendLine("    --verbose, -v         Verbose mode (default: none)")
                .AppendLine("    --deep, --shallow")
                .AppendLine("                          Creates a shallow clone of the specified depth, or a deep clone of all TFS changesets, and sets the default depth for pull operations (default: --deep)")
                .AppendLine("    --tag, --no-tag       Determine whether to tag all commits that map to changesets downloaded from TFS (default: --tag)")
                .AppendLine("    <projectcollection>   The TFS project collection URL")
                .AppendLine("    <serverpath>          The TFS server path to clone")
                .AppendLine("    <directory>           The name of a new directory to clone into.  If the directory is not specified, the name of the TFS folder will be used.  If this directory exists, it must be empty")
                .AppendLine("")
                .AppendLine("Clones a path from Microsoft Team Foundation Server, creating a new git repository.")
                ;
            Output.Write(sb.ToString());
        }
    }
}