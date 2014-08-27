using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ImputationH31per.Data;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using IImputationTfsModele = ImputationH31per.Modele.Entite.IImputationTfsNotifiable;
using ITacheTfsModele = ImputationH31per.Modele.Entite.ITacheTfsNotifiable<ImputationH31per.Modele.Entite.ITicketTfsNotifiable<ImputationH31per.Modele.Entite.IImputationTfsNotifiable>, ImputationH31per.Modele.Entite.IImputationTfsNotifiable>;
using ITicketTfsModele = ImputationH31per.Modele.Entite.ITicketTfsNotifiable<ImputationH31per.Modele.Entite.IImputationTfsNotifiable>;

namespace ImputationH31per.Modele
{
#warning TODO - faire vérification avant ajout d'imputation {datehorodatage, dateestim, dateconsomé  } > imput précédente

    public class ImputationH31perModele : IImputationH31perModele
    {
        #region Membres

        private readonly SortedObservableCollection<IdImputationTfs, ImputationTfs> _imputationTfsParIdImputationTfss;
        private readonly ObservableDictionary<int, TacheTfs> _tacheTfsParIdTacheTfss;
        private readonly ObservableDictionary<IdTicketTfs, TicketTfs> _ticketTfsParIdTicketTfss;

        private IServiceData _dataActive;
        private bool _estModifie;

        #endregion Membres

        #region ctor

        public ImputationH31perModele()
        {
            _tacheTfsParIdTacheTfss = new ObservableDictionary<int, TacheTfs>();
            _ticketTfsParIdTicketTfss = new ObservableDictionary<IdTicketTfs, TicketTfs>();
            _imputationTfsParIdImputationTfss = new SortedObservableCollection<IdImputationTfs, ImputationTfs>(IdImputationTfs.ComparateurDecroissant);

            _tacheTfsParIdTacheTfss.CollectionChanged += _tacheTfsParIdTacheTfss_CollectionChanged;
            _ticketTfsParIdTicketTfss.CollectionChanged += _ticketTfsParIdTicketTfss_CollectionChanged;
            _imputationTfsParIdImputationTfss.CollectionChanged += _imputationTfsParIdImputationTfss_CollectionChanged;
        }

        ~ImputationH31perModele()
        {
            _tacheTfsParIdTacheTfss.CollectionChanged -= _tacheTfsParIdTacheTfss_CollectionChanged;
            _ticketTfsParIdTicketTfss.CollectionChanged -= _ticketTfsParIdTicketTfss_CollectionChanged;
            _imputationTfsParIdImputationTfss.CollectionChanged -= _imputationTfsParIdImputationTfss_CollectionChanged;
        }

        #endregion ctor

        #region Propriétés

        public bool EstModifie
        {
            get { return _estModifie; }
            private set
            {
                if (_estModifie == value)
                    return;
                _estModifie = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIImputationH31perModele.ConstanteProprieteEstModifie));
            }
        }

        public IEnumerable<IImputationTfsModele> ImputationTfss
        {
            get { return _imputationTfsParIdImputationTfss.Values; }
        }

        public IServiceData ServiceDataActif
        {
            get { return _dataActive; }
            set
            {
                if (_dataActive == value)
                    return;
                _dataActive = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIImputationH31perModele.ConstanteProprieteServiceDataActif));
            }
        }

        public IEnumerable<ITacheTfsModele> TacheTfss
        {
            get { return _tacheTfsParIdTacheTfss.Values; }
        }

        public IEnumerable<ITicketTfsModele> TicketTfss
        {
            get { return _ticketTfsParIdTicketTfss.Values; }
        }

        #endregion Propriétés

        #region Evennements

        public event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        public event PropertyChangedEventHandler PropertyChanged;

        public event NotifyCollectionChangedEventHandler TacheTfssAChange;

        public event NotifyCollectionChangedEventHandler TicketTfssAChange;

        protected virtual void NotifierImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            ImputationTfssAChange.Notifier(sender, e);
        }

        protected virtual void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notifier(sender, e);
        }

        protected virtual void NotifierTacheTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            TacheTfssAChange.Notifier(sender, e);
        }

        protected virtual void NotifierTicketTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            TicketTfssAChange.Notifier(sender, e);
        }

        #endregion Evennements

        #region Méthodes

        public bool ContientImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
        {
            IdImputationTfs idImputationTfs = ObtenirIdImputationTfs(numero, numeroComplementaire, dateHorodatage);
            bool resultat = ContientImputationTfsCore(idImputationTfs);

            return resultat;
        }

        public bool ContientTacheTfs(int numero)
        {
            return ContientTacheTfsCore(numero);
        }

        public bool ContientTicketTfs(int numero, int? numeroComplementaire)
        {
            IdTicketTfs idTicketTfs = ObtenirIdTicketTfs(numero, numeroComplementaire);
            bool resultat = ContientTicketTfsCore(idTicketTfs);

            return resultat;
        }

        public IImputationTfsModele CreerImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
        {
            IdImputationTfs idImputationTfs = ObtenirIdImputationTfs(numero, numeroComplementaire, dateHorodatage);
            IImputationTfsModele resultat = CreerImputationTfsCore(idImputationTfs);

            return resultat;
        }

        public IImputationTfsModele ObtenirImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
        {
            IdImputationTfs idImputationTfs = ObtenirIdImputationTfs(numero, numeroComplementaire, dateHorodatage);
            ImputationTfs resultat;
            ObtenirImputationTfsCore(idImputationTfs, out resultat);

            return resultat;
        }

        public ITacheTfsModele ObtenirTacheTfs(int numero)
        {
            TacheTfs resultat;
            ObtenirTacheTfsCore(numero, out resultat);

            return resultat;
        }

        public ITicketTfsModele ObtenirTicketTfs(int numero, int? numeroComplementaire)
        {
            IdTicketTfs idTicketTfs = ObtenirIdTicketTfs(numero, numeroComplementaire);
            TicketTfs resultat;
            ObtenirTicketTfsCore(idTicketTfs, out resultat);

            return resultat;
        }

        public void ReinitialisetEstModifier()
        {
            ResetEstModifie();
        }

        public bool SupprimerImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
        {
            IdImputationTfs idImputationTfs = ObtenirIdImputationTfs(numero, numeroComplementaire, dateHorodatage);
            bool resultat = SupprimerImputationTfsCore(idImputationTfs);

            return resultat;
        }

        public void ViderImputationTfss()
        {
            ViderImputationTfssCore();
        }

        #endregion Méthodes

        #region Core

        private bool ContientImputationTfsCore(IdImputationTfs idImputationTfs)
        {
            return _imputationTfsParIdImputationTfss.ContainsKey(idImputationTfs);
        }

        private bool ContientTacheTfsCore(int numero)
        {
            return _tacheTfsParIdTacheTfss.ContainsKey(numero);
        }

        private bool ContientTicketTfsCore(IdTicketTfs idicketTfs)
        {
            return _ticketTfsParIdTicketTfss.ContainsKey(idicketTfs);
        }

        private ImputationTfs CreerEtInsererImputationTfsCore(TicketTfs ticketTfs, IdImputationTfs idImputationTfs)
        {
            ImputationTfs imputationTfs = new ImputationTfs(ticketTfs, idImputationTfs.DateHorodatage);
            _imputationTfsParIdImputationTfss.Add(idImputationTfs, imputationTfs);

            return imputationTfs;
        }

        private TacheTfs CreerEtInsererTacheTfsCore(int numero)
        {
            TacheTfs tacheTfs = new TacheTfs(numero);
            _tacheTfsParIdTacheTfss.Add(numero, tacheTfs);

            return tacheTfs;
        }

        private TicketTfs CreerEtInsererTicketTfsCore(TacheTfs tacheTfs, IdTicketTfs idTicketTfs)
        {
            TicketTfs ticketTfs = new TicketTfs(tacheTfs, idTicketTfs.NumeroComplementaire);
            _ticketTfsParIdTicketTfss.Add(idTicketTfs, ticketTfs);

            return ticketTfs;
        }

        private ImputationTfs CreerImputationTfsCore(IdImputationTfs idImputationTfs)
        {
            if (ContientImputationTfsCore(idImputationTfs))
                throw new IHException("Imputation déjà insérée.");

            IdTicketTfs idTicketTfs = ObtenirIdTicketTfs(idImputationTfs);
            TicketTfs ticketTfs = ObtenirOuCreerTicketTfsCore(idTicketTfs);

            ImputationTfs imputationTfs = CreerEtInsererImputationTfsCore(ticketTfs, idImputationTfs);

            return imputationTfs;
        }

        private bool ObtenirImputationTfsCore(IdImputationTfs idImputationTfs, out ImputationTfs imputationTfs)
        {
            bool resultat = _imputationTfsParIdImputationTfss.TryGetValue(idImputationTfs, out imputationTfs);

            return resultat;
        }

        private TacheTfs ObtenirOuCreerTacheTfsCore(int numero)
        {
            TacheTfs tacheTfs;
            bool succes = ObtenirTacheTfsCore(numero, out tacheTfs);
            if (false == succes)
            {
                tacheTfs = CreerEtInsererTacheTfsCore(numero);
            }
            Debug.Assert(tacheTfs != null);

            return tacheTfs;
        }

        private TicketTfs ObtenirOuCreerTicketTfsCore(IdTicketTfs idTicketTfs)
        {
            TicketTfs ticketTfs;
            bool succes = ObtenirTicketTfsCore(idTicketTfs, out ticketTfs);
            if (false == succes)
            {
                int numero = idTicketTfs.Numero;
                TacheTfs tacheTfs = ObtenirOuCreerTacheTfsCore(numero);

                ticketTfs = CreerEtInsererTicketTfsCore(tacheTfs, idTicketTfs);
            }
            Debug.Assert(ticketTfs != null);

            return ticketTfs;
        }

        private bool ObtenirTacheTfsCore(int numero, out TacheTfs tacheTfs)
        {
            bool resultat = _tacheTfsParIdTacheTfss.TryGetValue(numero, out tacheTfs);

            return resultat;
        }

        private bool ObtenirTicketTfsCore(IdTicketTfs idTicketTfs, out TicketTfs ticketTfs)
        {
            bool resultat = _ticketTfsParIdTicketTfss.TryGetValue(idTicketTfs, out ticketTfs);

            return resultat;
        }

        private void ResetEstModifie()
        {
            EstModifie = false;
        }

        private void SetEstModifie()
        {
            EstModifie = true;
        }

        private bool SupprimerImputationTfsCore(IdImputationTfs idImputationTfs)
        {
            ImputationTfs imputationTfs;
            bool resultat = ObtenirImputationTfsCore(idImputationTfs, out imputationTfs);
            if (resultat)
            {
                resultat = SupprimerImputationTfsCore(idImputationTfs, imputationTfs);
            }

            return resultat;
        }

        private bool SupprimerImputationTfsCore(IdImputationTfs idImputationTfs, ImputationTfs imputationTfs)
        {
            Debug.Assert(ObtenirIdImputationTfs(imputationTfs) == idImputationTfs);

            IdTicketTfs idTicketTfs = ObtenirIdTicketTfs(idImputationTfs);
            TicketTfs ticketTfs = ObtenirOuCreerTicketTfsCore(idTicketTfs);
            bool resultat = ticketTfs.SupprimerImputationTfs(imputationTfs);
            resultat = resultat && _imputationTfsParIdImputationTfss.Remove(idImputationTfs);

            if (ticketTfs.ImputationTfss.Empty())
            {
                resultat = resultat && SupprimerTicketTfsCore(idTicketTfs, ticketTfs);
            }

            return resultat;
        }

        private bool SupprimerTacheTfsCore(int numero)
        {
            bool resultat = _tacheTfsParIdTacheTfss.Remove(numero);

            return resultat;
        }

        private bool SupprimerTicketTfsCore(IdTicketTfs idTicketTfs, TicketTfs ticketTfs)
        {
            Debug.Assert(ObtenirIdTicketTfs(ticketTfs) == idTicketTfs);

            int numero = idTicketTfs.Numero;
            TacheTfs tacheTfs = ObtenirOuCreerTacheTfsCore(numero);
            bool resultat = tacheTfs.SupprimerTicketTfs(ticketTfs);
            resultat = resultat && _ticketTfsParIdTicketTfss.Remove(idTicketTfs);

            if (tacheTfs.TicketTfss.Empty())
            {
                resultat = resultat && SupprimerTacheTfsCore(numero);
            }

            return resultat;
        }

        private void ViderImputationTfssCore()
        {
            foreach (TicketTfs ticketTfs in _ticketTfsParIdTicketTfss.Values)
            {
                ticketTfs.ViderImputationTfss();
            }
            //_imputationTfsParIdImputationTfss.Clear();    // interdit
            foreach (IdImputationTfs idImputationTfs in _imputationTfsParIdImputationTfss.Keys.ToArray())
            {
                _imputationTfsParIdImputationTfss.Remove(idImputationTfs);
            }

            foreach (TacheTfs tacheTfs in _tacheTfsParIdTacheTfss.Values)
            {
                tacheTfs.ViderTicketTfss();
            }
            _ticketTfsParIdTicketTfss.Clear();

            _tacheTfsParIdTacheTfss.Clear();
        }

        #region Abonnements

        private void _imputationTfsParIdImputationTfss_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetEstModifie();    // pour toutes modifications

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (IImputationTfsModele imputationTfs in (e.NewItems ?? new object[0]))
                        imputationTfs.PropertyChanged += imputationTfs_PropertyChanged;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (IImputationTfsModele imputationTfs in (e.OldItems ?? new object[0]))
                        imputationTfs.PropertyChanged -= imputationTfs_PropertyChanged;
                    break;

                case NotifyCollectionChangedAction.Reset:
                    throw new IHException("Opération non supporté");    // pas possible de réaliser les désabonnement

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                default:
                    Debug.Fail("Cas non géré");
                    break;
            }

            NotifierImputationTfssAChange(this, e);
        }

        private void _tacheTfsParIdTacheTfss_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            e = e.TranslateObservableDictionaryEventArgs<Tuple<int?>, TicketTfs, TicketTfs>((kvp) => kvp.Value);
            NotifierTacheTfssAChange(this, e);
        }

        private void _ticketTfsParIdTicketTfss_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            e = e.TranslateObservableDictionaryEventArgs<Tuple<int?>, TicketTfs, TicketTfs>((kvp) => kvp.Value);
            NotifierTicketTfssAChange(this, e);
        }

        private void imputationTfs_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetEstModifie();    // pout toutes modifications
        }

        #endregion Abonnements

        #endregion Core

        #region Méthodes utilitaires

        private static IdImputationTfs ObtenirIdImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
        {
            IdImputationTfs resultat = new IdImputationTfs(numero, numeroComplementaire, dateHorodatage);

            return resultat;
        }

        private static IdImputationTfs ObtenirIdImputationTfs(IInformationImputationTfs imputation)
        {
            return ObtenirIdImputationTfs(imputation.Numero, imputation.NumeroComplementaire, imputation.DateHorodatage);
        }

        private static IdTicketTfs ObtenirIdTicketTfs(int numero, int? numeroComplementaire)
        {
            IdTicketTfs resultat = new IdTicketTfs(numero, numeroComplementaire);

            return resultat;
        }

        private static IdTicketTfs ObtenirIdTicketTfs(IInformationTicketTfs ticket)
        {
            return ObtenirIdTicketTfs(ticket.Numero, ticket.NumeroComplementaire);
        }

        private IdTicketTfs ObtenirIdTicketTfs(IdImputationTfs idImputationTfs)
        {
            return ObtenirIdTicketTfs(idImputationTfs.Numero, idImputationTfs.NumeroComplementaire);
        }

        #endregion Méthodes utilitaires

        #region Class IdTicketTfs

        private class IdTicketTfs : Tuple<int, int?>
        {
            public IdTicketTfs(int numero, int? numeroComplementaire)
                : base(numero, numeroComplementaire)
            {
            }

            public int Numero { get { return Item1; } }

            public int? NumeroComplementaire { get { return Item2; } }

            public static bool Equals(IdTicketTfs a, IdTicketTfs b)
            {
                bool resultat = false;

                if (false == Object.ReferenceEquals(a, b))
                {
                    resultat = a.Equals(b);
                }

                return resultat;
            }

            public static bool operator !=(IdTicketTfs a, IdTicketTfs b)
            {
                return false == Equals(a, b);
            }

            public static bool operator ==(IdTicketTfs a, IdTicketTfs b)
            {
                return Equals(a, b);
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        #endregion Class IdTicketTfs

        #region Class IdImputationTfs

        private class IdImputationTfs : Tuple<int, int?, DateTimeOffset>
        {
            public IdImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
                : base(numero, numeroComplementaire, dateHorodatage)
            {
            }

            public DateTimeOffset DateHorodatage { get { return Item3; } }

            public int Numero { get { return Item1; } }

            public int? NumeroComplementaire { get { return Item2; } }

            public static bool Equals(IdTicketTfs a, IdTicketTfs b)
            {
                bool resultat = false;

                if (false == Object.ReferenceEquals(a, b))
                {
                    resultat = a.Equals(b);
                }

                return resultat;
            }

            public static bool operator !=(IdImputationTfs a, IdImputationTfs b)
            {
                return false == Equals(a, b);
            }

            public static bool operator ==(IdImputationTfs a, IdImputationTfs b)
            {
                return Equals(a, b);
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            #region ComparateurDecroissant

            private static IComparer<IdImputationTfs> _comparateurDecroissant;

            public static IComparer<IdImputationTfs> ComparateurDecroissant
            {
                get
                {
                    if (_comparateurDecroissant == null)
                        _comparateurDecroissant = new ClasseComparateurDecroissant();
                    return _comparateurDecroissant;
                }
            }

            private class ClasseComparateurDecroissant : IComparer<IdImputationTfs>
            {
                public int Compare(IdImputationTfs x, IdImputationTfs y)
                {
                    return ImputationTfs.ComparateurDateHorodatageDecroissant.Compare(x.DateHorodatage, y.DateHorodatage);
                }
            }

            #endregion ComparateurDecroissant
        }

        #endregion Class IdImputationTfs
    }
}