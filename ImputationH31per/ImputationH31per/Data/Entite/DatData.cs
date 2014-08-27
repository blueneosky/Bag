using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Data.Entite
{
    public class DatData : IDatData<DatTacheTfs, DatIHFormParametre>
    {
        public DatData()
        {
            TacheTfss = new DatTacheTfs[0];
            IHFormParametres = new DatIHFormParametre[0];
        }

        public IEnumerable<DatIHFormParametre> IHFormParametres { get; set; }

        public IEnumerable<DatTacheTfs> TacheTfss { get; set; }
    }
}