using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public abstract class ElemntBase : IElement
    {
        #region IElement Membres

        public abstract string Libelle { get; }

        #endregion IElement Membres
    }

#warning TODO - penser à implémenter All et Any
}