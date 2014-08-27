using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele.Entite
{
    public class TicketTfs : TicketTfsBase<ImputationTfs>
    {
        #region Membres

        /// <summary>
        /// Note : stockage inversé : date plus récentes aux plus vieilles
        /// </summary>
        private readonly SortedObservableCollection<DateTimeOffset, ImputationTfs> _imputationTfss;

        private readonly TacheTfs _tacheTfs;

        #endregion Membres

        #region ctor

        public TicketTfs(TacheTfs tacheTfs, int? numeroComplementaire)
            : base(tacheTfs, numeroComplementaire)
        {
            Debug.Assert(tacheTfs != null);

            _tacheTfs = tacheTfs;
            _tacheTfs.AjouterTicketTfs(this);

            new WeakEventHandler<TicketTfs, PropertyChangedEventArgs, TacheTfs>(
                this
                , NotifierPropertyChanged
                , OnDetachedPropertyChanged
                , OnAttachedPropertyChanged
                );

            _imputationTfss = new SortedObservableCollection<DateTimeOffset, ImputationTfs>(ImputationTfs.ComparateurDateHorodatageDecroissant);
            _imputationTfss.CollectionChanged += _imputationTfss_CollectionChanged;
        }

        ~TicketTfs()
        {
            _imputationTfss.CollectionChanged -= _imputationTfss_CollectionChanged;
        }

        private void _imputationTfss_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifierImputationTfssAChange(this, e);
        }

        #region WeakEventHandler sur _tacheTfs.PropertyChanged

        private static void NotifierPropertyChanged(TicketTfs ticketTfs, object sender, PropertyChangedEventArgs e)
        {
            ticketTfs.NotifierPropertyChanged(ticketTfs, e);
        }

        private static void OnDetachedPropertyChanged(WeakEventHandler<TicketTfs, PropertyChangedEventArgs, TacheTfs> weakEventHandler)
        {
            weakEventHandler.Handler.PropertyChanged -= weakEventHandler.OnEvent;
        }

        private TacheTfs OnAttachedPropertyChanged(WeakEventHandler<TicketTfs, PropertyChangedEventArgs, TacheTfs> weakEventHandler)
        {
            _tacheTfs.PropertyChanged += weakEventHandler.OnEvent;
            return _tacheTfs;
        }

        #endregion WeakEventHandler sur _tacheTfs.PropertyChanged

        #endregion ctor

        #region Propriétés

        public override IEnumerable<ImputationTfs> ImputationTfss
        {
            get { return _imputationTfss.Values; }
        }

        public override string Nom
        {
            get { return _tacheTfs.Nom; }
            set { _tacheTfs.Nom = value; }
        }

        public override string NomGroupement
        {
            get { return _tacheTfs.NomGroupement; }
            set { _tacheTfs.NomGroupement = value; }
        }

        public override int Numero
        {
            get { return _tacheTfs.Numero; }
        }

        #endregion Propriétés

        #region Méthodes

        public override void AjouterImputationTfs(ImputationTfs imputationTfs)
        {
            _imputationTfss.Add(imputationTfs.DateHorodatage, imputationTfs);
        }

        public ImputationTfs DerniereImputation()
        {
            ImputationTfs imputationTfs = _imputationTfss.Values.FirstOrDefault();

            return imputationTfs;
        }

        public ImputationTfs ObtenirImputation(DateTimeOffset dateHorodatage)
        {
            ImputationTfs imputationTfs = _imputationTfss[dateHorodatage];

            return imputationTfs;
        }

        public override bool SupprimerImputationTfs(DateTimeOffset dateHorodatage)
        {
            bool resultat = _imputationTfss.Remove(dateHorodatage);

            return resultat;
        }

        public override void ViderImputationTfss()
        {
            _imputationTfss.Clear();
        }

        #endregion Méthodes
    }
}