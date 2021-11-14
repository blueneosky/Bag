using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    public enum EnumTileFamily : uint
    {
        None = 0,

        Numbers = 2 * Base,
        Dots = 3 * Base,
        Bamboos = 4 * Base,
        Winds = 5 * Base,
        Dragons = 6 * Base,

        Base = 100,
    }
}
