using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public abstract class InformationTicketTfsBase : InformationTacheTfsBase, IInformationTicketTfsNotifiable
    {
        #region Membres

        private readonly int? _numeroComplementaire;

        private bool _estTacheAvecEstim;
        private string _nomComplementaire;

        #endregion Membres

        #region ctor

        protected InformationTicketTfsBase(IInformationTacheTfs tacheTfs, int? numeroComplementaire)
            : this(tacheTfs.Numero, numeroComplementaire)
        {
        }

        protected InformationTicketTfsBase(int numero, int? numeroComplementaire)
            : base(numero)
        {
            _numeroComplementaire = numeroComplementaire;
        }

        #endregion ctor

        #region Propriétés

        #region Identification

        public virtual int? NumeroComplementaire
        {
            get { return _numeroComplementaire; }
        }

        #endregion Identification

        public virtual bool EstTacheAvecEstim
        {
            get { return _estTacheAvecEstim; }
            set
            {
                if (_estTacheAvecEstim == value)
                    return;
                _estTacheAvecEstim = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteEstTacheAvecEstim));
            }
        }

        public virtual string NomComplementaire
        {
            get { return _nomComplementaire; }
            set
            {
                if (String.Equals(_nomComplementaire, value))
                    return;
                _nomComplementaire = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteNomComplementaire));
            }
        }

        #endregion Propriétés

        #region Overrides

        // override object.Equals
        public override bool Equals(object obj)
        {
            return Object.ReferenceEquals(this, obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return (base.GetHashCode() * 3) ^ (_numeroComplementaire ?? 0);
        }

        #endregion Overrides
    }
}