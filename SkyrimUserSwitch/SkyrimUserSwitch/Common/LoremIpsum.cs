using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyrimUserSwitch.Common
{
    internal static class LoremIpsum
    {
#if DEBUG
        public const string ConstLoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non risus. Suspendisse lectus tortor, dignissim sit amet, adipiscing nec, ultricies sed, dolor. Cras elementum ultrices diam. Maecenas ligula massa, varius a, semper congue, euismod non, mi.";

        private static IEnumerable<string> _words;

        public static IEnumerable<string> Words
        {
            get
            {
                if (_words == null)
                {
                    _words = ConstLoremIpsum
                        .Split(',', '.', ' ')
                        .Where(w => !String.IsNullOrWhiteSpace(w) && w.Length > 3)
                        .Distinct()
                        .ToArray();
                }
                return _words;
            }
        }

#endif
    }
}