using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Data.Entite
{
    public interface IDatData<out TDatTacheTfs, out TDatIHFormParametre>
        where TDatTacheTfs : IDatTacheTfs
        where TDatIHFormParametre : IDatIHFormParametre
    {
        IEnumerable<TDatIHFormParametre> IHFormParametres { get; }

        IEnumerable<TDatTacheTfs> TacheTfss { get; }
    }
}