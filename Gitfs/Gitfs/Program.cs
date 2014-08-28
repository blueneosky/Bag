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
            args = "help clone"
                .Split(' ');
            return Proceed(args) ? 0 : -1;
        }

        private static bool Proceed(string[] args)
        {
            // TODO : help, clone and pull


            return Commands.Help.Proceed(args);
        }

        
    }
}
