using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitfs.Engine
{
    internal abstract class Command
    {
        public abstract bool Proceed(string[] args);

        public abstract void ShowHelp();
    }
}