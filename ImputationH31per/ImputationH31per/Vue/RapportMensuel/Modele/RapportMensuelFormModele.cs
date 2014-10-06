﻿using System;
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
        private IEnumerable<IInformationImputationTfs> _imputationsPourRegroupementCourant;
        private IEnumerable<IInformationImputationTfs> _imputationRestantes;
        private IEnumerable<IInformationImputationTfs> _imputationPourGroupes;
        private IEnumerable<GroupeItem> _groupes;
        private GroupeItem _groupeSelectionne;
        private IEnumerable<IInformationImputationTfs> _imputationPourTaches;
        private IEnumerable<TacheItem> _taches;
        private TacheItem _tacheSelectionnee;
        private IEnumerable<IInformationImputationTfs> _imputationPourTickets;
        private IEnumerable<TicketItem> _tickets;
        private TicketItem _ticketSelectionne;
        private IEnumerable<IInformationItem<IInformationTacheTfs>> _itemsRegroupementCourant;
        private IInformationItem<IInformationTacheTfs> _itemRegroupementCourantSelectionne;
        private IEnumerable<IInformationTacheTfs> _imputationsDuRegroupementCourant;

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
                MettreAJourImputationsPourRegroupementCourant();
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationsPourRegroupementCourant
        {
            get { return _imputationsPourRegroupementCourant; }
            private set
            {
                _imputationsPourRegroupementCourant = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteImputationsPourRegroupementCourant));
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
                MettreAJourImputationPourTickets();
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationPourTickets
        {
            get { return _imputationPourTickets; }
            private set
            {
                _imputationPourTickets = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteImputationPourTickets));
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
            }
        }

        public IEnumerable<IInformationItem<IInformationTacheTfs>> ItemsRegroupementCourant
        {
            get { return _itemsRegroupementCourant; }
            private set
            {
                _itemsRegroupementCourant = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteItemsRegroupementCourant));
                MettreAJourImputationsDuRegroupementCourant();
            }
        }

        public IInformationItem<IInformationTacheTfs> ItemRegroupementCourantSelectionne
        {
            get { return _itemRegroupementCourantSelectionne; }
            set
            {
                _itemRegroupementCourantSelectionne = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteItemRegroupementCourantSelectionne));
            }
        }

        public IEnumerable<IInformationTacheTfs> ImputationsDuRegroupementCourant
        {
            get { return _imputationsDuRegroupementCourant; }
            set
            {
                _imputationsDuRegroupementCourant = value;
                MettreAJourImputationRestantes();
            }
        }

        #endregion Propriétés

        #region Methodes

        public void AjouterAuRegroupement(IInformationItem<IInformationTacheTfs> item)
        {
#warning TODO ALPHA BETA point
            throw new NotImplementedException();
        }

        public void RetirerDuRegroupement(IInformationItem<IInformationTacheTfs> item)
        {
#warning TODO ALPHA BETA point
            throw new NotImplementedException();
        }

        #endregion Methodes

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

        private void MettreAJourImputationsPourRegroupementCourant()
        {
#warning TODO - point BETA ALPHA - implémenter : ImputationsDuMois - ImputationsRegroupements
            ImputationsPourRegroupementCourant = ImputationsDuMois;
        }

        private void MettreAJourImputationRestantes()
        {
#warning TODO - point BETA ALPHA - implémenter : ImputationsPourRegroupementCourant - ImputationsDuRegroupementCourant
            ImputationRestantes = ImputationsPourRegroupementCourant;
        }

        private void MettreAJourImputationPourGroupes()
        {
            ImputationPourGroupes = ImputationRestantes;
        }

        private void MettreAJourGroupes()
        {
            List<GroupeItem> groupes = ImputationPourGroupes
                .GroupBy(i => i.NomGroupement)
                .Select(grp => new GroupeItem(grp.First()))
                .ToList();
            groupes.Add(GroupeItem.Tous);
            Groupes = groupes;
        }

        private void MettreAJourGroupeSelectionne()
        {
            GroupeSelectionne = ObtenirMiseAJourSelectionItem<GroupeItem, IInformationTacheTfs>(Groupes, GroupeSelectionne);
        }

        private void MettreAJourImputationPourTaches()
        {
            IEnumerable<IInformationImputationTfs> imputations;
            EnumTypeItem typeItem = (GroupeSelectionne != null) ? GroupeSelectionne.TypeItem : EnumTypeItem.Tous;
            switch (typeItem)
            {
                case EnumTypeItem.Tous:
                    imputations = ImputationPourGroupes;
                    break;

                case EnumTypeItem.Entite:
                    string nomGroupe = GroupeSelectionne.Entite.NomGroupement;
                    imputations = ImputationPourGroupes
                        .Where(i => String.Equals(i.NomGroupement, nomGroupe))
                        .Execute();
                    break;

                default:
                    imputations = new IInformationImputationTfs[0];
                    break;
            }
            ImputationPourTaches = imputations;
        }

        private void MettreAJourTaches()
        {
            List<TacheItem> taches = ImputationPourTaches
                .GroupBy(i => i.Numero)
                .Select(grp => new TacheItem(grp.First()))
                .ToList();
            taches.Add(TacheItem.Tous);
            Taches = taches;
        }

        private void MettreAJourTacheSelectionnee()
        {
            TacheSelectionnee = ObtenirMiseAJourSelectionItem<TacheItem, IInformationTacheTfs>(Taches, TacheSelectionnee);
        }

        private void MettreAJourImputationPourTickets()
        {
            IEnumerable<IInformationImputationTfs> imputations;
            EnumTypeItem typeItem = (TacheSelectionnee != null) ? TacheSelectionnee.TypeItem : EnumTypeItem.Tous;
            switch (typeItem)
            {
                case EnumTypeItem.Tous:
                    imputations = ImputationPourTaches;
                    break;

                case EnumTypeItem.Entite:
                    int numeroTache = TacheSelectionnee.Entite.Numero;
                    imputations = ImputationPourTaches
                        .Where(i => String.Equals(i.Numero, numeroTache))
                        .Execute();
                    break;

                default:
                    imputations = new IInformationImputationTfs[0];
                    break;
            }
            ImputationPourTickets = imputations;
        }

        private void MettreAJourTickets()
        {
            List<TicketItem> ticket = ImputationPourTickets
                .GroupBy(i => i.NumeroComplet())
                .Select(grp => new TicketItem(grp.First()))
                .ToList();
            ticket.Add(TicketItem.Tous);
            Tickets = ticket;
        }

        private void MettreAJourTicketSelectionne()
        {
            TicketSelectionne = ObtenirMiseAJourSelectionItem<TicketItem, IInformationTicketTfs>(Tickets, TicketSelectionne);
        }

        private void MettreAJourImputationsDuRegroupementCourant()
        {
#warning TODO ALPHA point
            ImputationsDuRegroupementCourant = new IInformationImputationTfs[0];
        }

        private TItem ObtenirMiseAJourSelectionItem<TItem, TEntite>(IEnumerable<TItem> source, TItem ancienItem)
         where TItem : class, IItem<TEntite>
        {
            TItem nouveauItem = null;
            if ((ancienItem != null) && (ancienItem.TypeItem == EnumTypeItem.Entite))
            {
                nouveauItem = source
                    .Where(i => (i.TypeItem == EnumTypeItem.Entite) && ancienItem.Equals(i))
                    .FirstOrDefault();
            }
            if (nouveauItem == null)
            {
                nouveauItem = source
                    .Where(i => i.TypeItem == EnumTypeItem.Tous)
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