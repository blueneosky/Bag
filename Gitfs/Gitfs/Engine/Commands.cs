using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitfs.Engine
{
    internal static class Commands
    {
        public const string ConstHelp = "help";
        public const string ConstClone = "clone";
        public const string ConstPull = "pull";

        public static Help Help = new Help();
        public static Clone Clone = new Clone();
        public static Pull Pull = new Pull();

        private static Dictionary<string, Command> _commandByTexts = new Dictionary<string, Command>
        {
            { ConstHelp , Help  },
            { ConstClone, Clone },
            { ConstPull , Pull  },
        };

        public static Command ObtenirCommand(string command)
        {
            command = command.ToLowerInvariant();
            Command result;
            if (_commandByTexts.TryGetValue(command, out result))
                return result;

            return null;
        }
    }
}