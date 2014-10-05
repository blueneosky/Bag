using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class GroupeItem : BaseItem<string>
    {
        public GroupeItem(EnumTypeItem typeItem)
            : base(typeItem)
        {
        }

        public GroupeItem(string groupe)
            : base(groupe)
        {
        }

        protected override string ObtenirLibelleEntite()
        {
            return Entite;
        }

        protected override bool EntiteEgale(string entite)
        {
            return String.Equals(Entite, entite);
        }

        public static readonly GroupeItem Tous = new GroupeItem(EnumTypeItem.Tous);
        public static readonly GroupeItem Aucun = new GroupeItem(EnumTypeItem.Aucun);
    }
}