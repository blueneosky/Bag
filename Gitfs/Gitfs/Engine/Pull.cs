using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitfs.Engine
{
    internal class Pull : Command
    {
        public override bool Proceed(string[] args)
        {
#warning TODO BETA point
            return false;
        }

        public override void ShowHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine("usage: git-tfs pull [--deep|--shallow]")
                .AppendLine("")
                .AppendLine("Arguments:")
                .AppendLine("    --deep, --shallow     Performs a shallow fetch of the specified depth, or a deep fetch of all TFS changesets since the last fetch. If omitted, the depth value provided during clone or configure is used.")
                .AppendLine("")
                .AppendLine("Pulls the latest code from TFS and merge/rebase the changes into master.")
                ;
        }
    }
}