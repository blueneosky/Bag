using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class ElementTache : ElementInformationBase<IInformationTacheTfs>
    {
        #region ctor

        public ElementTache(IInformationTacheTfs information)
            : base(information)
        {
        }

        #endregion ctor

        #region ElementInformationBase

        public override string Libelle
        {
            get { return Information.NomGroupement; }
        }

        #endregion ElementInformationBase
    }
}