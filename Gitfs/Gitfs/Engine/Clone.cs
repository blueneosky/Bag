using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitfs.Engine
{
    internal class Clone : Command
    {
        public override bool Proceed(string[] args)
        {
#warning TODO ALPHA point
            return false;
        }

        public override void ShowHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine("usage: git-tfs clone [--bare] [--deep--shallow] <projectcollection> <serverpath> [<directory>]")
                .AppendLine("")
                .AppendLine("Arguments:")
                .AppendLine("    --bare                Creates a \"bare\" git repository. The directory created will be the git repository itself, instead of creating a working directory with a .git directory underneath (Beta)")
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