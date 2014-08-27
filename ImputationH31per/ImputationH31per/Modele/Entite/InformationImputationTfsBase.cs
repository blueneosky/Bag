using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public abstract class InformationImputationTfsBase : InformationTicketTfsBase, IInformationImputationTfsNotifiable
    {
        #region Membres

        private readonly DateTimeOffset _dateHorodatage;

        private DateTimeOffset? _dateEstimCourant;
        private DateTimeOffset? _dateSommeConsommee;
        private double? _estimCourant;
        private double? _sommeConsommee;
        private string _commentaire;

        #endregion Membres

        #region ctor

        protected InformationImputationTfsBase(IInformationTicketTfs ticketTfs, DateTimeOffset dateHorodatage)
            : this(ticketTfs.Numero, ticketTfs.NumeroComplementaire, dateHorodatage)
        {
        }

        protected InformationImputationTfsBase(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
            : base(numero, numeroComplementaire)
        {
            _dateHorodatage = dateHorodatage;
        }

        #endregion ctor

        #region Propriétés

        #region Identification

        public virtual DateTimeOffset DateHorodatage
        {
            get { return _dateHorodatage; }
        }

        #endregion Identification

        public virtual DateTimeOffset? DateEstimCourant
        {
            get { return _dateEstimCourant; }
            set
            {
                if (_dateEstimCourant == value)
                    return;
                _dateEstimCourant = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationImputationTfsNotifiable.ConstanteProprieteDateEstimCourant));
            }
        }

        public virtual DateTimeOffset? DateSommeConsommee
        {
            get { return _dateSommeConsommee; }
            set
            {
                if (_dateSommeConsommee == value)
                    return;
                _dateSommeConsommee = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationImputationTfsNotifiable.ConstanteProprieteDateSommeConsommee));
            }
        }

        public virtual double? EstimCourant
        {
            get { return _estimCourant; }
            set
            {
                if (_estimCourant == value)
                    return;
                _estimCourant = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationImputationTfsNotifiable.ConstanteProprieteEstimCourant));
            }
        }

        public virtual double? SommeConsommee
        {
            get { return _sommeConsommee; }
            set
            {
                if (_sommeConsommee == value)
                    return;
                _sommeConsommee = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationImputationTfsNotifiable.ConstanteProprieteSommeConsommee));
            }
        }

        public virtual string Commentaire
        {
            get { return _commentaire; }
            set
            {
                if (_commentaire == value)
                    return;
                _commentaire = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationImputationTfsNotifiable.ConstanteProprieteCommentaire));
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
            return (base.GetHashCode() * 3) ^ _dateHorodatage.GetHashCode();
        }

        #endregion Overrides
    }
}