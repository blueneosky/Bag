using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Gitfs.Engine
{
    internal class Clone : Command
    {
        bool? _isDeep = null;
        bool? _withTag = null;
        string _projectcollection = null;
        string _serverpath = null;
        string _directory = null;

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

            Output.WriteLine("deep : " + _isDeep);
            Output.WriteLine("tag : " + _withTag);
            Output.WriteLine("projectcollection : " + _projectcollection);
            Output.WriteLine("serverpath : " + _serverpath);
            Output.WriteLine("directory : " + _directory);

#warning TODO ALPHA point
            return true;

        }

        private bool ExtractArgs(ref string[] args)
        {
            int targetCount = 0;
            while (args.Any())
            {
                string arg = args[0];
                if (arg.StartsWith("--", StringComparison.OrdinalIgnoreCase))
                {
                    switch (arg)
                    {
                        case "--deep":
                            _isDeep = _isDeep ?? true;
                            break;

                        case "--shallow":
                            _isDeep = _isDeep ?? false;
                            break;

                        case "--tag":
                            _withTag = _withTag ?? true;
                            break;

                        case "--no-tag":
                            _withTag = _withTag ?? false;
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
                            _projectcollection = arg;
                            break;

                        case 1:
                            _serverpath = arg;
                            break;

                        case 2:
                            _directory = arg;
                            break;

                        default:
                            return false;
                    }
                    targetCount++;
                }
                args = args.Skip(1).ToArray();
            }

            if (String.IsNullOrEmpty(_projectcollection)
                || String.IsNullOrEmpty(_serverpath))
            {
                return false;
            }

            _isDeep = _isDeep ?? true;    // default value
            _withTag = _withTag ?? true;  // default value
            _directory = _directory ?? "./" + _serverpath.Split('/', '\\').LastOrDefault();
            _directory = Path.GetFullPath(_directory);

            return true;
        }

        public override void ShowHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine("usage: git-tfs clone [--deep|--shallow] <projectcollection> <serverpath> [<directory>]")
                .AppendLine("")
                .AppendLine("Arguments:")
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