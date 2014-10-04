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

        public static GroupeItem Tous = new GroupeItem(EnumTypeItem.Tous);
        public static GroupeItem Aucun = new GroupeItem(EnumTypeItem.Aucun);
    }
}