using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gitfs.Util;

namespace Gitfs.Engine
{
    internal class Help : Command
    {
        public override bool Proceed(string[] args)
        {
            string firstArg = args.FirstOrDefault();

            if (String.Equals(firstArg, "help", StringComparison.OrdinalIgnoreCase))
            {
                string[] helpArgs = args.Skip(1).ToArray();
                if (helpArgs.Any())
                {
                    string secondArgs = (helpArgs.FirstOrDefault() ?? String.Empty).ToLowerInvariant();
                    Command command = Commands.ObtenirCommand(secondArgs);
                    if (command != null)
                    {
                        command.ShowHelp();
                        return true; ;
                    }
                }
            }

            ShowGlobalHelp();

            return true;
        }

        public void ShowGlobalHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine(@"usage: git-tfs [<command...>]")
                .AppendLine(@"")
                .AppendLine(@"The git-tfs commands are:")
                .AppendLine(@"   help       Displays usage information")
                .AppendLine(@"   clone      Initializes a git repository from a TFS path")
                .AppendLine(@"   pull       Pulls the latest code from TFS and merge/rebase the changes into master");
            Output.Write(sb.ToString());
        }

        public override void ShowHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine(@"usage: git-tfs HELP [<command...>]")
                .AppendLine(@"")
                .AppendLine(@"The git-tfs commands are:")
                .AppendLine(@"   help")
                .AppendLine(@"   clone")
                .AppendLine(@"   pull");
            Output.Write(sb.ToString());
        }
    }
}