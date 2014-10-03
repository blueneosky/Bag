using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class TicketItem : InformationBaseItem<IInformationImputationTfs>
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
    }
}