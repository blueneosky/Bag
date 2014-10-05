using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class TacheItem : InformationBaseItem<IInformationTacheTfs>
    {
        public TacheItem(EnumTypeItem typeItem)
            : base(typeItem)
        {
        }

        public TacheItem(IInformationTacheTfs information)
            : base(information)
        {
        }

        protected override string ObtenirLibelleEntite()
        {
            return String.Format("{0} : {1}", Information.NumeroComplet(), Information.NomComplet());
        }

        protected override bool EntiteEgale(IInformationTacheTfs entite)
        {
            return Entite.Numero == entite.Numero;
        }

        public static readonly TacheItem Tous = new TacheItem(EnumTypeItem.Tous);
    }
}