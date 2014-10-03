using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class TacheItem : InformationBaseItem<IInformationTacheTfs>
    {
        #region ctor

        public TacheItem(IInformationTacheTfs information)
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