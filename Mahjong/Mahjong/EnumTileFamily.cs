using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    enum EnumTileFamily : uint
    {
        None = 0,

        Numbers = 1 * Base,
        Dots = 2 * Base,
        Bamboos = 3 * Base,
        Winds = 4 * Base,
        Dragons = 5 * Base,

        Base = 100,
    }
}
