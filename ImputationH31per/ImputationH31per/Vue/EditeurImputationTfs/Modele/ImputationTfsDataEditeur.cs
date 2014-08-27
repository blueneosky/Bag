using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.EditeurImputationTfs.Modele
{
    public class ImputationTfsDataEditeur : ImputationTfsData
    {
        #region Constantes

        protected static DateTimeOffset ConstanteDateHorodatageDefaut = DateTimeOffset.MinValue;
        private const int ConstanteNumeroImputationTfsInconnu = -1;

        #endregion Constantes

        #region Membres

        private readonly int? _numero;

        private bool _estCommentaireModifiable;
        private bool _estDateEstimCourantModifiable;
        private bool _estDateSommeConsommeeModifiable;
        private bool _estEstimCourantModifiable;
        private bool _estEstTacheAvecEstimModifiable;
        private bool _estImputationTfsValide;
        private bool _estNomComplementaireModifiable;
        private bool _estNomGroupementModifiable;
        private bool _estNomModifiable;
        private bool _estSommeConsommeeModifiable;

        #endregion Membres

        #region ctor

        public ImputationTfsDataEditeur(int? numero, int? numeroComplentaire)
            : base(numero ?? ConstanteNumeroImputationTfsInconnu, numeroComplentaire, ConstanteDateHorodatageDefaut)
        {
            _numero = numero;
            MiseAJour();
        }

        public ImputationTfsDataEditeur(IInformationTicketTfs ticketTfs)
            : base(ticketTfs, ConstanteDateHorodatageDefaut)
        {
            _numero = ticketTfs.Numero;
            MiseAJour();
        }

        #endregion ctor

        #region Propriétés

        #region Overrides

        public override DateTimeOffset? DateEstimCourant
        {
            get
            {
                if (false == EstTacheAvecEstim)
                    return null;
                return base.DateEstimCourant;
            }
            set { base.DateEstimCourant = value; }
        }

        public override double? EstimCourant
        {
            get
            {
                if (false == EstTacheAvecEstim)
                    return null;
                return base.EstimCourant;
            }
            set
            {
                base.EstimCourant = value;
                MiseAJourEstDateEstimCourantModifiable(EstTacheAvecEstim, EstimCourant);
            }
        }

        public override bool EstTacheAvecEstim
        {
            get { return base.EstTacheAvecEstim; }
            set
            {
                base.EstTacheAvecEstim = value;
                MiseAJourEstEstimCourantModifiable(EstTacheAvecEstim);
            }
        }

        public override double? SommeConsommee
        {
            get { return base.SommeConsommee; }
            set
            {
                base.SommeConsommee = value;
                MiseAJourEstDateSommeConsommeeModifiable(SommeConsommee);
            }
        }

        #endregion Overrides

        public bool EstCommentaireModifiable
        {
            get { return _estCommentaireModifiable; }
            set
            {
                if (_estCommentaireModifiable == value)
                    return;
                _estCommentaireModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstCommentaireModifiable));
            }
        }

        public bool EstDateEstimCourantModifiable
        {
            get { return _estDateEstimCourantModifiable; }
            private set
            {
                if (_estDateEstimCourantModifiable == value)
                    return;
                _estDateEstimCourantModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstDateEstimCourantModifiable));
            }
        }

        public bool EstDateSommeConsommeeModifiable
        {
            get { return _estDateSommeConsommeeModifiable; }
            private set
            {
                if (_estDateSommeConsommeeModifiable == value)
                    return;
                _estDateSommeConsommeeModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstDateSommeConsommeeModifiable));
            }
        }

        public bool EstEstimCourantModifiable
        {
            get { return _estEstimCourantModifiable; }
            private set
            {
                if (_estEstimCourantModifiable == value)
                    return;
                _estEstimCourantModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstEstimCourantModifiable));
            }
        }

        public bool EstEstTacheAvecEstimModifiable
        {
            get { return _estEstTacheAvecEstimModifiable; }
            private set
            {
                if (_estEstTacheAvecEstimModifiable == value)
                    return;
                _estEstTacheAvecEstimModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstEstTacheAvecEstimModifiablet));
            }
        }

        public bool EstImputationTfsValide
        {
            get { return _estImputationTfsValide; }
            private set
            {
                if (_estImputationTfsValide == value)
                    return;
                _estImputationTfsValide = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstImputationTfsValide));
            }
        }

        public bool EstNomComplementaireModifiable
        {
            get { return _estNomComplementaireModifiable; }
            private set
            {
                if (_estNomComplementaireModifiable == value)
                    return;
                _estNomComplementaireModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstNomComplementaireModifiable));
            }
        }

        public bool EstNomGroupementModifiable
        {
            get { return _estNomGroupementModifiable; }
            private set
            {
                if (_estNomGroupementModifiable == value)
                    return;
                _estNomGroupementModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstNomGroupementModifiable));
            }
        }

        public bool EstNomModifiable
        {
            get { return _estNomModifiable; }
            private set
            {
                if (_estNomModifiable == value)
                    return;
                _estNomModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstNomModifiable));
            }
        }

        public bool EstSommeConsommeeModifiable
        {
            get { return _estSommeConsommeeModifiable; }
            private set
            {
                if (_estSommeConsommeeModifiable == value)
                    return;
                _estSommeConsommeeModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteImputationTfsDataEditeur.ConstanteProprieteEstSommeConsommeeModifiable));
            }
        }

        #endregion Propriétés

        #region Méthodes privées

        private void MiseAJour()
        {
            bool estTacheAvecEstim = EstTacheAvecEstim;
            int? numero = _numero;
            int? numeroComplementaire = NumeroComplementaire;
            double? estimCourant = EstimCourant;
            double? sommeConsommee = SommeConsommee;

            MiseAJourEstImputationTfsValide(numero);
            MiseAJourEstNomModifiable();
            MiseAJourEstNomComplementaireModifiable(numeroComplementaire);
            MiseAJourEstNomGroupementModifiable();
            MiseAJourEstCommentaireModifiable();
            MiseAJourEstEstTacheAvecEstimModifiable();
            MiseAJourEstEstimCourantModifiable(estTacheAvecEstim);
            MiseAJourEstDateEstimCourantModifiable(estTacheAvecEstim, estimCourant);
            MiseAJourEstSommeConsommeeModifiable();
            MiseAJourEstDateSommeConsommeeModifiable(sommeConsommee);
        }

        private void MiseAJourEstCommentaireModifiable()
        {
            EstCommentaireModifiable = true;
        }

        private void MiseAJourEstDateEstimCourantModifiable(bool estTacheAvecEstim, double? estimCourant)
        {
            EstDateEstimCourantModifiable = estTacheAvecEstim && estimCourant.HasValue;
        }

        private void MiseAJourEstDateSommeConsommeeModifiable(double? sommeConsommee)
        {
            EstDateSommeConsommeeModifiable = sommeConsommee.HasValue;
        }

        private void MiseAJourEstEstimCourantModifiable(bool estTacheAvecEstim)
        {
            EstEstimCourantModifiable = estTacheAvecEstim;
            MiseAJourEstDateEstimCourantModifiable(estTacheAvecEstim, EstimCourant);
        }

        private void MiseAJourEstEstTacheAvecEstimModifiable()
        {
            EstEstTacheAvecEstimModifiable = true;
        }

        private void MiseAJourEstImputationTfsValide(int? numero)
        {
            EstImputationTfsValide = numero.HasValue;
        }

        private void MiseAJourEstNomComplementaireModifiable(int? numeroComplementaire)
        {
            EstNomComplementaireModifiable = numeroComplementaire.HasValue;
        }

        private void MiseAJourEstNomGroupementModifiable()
        {
            EstNomGroupementModifiable = true;
        }

        private void MiseAJourEstNomModifiable()
        {
            EstNomModifiable = true;
        }

        private void MiseAJourEstSommeConsommeeModifiable()
        {
            EstSommeConsommeeModifiable = true;
        }

        #endregion Méthodes privées

        private static ImputationTfsDataEditeur _vide;

        public static ImputationTfsDataEditeur Vide
        {
            get
            {
                if (_vide == null)
                    _vide = new ImputationTfsDataEditeur(null, null);

                return _vide;
            }
        }

        public static new ImputationTfsDataEditeur Copier(IInformationImputationTfs imputation)
        {
            ImputationTfsDataEditeur resultat = new ImputationTfsDataEditeur(imputation);
            resultat.DefinirProprietes(imputation);

            return resultat;
        }
    }
}