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
        private IEnumerable<IInformationImputationTfs> _imputationsDuMois;
        private IEnumerable<IInformationImputationTfs> _imputationRestantes;
        private IEnumerable<IInformationImputationTfs> _imputationPourGroupes;
        private IEnumerable<GroupeItem> _groupes;
        private GroupeItem _groupeSelectionne;
        private IEnumerable<IInformationImputationTfs> _imputationPourTaches;
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
                MettreAJourImputationDuMois();
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationsDuMois
        {
            get { return _imputationsDuMois; }
            private set
            {
                _imputationsDuMois = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteImputationsDuMois));
                MettreAJourImputationRestantes();
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationRestantes
        {
            get { return _imputationRestantes; }
            private set
            {
                _imputationRestantes = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteImputationRestantes));
                MettreAJourImputationPourGroupes();
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationPourGroupes
        {
            get { return _imputationPourGroupes; }
            private set
            {
                _imputationPourGroupes = value;

                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteImputationPourGroupes));
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

        public GroupeItem GroupeSelectionne
        {
            get { return _groupeSelectionne; }
            set
            {
                _groupeSelectionne = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteGroupeSelectionne));
                MettreAJourImputationPourTaches();
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationPourTaches
        {
            get { return _imputationPourTaches; }
            private set
            {
                _imputationPourTaches = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteImputationPourTaches));
                MettreAJourTaches();
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

        private void MettreAJourImputationDuMois()
        {
            DateTimeOffset moisAnnee = DateMoisAnnee
                .AddDays(-DateMoisAnnee.Day + 1)
                .Add(-DateMoisAnnee.TimeOfDay);
            DateTimeOffset dateMin = moisAnnee;
            DateTimeOffset dateMax = moisAnnee.AddMonths(1).Date;

            ImputationsDuMois = _imputationH31perModele.ImputationTfss
                .Where(i => { DateTimeOffset date = i.DateImputationPlusRecente(); return date.EstComprisEntre(dateMin, dateMax); })
                //.Reverse()  // optimisation
                //.OrderBy(i => i, ConstanteOrdreAffichageImputationTfs)
                .Execute();
        }

        private void MettreAJourImputationRestantes()
        {
#warning TODO - point BETA ALPHA - implémenter !
            ImputationRestantes = ImputationsDuMois;
        }

        private void MettreAJourImputationPourGroupes()
        {
            ImputationPourGroupes = ImputationRestantes;
        }

        private void MettreAJourGroupes()
        {
            List<GroupeItem> groupes = ImputationPourGroupes
                .GroupBy(i => i.NomGroupement)
                .Select(grp => new GroupeItem(grp.Key))
                .ToList();
            if (groupes.Any())
                groupes.Add(GroupeItem.Tous);
            groupes.Add(GroupeItem.Aucun);
            Groupes = groupes;
        }

        private void MettreAJourGroupeSelectionne()
        {
            GroupeSelectionne = ObtenirMiseAJourSelectionItem<GroupeItem, string>(Groupes, GroupeSelectionne);
        }

        private void MettreAJourImputationPourTaches()
        {
            IEnumerable<IInformationImputationTfs> imputations;
            EnumTypeItem typeItem = (GroupeSelectionne != null) ? GroupeSelectionne.TypeItem : EnumTypeItem.Aucun;
            switch (typeItem)
            {
                case EnumTypeItem.Tous:
                    imputations = ImputationPourGroupes;
                    break;

                case EnumTypeItem.Entite:
                    string nomGroupe = GroupeSelectionne.Entite;
                    imputations = ImputationPourGroupes
                        .Where(i => String.Equals(i.NomGroupement, nomGroupe))
                        .Execute();
                    break;

                case EnumTypeItem.Aucun:
                default:
                    imputations = new IInformationImputationTfs[0];
                    break;
            }
            ImputationPourTaches = imputations;
        }

#warning TODO - point ALPHA - implémenter !

        private void MettreAJourTaches()
        {
            List<TacheItem> taches = ImputationPourTaches
                .GroupBy(i => i.Numero)
                .Select(grp => new TacheItem(grp.First()))
                .ToList();
            if (taches.Any())
                taches.Add(TacheItem.Tous);
            taches.Add(TacheItem.Aucun);
            Taches = taches;
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

        private TItem ObtenirMiseAJourSelectionItem<TItem, TEntite>(IEnumerable<TItem> source, TItem ancienItem)
         where TItem : class, IItem<TEntite>
        {
            TItem nouveauItem = null;
            if (ancienItem != null)
            {
                EnumTypeItem ancienTypeItem = ancienItem.TypeItem;
                if (ancienTypeItem == EnumTypeItem.Tous)
                {
                    nouveauItem = source
                        .Where(i => i.TypeItem == EnumTypeItem.Tous)
                        .FirstOrDefault();
                }
                else if (ancienTypeItem == EnumTypeItem.Entite)
                {
                    nouveauItem = source
                        .Where(i => (i.TypeItem == EnumTypeItem.Entite) && ancienItem.Equals(i))
                        .FirstOrDefault();
                }
            }
            if (nouveauItem == null)
            {
                nouveauItem = source
                    .Where(i => i.TypeItem == EnumTypeItem.Aucun)
                    .FirstOrDefault();
            }

            return nouveauItem;
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