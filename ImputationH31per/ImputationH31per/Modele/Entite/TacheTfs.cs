using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele.Entite
{
    public class TacheTfs : TacheTfsBase<TicketTfs, ImputationTfs>
    {
        #region Membres

        private readonly ObservableDictionary<Tuple<int?>, TicketTfs> _ticketTfss;

        #endregion Membres

        #region ctor

        public TacheTfs(int numero)
            : base(numero)
        {
            _ticketTfss = new ObservableDictionary<Tuple<int?>, TicketTfs>();
            _ticketTfss.CollectionChanged += _ticketTfss_CollectionChanged;
        }

        ~TacheTfs()
        {
            _ticketTfss.CollectionChanged -= _ticketTfss_CollectionChanged;
        }

        private void _ticketTfss_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            e = e.TranslateObservableDictionaryEventArgs<Tuple<int?>, TicketTfs, TicketTfs>((kvp) => kvp.Value);
            NotifierTicketTfssAChange(this, e);
        }

        #endregion ctor

        #region Propriétés

        public override IEnumerable<TicketTfs> TicketTfss
        {
            get { return _ticketTfss.Values; }
        }

        #endregion Propriétés

        #region Méthodes

        public override void AjouterTicketTfs(TicketTfs ticketTfs)
        {
            Tuple<int?> id = new Tuple<int?>(ticketTfs.NumeroComplementaire);
            _ticketTfss.Add(id, ticketTfs);
        }

        public override bool SupprimerTicketTfs(int? numeroComplementaire)
        {
            Tuple<int?> id = new Tuple<int?>(numeroComplementaire);
            bool resultat = _ticketTfss.Remove(id);

            return resultat;
        }

        public override void ViderTicketTfss()
        {
            _ticketTfss.Clear();
        }

        #endregion Méthodes
    }
}