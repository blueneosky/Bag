using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class TicketItem : InformationBaseItem<IInformationTicketTfs>
    {
        public TicketItem(EnumTypeItem typeItem)
            : base(typeItem)
        {
        }

        public TicketItem(IInformationImputationTfs information)
            : base(information)
        {
        }

        protected override string ObtenirLibelleEntite()
        {
            return String.Format("{0} : {1}", Information.NumeroComplet(), Information.NomComplet());
        }

        public static readonly TicketItem Tous = new TicketItem(EnumTypeItem.Tous);
        public static readonly TicketItem Aucun = new TicketItem(EnumTypeItem.Aucun);
    }
}