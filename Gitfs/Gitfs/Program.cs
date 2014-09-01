using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gitfs.Engine;
using Gitfs.Util;

namespace Gitfs
{
    class Program
    {
        static int Main(string[] args)
        {
#if DEBUG
            //Console.ReadLine();
            Env.Projectcollection = "http://obestfsp01:8080/";
            Env.Serverpath = "$/HEO";
            Env.VerboseMode = true;

            //args = "clone -v http://obestfsp01:8080/ $/HEO F:/HEO"
            //.Split(' ');
            return Commands.Pull.Proceed() ? 0 : -1;
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
