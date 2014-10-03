using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public class RapportMensuelFormModele : IRapportMensuelFormModele
    {
        #region Constantes

        private static IComparer<IInformationImputationTfs> ConstanteOrdreAffichageImputationTfs = ImputationTfs.ComparateurImputationTfsCroissant;

        #endregion Constantes

        #region Membres

        private readonly IImputationH31perModele _imputationH31perModele;

        private DateTimeOffset _dateMoisAnnee;
        private IEnumerable<IInformationImputationTfs> _informationImputations;
        private IEnumerable<GroupeItem> _groupes;
        private GroupeItem _groupeSelectionne;
        private IEnumerable<TacheItem> _taches;
        private TacheItem _tacheSelectionnee;
        private IEnumerable<TicketItem> _tickets;
        private TicketItem _ticketSelectionne;

        #endregion Membres

        #region ctor

        public RapportMensuelFormModele(IImputationH31perModele imputationH31perModele)
        {
            this._imputationH31perModele = imputationH31perModele;

            this.DateMoisAnnee = DateTime.Now;  // déclenche la mise à jour en cascadde du modèle
        }

        #endregion ctor

        #region Propriétés

        public IImputationH31perModele ImputationH31perModele
        {
            get { return _imputationH31perModele; }
        }

        public DateTimeOffset DateMoisAnnee
        {
            get { return _dateMoisAnnee; }
            set
            {
                if ((_dateMoisAnnee.Month == value.Month) && (_dateMoisAnnee.Year == value.Year))
                    return;
                _dateMoisAnnee = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteDateMoisAnnee));
                MettreAJourInformationImputations();
            }
        }

        public IEnumerable<IInformationImputationTfs> InformationImputations
        {
            get { return _informationImputations; }
            set
            {
                _informationImputations = value;
                MettreAJourGroupes();
            }
        }

        public IEnumerable<GroupeItem> Groupes
        {
            get { return _groupes; }
            private set
            {
                _groupes = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteGroupes));
                MettreAJourGroupeSelectionne();
            }
        }

        public IEnumerable<TacheItem> Taches
        {
            get { return _taches; }
            private set
            {
                _taches = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteTaches));
                MettreAJourTacheSelectionnee();
            }
        }

        public IEnumerable<TicketItem> Tickets
        {
            get { return _tickets; }
            private set
            {
                _tickets = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteTickets));
                MettreAJourTicketSelectionne();
            }
        }

        public GroupeItem GroupeSelectionne
        {
            get { return _groupeSelectionne; }
            set
            {
                _groupeSelectionne = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteGroupeSelectionne));
                MettreAJourTaches();
            }
        }

        public TacheItem TacheSelectionnee
        {
            get { return _tacheSelectionnee; }
            set
            {
                _tacheSelectionnee = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteTacheSelectionnee));
                MettreAJourTickets();
            }
        }

        public TicketItem TicketSelectionne
        {
            get { return _ticketSelectionne; }
            set
            {
                _ticketSelectionne = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteTicketSelectionne));
                MettreAJourGroupement();
            }
        }

        #endregion Propriétés

        #region Mise à jour de propriétés

        private void MettreAJourInformationImputations()
        {
#warning TODO - point ALPHA ALPHA - vérifie l'écart jour...
            DateTimeOffset moisAnnee = DateMoisAnnee;
            DateTimeOffset dateMin = moisAnnee;
            DateTimeOffset dateMax = moisAnnee.AddMonths(1).Date;

            InformationImputations = _imputationH31perModele.ImputationTfss
                .Where(i => { DateTimeOffset date = i.DateImputationPlusRecente(); return date.EstComprisEntre(dateMin, dateMax); })
                .Reverse()  // optimisation
                .OrderBy(i => i, ConstanteOrdreAffichageImputationTfs)
                .Execute();
        }

#warning TODO - point ALPHA - implémenter !

        private void MettreAJourGroupes()
        {
#warning TODO - point ALPHA - implémenter !
            Groupes = Enumerable.Empty<GroupeItem>();
        }

        private void MettreAJourGroupeSelectionne()
        {
#warning TODO - point ALPHA - implémenter !
            GroupeSelectionne = null;
        }

        private void MettreAJourTaches()
        {
#warning TODO - point ALPHA - implémenter !
            Taches = Enumerable.Empty<TacheItem>();
        }

        private void MettreAJourTacheSelectionnee()
        {
#warning TODO - point ALPHA - implémenter !
            TacheSelectionnee = null;
        }

        private void MettreAJourTickets()
        {
#warning TODO - point ALPHA - implémenter !
            Tickets = Enumerable.Empty<TicketItem>();
        }

        private void MettreAJourTicketSelectionne()
        {
#warning TODO - point ALPHA - implémenter !
            TicketSelectionne = null;
        }

        private void MettreAJourGroupement()
        {
#warning TODO - point ALPHA - implémenter !
            
        }

        #endregion Mise à jour de propriétés

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notifier(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}