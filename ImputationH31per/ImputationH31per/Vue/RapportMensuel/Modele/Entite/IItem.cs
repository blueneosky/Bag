using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public interface IItem<out T>
    {
        T Entite { get; }

        string Libelle { get; }

        EnumTypeItem TypeItem { get; }
    }
}