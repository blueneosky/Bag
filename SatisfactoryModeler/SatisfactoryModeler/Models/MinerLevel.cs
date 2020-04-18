using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Models
{
    public enum MinerLevel
    {
        Mk_1 = 1,
        Mk_2 = 2,
        Mk_3 = 3,
    }

    public static class MinerLevelExtensions
    {
        public static double ToFactor(this MinerLevel value)
                => Math.Pow(2, (int)value - 1);
    }
}
