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
        public override bool Proceed(string[] args)
        {
            Debug.Assert(args.Any() && String.Equals(args[0], Commands.ConstClone, StringComparison.OrdinalIgnoreCase));
            args = args.Skip(1).ToArray();

            bool? isDeep = null;
            bool? withTag = null;
            string projectcollection = null;
            string serverpath = null;
            string directory = null;

            int targetCount = 0;
            while (args.Any())
            {
                string arg = args[0];
                if (arg.StartsWith("--", StringComparison.OrdinalIgnoreCase))
                {
                    switch (arg)
                    {
                        case "--deep":
                            isDeep = isDeep ?? true;
                            break;

                        case "--shallow":
                            isDeep = isDeep ?? false;
                            break;

                        case "--tag":
                            withTag = withTag ?? true;
                            break;

                        case "--no-tag":
                            withTag = withTag ?? false;
                            break;

                        default:
                            ShowHelp();
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
                            ShowHelp();
                            return false;
                    }
                    targetCount++;
                }
                args = args.Skip(1).ToArray();
            }

            if (String.IsNullOrEmpty(projectcollection)
                || String.IsNullOrEmpty(serverpath))
            {
                ShowHelp();
                return false;
            }

            isDeep = isDeep ?? true;    // default value
            withTag = withTag ?? true;  // default value
            directory = directory ?? "./" + serverpath.Split('/', '\\').LastOrDefault();
            directory = Path.GetFullPath(directory);

            Output.WriteLine("deep : " + isDeep);
            Output.WriteLine("tag : " + withTag);
            Output.WriteLine("projectcollection : " + projectcollection);
            Output.WriteLine("serverpath : " + serverpath);
            Output.WriteLine("directory : " + directory);

#warning TODO ALPHA point
            return false;
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