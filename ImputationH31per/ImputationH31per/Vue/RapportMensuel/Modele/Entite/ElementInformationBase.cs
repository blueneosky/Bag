using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public abstract class ElementInformationBase<TInformationTacheTfs> : ElemntBase, IElementInformation<TInformationTacheTfs>
          where TInformationTacheTfs : IInformationTacheTfs
    {
        #region Membres

        private readonly TInformationTacheTfs _information;

        #endregion Membres

        #region ctor

        public ElementInformationBase(TInformationTacheTfs information)
        {
            _information = information;
        }

        #endregion ctor

        #region IElementInformation<TInformationTacheTfs> Membres

        public TInformationTacheTfs Information
        {
            get { return _information; }
        }

        #endregion IElementInformation<TInformationTacheTfs> Membres

    }
}