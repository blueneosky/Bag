using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.Common
{
    public static class Tools
    {
        private const int ConstNbSpaceForTab = 4;
        private static List<string> _nbTabCahe = new List<string> { string.Empty };
        private static string ConstBaseTab = String.Concat(Enumerable.Repeat(" ", ConstNbSpaceForTab));

        public static string GetTab(int nbTab)
        {
            string result;

            if (nbTab < _nbTabCahe.Count)
            {
                result = _nbTabCahe[nbTab];
            }
            else
            {
                result = GetTabCached(nbTab);
            }

            return result;
        }

        private static string GetTabCached(int nbTab)
        {
            string result = null;
            for (int i = _nbTabCahe.Count; i <= nbTab; i++)
            {
                result = _nbTabCahe[i - 1] + ConstBaseTab;
                _nbTabCahe.Add(result);
            }

            return result;
        }
    }
}