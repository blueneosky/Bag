using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Data.Entite
{
    public interface IDatTacheTfs : ITacheTfs<IDatTicketTfs, IDatImputationTfs>
    {
    }
}