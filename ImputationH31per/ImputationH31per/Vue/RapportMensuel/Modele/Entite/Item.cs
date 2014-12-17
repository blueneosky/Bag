using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class Item<T> : BaseItem<T>
    {
        public Item(EnumTypeItem typeItem)
            : base(typeItem)
        {
        }

        public Item(T entite)
            : base(entite)
        {
        }

        protected override string ObtenirLibelleEntite()
        {
            return "" + Entite;
        }
    }
}