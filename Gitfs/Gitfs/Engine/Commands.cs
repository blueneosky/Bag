using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitfs.Engine
{
    internal static class Commands
    {
        public static Help Help = new Help();
        public static Clone Clone = new Clone();
        public static Pull Pull = new Pull();

        private static Dictionary<string, Command> _commandByTexts = new Dictionary<string, Command>
        {
            { "help",  Help  },
            { "clone", Clone },
            { "pull",  Pull  },
        };

        public static Command ObtenirCommand(string command)
        {
            Command result;
            if (_commandByTexts.TryGetValue(command, out result))
                return result;

            return null;
        }
    }
}