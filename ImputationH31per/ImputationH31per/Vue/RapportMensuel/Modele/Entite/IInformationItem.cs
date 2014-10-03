using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public interface IInformationItem<out TInformationTacheTfs> : IItem
        where TInformationTacheTfs : IInformationTacheTfs
    {
        TInformationTacheTfs Information { get; }
    }
}