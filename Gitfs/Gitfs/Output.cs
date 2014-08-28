using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gitfs
{
    internal static class Output
    {
        public static void Write(string text)
        {
            Console.Write(text);
            Debug.Write(text);
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
            Debug.WriteLine(text);
        }
    }
}