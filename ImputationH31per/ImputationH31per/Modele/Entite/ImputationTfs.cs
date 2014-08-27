using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele.Entite
{
    public class ImputationTfs : ImputationTfsBase
    {
        #region Membres

        private readonly TicketTfs _ticketTfs;

        #endregion Membres

        #region ctor

        public ImputationTfs(TicketTfs ticketTfs, DateTimeOffset dateHorodatage)
            : base(ticketTfs, dateHorodatage)
        {
            Debug.Assert(ticketTfs != null);

            _ticketTfs = ticketTfs;
            _ticketTfs.AjouterImputationTfs(this);

            new WeakEventHandler<ImputationTfs, PropertyChangedEventArgs, TicketTfs>(
                this
                , NotifierPropertyChanged
                , OnDetachedPropertyChanged
                , OnAttachedPropertyChanged
                );
        }

        #region WeakEventHandler sur _ticketTfs.PropertyChanged

        private static void NotifierPropertyChanged(ImputationTfs imputationTfs, object sender, PropertyChangedEventArgs e)
        {
            imputationTfs.NotifierPropertyChanged(imputationTfs, e);
        }

        private static void OnDetachedPropertyChanged(WeakEventHandler<ImputationTfs, PropertyChangedEventArgs, TicketTfs> weakEventHandler)
        {
            weakEventHandler.Handler.PropertyChanged -= weakEventHandler.OnEvent;
        }

        private TicketTfs OnAttachedPropertyChanged(WeakEventHandler<ImputationTfs, PropertyChangedEventArgs, TicketTfs> weakEventHandler)
        {
            _ticketTfs.PropertyChanged += weakEventHandler.OnEvent;
            return _ticketTfs;
        }

        #endregion WeakEventHandler sur _ticketTfs.PropertyChanged

        #endregion ctor

        #region Propriétés

        public override bool EstTacheAvecEstim
        {
            get { return _ticketTfs.EstTacheAvecEstim; }
            set { _ticketTfs.EstTacheAvecEstim = value; }
        }

        public override string Nom
        {
            get { return _ticketTfs.Nom; }
            set { _ticketTfs.Nom = value; }
        }

        public override string NomComplementaire
        {
            get { return _ticketTfs.NomComplementaire; }
            set { _ticketTfs.NomComplementaire = value; }
        }

        public override string NomGroupement
        {
            get { return _ticketTfs.NomGroupement; }
            set { _ticketTfs.NomGroupement = value; }
        }

        public override int Numero
        {
            get { return _ticketTfs.Numero; }
        }

        public override int? NumeroComplementaire
        {
            get { return _ticketTfs.NumeroComplementaire; }
        }

        #endregion Propriétés

        #region CaparateurDateHorodatage_X

        private static ClasseComparateurDateHorodatageCroissant _comparateurDateHorodatageCroissant;

        private static ClasseComparateurDateHorodatageDecroissant _comparateurDateHorodatageDecroissant;

        public static IComparer<DateTimeOffset> ComparateurDateHorodatageCroissant
        {
            get
            {
                if (_comparateurDateHorodatageCroissant == null)
                    _comparateurDateHorodatageCroissant = new ClasseComparateurDateHorodatageCroissant();
                return _comparateurDateHorodatageCroissant;
            }
        }

        public static IComparer<DateTimeOffset> ComparateurDateHorodatageDecroissant
        {
            get
            {
                if (_comparateurDateHorodatageDecroissant == null)
                    _comparateurDateHorodatageDecroissant = new ClasseComparateurDateHorodatageDecroissant();
                return _comparateurDateHorodatageDecroissant;
            }
        }

        private static int CompareDateHorodatageCroissant(DateTimeOffset x, DateTimeOffset y)
        {
            return x.CompareTo(y);
        }

        private static int CompareDateHorodatageDecroissant(DateTimeOffset x, DateTimeOffset y)
        {
            return y.CompareTo(x);
        }

        private class ClasseComparateurDateHorodatageCroissant : IComparer<DateTimeOffset>
        {
            public int Compare(DateTimeOffset x, DateTimeOffset y)
            {
                return CompareDateHorodatageCroissant(x, y);
            }
        }

        private class ClasseComparateurDateHorodatageDecroissant : IComparer<DateTimeOffset>
        {
            public int Compare(DateTimeOffset x, DateTimeOffset y)
            {
                return CompareDateHorodatageDecroissant(x, y);
            }
        }

        #endregion CaparateurDateHorodatage_X

        #region CaparateurImputationTfs_X

        private static ClasseComparateurImputationTfsCroissant _comparateurImputationTfsCroissant;

        private static ClasseComparateurImputationTfsDecroissant _comparateurImputationTfsDecroissant;

        public static IComparer<IInformationImputationTfs> ComparateurImputationTfsCroissant
        {
            get
            {
                if (_comparateurImputationTfsCroissant == null)
                    _comparateurImputationTfsCroissant = new ClasseComparateurImputationTfsCroissant();
                return _comparateurImputationTfsCroissant;
            }
        }

        public static IComparer<IInformationImputationTfs> ComparateurImputationTfsDecroissant
        {
            get
            {
                if (_comparateurImputationTfsDecroissant == null)
                    _comparateurImputationTfsDecroissant = new ClasseComparateurImputationTfsDecroissant();
                return _comparateurImputationTfsDecroissant;
            }
        }

        private static int CompareImputationTfsCroissant(IInformationImputationTfs x, IInformationImputationTfs y)
        {
            return CompareDateHorodatageCroissant(x.DateHorodatage, y.DateHorodatage);
        }

        private static int CompareImputationTfsDecroissant(IInformationImputationTfs x, IInformationImputationTfs y)
        {
            return CompareDateHorodatageDecroissant(x.DateHorodatage, y.DateHorodatage);
        }

        private class ClasseComparateurImputationTfsCroissant : IComparer<IInformationImputationTfs>
        {
            public int Compare(IInformationImputationTfs x, IInformationImputationTfs y)
            {
                return CompareImputationTfsCroissant(x, y);
            }
        }

        private class ClasseComparateurImputationTfsDecroissant : IComparer<IInformationImputationTfs>
        {
            public int Compare(IInformationImputationTfs x, IInformationImputationTfs y)
            {
                return CompareImputationTfsDecroissant(x, y);
            }
        }

        #endregion CaparateurImputationTfs_X
    }
}