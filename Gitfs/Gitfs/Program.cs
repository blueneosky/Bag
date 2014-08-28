using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gitfs.Engine;

namespace Gitfs
{
    class Program
    {
        static int Main(string[] args)
        {
#if DEBUG
            args = "clone titi toto"
                .Split(' ');
#endif
            return Proceed(args) ? 0 : -1;
        }

        private static bool Proceed(string[] args)
        {
            string firstArg = args.FirstOrDefault();
            Command command = Commands.ObtenirCommand(firstArg);
            if (command != null)
                return command.Proceed(args);

            return Commands.Help.Proceed(args);
        }

        
    }
}
