using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public abstract class InformationBaseItem<TInformationTacheTfs> : BaseItem<TInformationTacheTfs>, IInformationItem<TInformationTacheTfs>
          where TInformationTacheTfs : IInformationTacheTfs
    {
        protected InformationBaseItem(EnumTypeItem typeItem)
            : base(typeItem)
        {
        }

        protected InformationBaseItem(TInformationTacheTfs information)
            : base(information)
        {
        }

        public TInformationTacheTfs Information
        {
            get { return Entite; }
        }
    }
}