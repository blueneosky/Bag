﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private Regroupement _regroupementCourant;
        private IInformationItem<IInformationTacheTfs> _regroupementCourantItemSelectionne;
        private IEnumerable<IInformationImputationTfs> _imputationsDuRegroupementCourant;
        private int _regroupementCourantTotalHeure;
        private IEnumerable<Regroupement> _regroupements;
        private Regroupement _regroupementsItemSelectionne;
        private IEnumerable<Regroupement> _regroupementRapports;

        #endregion Membres

        #region ctor

        public RapportMensuelFormModele(IImputationH31perModele imputationH31perModele)
        {
            this._imputationH31perModele = imputationH31perModele;

            _regroupements = new Regroupement[0];   // pour ne pas impacter de rafraichissement en cascade
            _regroupementCourant = new Regroupement(String.Empty);    // regroupement recréé et finalisé après la date
            this.DateMoisAnnee = DateTime.Now;  // déclenche la mise à jour en cascadde du modèle
            CreerNouveauRegroupemet();          // important de faire l'assignation à ce moment (sinon le modèle n'est pas initialisé dans son intégralité)
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
                Regroupements = new Regroupement[0];
                CreerNouveauRegroupemet();
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

        public Regroupement RegroupementCourant
        {
            get { return _regroupementCourant; }
            private set
            {
                if (_regroupementCourant != null)
                    _regroupementCourant.NomModifie -= _regroupementCourant_NomModifie;
                _regroupementCourant = value;
                if (_regroupementCourant != null)
                    _regroupementCourant.NomModifie += _regroupementCourant_NomModifie;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourant));
                MettreAJourRegroupementCourantItemSelectionne();
                MettreAJourImputationRestantes();
                MettreAJourImputationsDuRegroupementCourant();
            }
        }

        private void _regroupementCourant_NomModifie(object sender, EventArgs e)
        {
            NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourantNom));
        }

        public IInformationItem<IInformationTacheTfs> RegroupementCourantItemSelectionne
        {
            get { return _regroupementCourantItemSelectionne; }
            set
            {
                _regroupementCourantItemSelectionne = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourantItemSelectionne));
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationsDuRegroupementCourant
        {
            get { return _imputationsDuRegroupementCourant; }
            set
            {
                _imputationsDuRegroupementCourant = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteImputationsDuRegroupementCourant));
                MettreAJourRegroupementCourantTotalHeure();
            }
        }

        public int RegroupementCourantTotalHeure
        {
            get { return _regroupementCourantTotalHeure; }
            private set
            {
                _regroupementCourantTotalHeure = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourantTotalHeure));
            }
        }

        public IEnumerable<Regroupement> Regroupements
        {
            get { return _regroupements; }
            private set
            {
                _regroupements = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupements));
                MettreAJourRegroupementsItemSelectionne();
                MettreAJourImputationsPourRegroupementCourant();
                MettreAJourRegroupementRapports();
            }
        }

        public Regroupement RegroupementsItemSelectionne
        {
            get { return _regroupementsItemSelectionne; }
            set
            {
                _regroupementsItemSelectionne = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementsItemSelectionne));
            }
        }

        public IEnumerable<Regroupement> RegroupementRapports
        {
            get { return _regroupementRapports; }
            set
            {
                _regroupementRapports = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementRapports));
            }
        }

        #endregion Propriétés

        #region Methodes

        public void AjouterAuRegroupement(IInformationItem<IInformationTacheTfs> item)
        {
            Regroupement regroupement = RegroupementCourant;
            regroupement.Items.Add(item);
            RegroupementCourant = regroupement;
        }

        public void RetirerDuRegroupement(IInformationItem<IInformationTacheTfs> item)
        {
            Regroupement regroupement = RegroupementCourant;
            int index = regroupement.Items.FindIndex(r => r.Equals(item));
            if (index < 0)
                return;

            regroupement.Items.RemoveAt(index);
            RegroupementCourant = regroupement;
        }

        public void RetirerDeRegroupements(Regroupement regroupement)
        {
            IEnumerable<Regroupement> regroupements = Regroupements
                .Where(r => false == r.Equals(regroupement))
                .Execute();
            Regroupements = new Regroupement[0];
            // boucle pour mettre à jour
            foreach (var regrouement in regroupements)
            {
                AjouterRegroupement(regrouement);
            }
        }

        public void AjouterRegroupementCourant()
        {
            AjouterRegroupement(RegroupementCourant);
            CreerNouveauRegroupemet();
        }

        private void AjouterRegroupement(Regroupement regroupement)
        {
            if (regroupement.Items.Count == 0)
                return;

            List<Regroupement> regroupements = Regroupements.ToList();
            IEnumerable<IInformationImputationTfs> imputations = ImputationsDuMois;
            imputations = ObtenirInformationImputationsFiltres(imputations, regroupements, false);
            imputations = ObtenirInformationImputationsFiltres(imputations, regroupement, true);
            int totalHeure = (int)ObtenirTotalHeure(imputations);
            regroupement.TotalHeure = totalHeure;
            regroupements.Add(regroupement);
            Regroupements = regroupements;
        }

        private void CreerNouveauRegroupemet()
        {
            RegroupementCourant = new Regroupement(String.Empty);
            RegroupementCourant.Nom = Regroupements.Select(r => r.Nom).NomUnique("nouveau");    // pour notification
        }

        private double ObtenirTotalHeure(IEnumerable<IInformationImputationTfs> imputations)
        {
            return imputations
                .Sum(i => _imputationH31perModele.ObtenirDifferenceConsommee(i) ?? 0);
        }

        private static IEnumerable<Regroupement> ObtenirRegroupementVentilles(IEnumerable<Regroupement> regroupements)
        {
            regroupements = regroupements.Execute();

            List<Regroupement> resultat = regroupements
                .Select(r => r.Clone())
                .ToList();
            List<Tuple<Regroupement, int>> regroupementEtExcedantRestants = resultat
                .OrderByDescending(r => r.TotalHeure % 4)
                .ThenByDescending(r => r.TotalHeure)
                .Select(r => Tuple.Create(r, (r.TotalHeure ?? 0) % 4))
                .ToList();

            while (regroupementEtExcedantRestants.Sum(t => t.Item2) >= 4)
            {
                IEnumerable<Tuple<Regroupement, int>> resultatRegroupement = Regrouper(regroupementEtExcedantRestants);
                Debug.Assert(resultatRegroupement != null);
                var premierTuple = resultatRegroupement.First();
                int sommeExcedant = 0;
                foreach (var tuple in resultatRegroupement.Skip(1))
                {
                    int excedant = tuple.Item2;
                    sommeExcedant += excedant;
                    tuple.Item1.TotalHeure -= excedant;
                }
                premierTuple.Item1.TotalHeure += sommeExcedant;
            }

            // Normalement pas nécessaire
            //resultat = regroupements
            //    .Join(resultat, r => r.Nom, r => r.Nom, (outer, inner) => inner)       // conserve l'ordre de 'regroupements' avec les instances de 'resultat'
            //    .ToList();

            return resultat;
        }

        private static IEnumerable<Tuple<Regroupement, int>> Regrouper(List<Tuple<Regroupement, int>> source)
        {
            HashSet<int> indexes = new HashSet<int> { 0 };
            int somme = 0;

            Debug.Assert(source.Sum(t => t.Item2) >= 4);
            while (somme != 4)
            {
                somme = source[0].Item2;

                int index = 1;
                while ((index < source.Count) && (somme < 4))
                {
                    int excedant = source[index].Item2;
                    if ((somme + excedant) <= 4)
                    {
                        indexes.Add(index);
                        somme += excedant;
                    }
                    index++;
                }

                if (somme != 4)
                {
                    // pas de regroupement complet des excedants
                    // découpage du dernier pour obtenir 1 unité + (n-1)
                    index = source.TakeWhile(t => t.Item2 > 1).Count() - 1;
                    var tuple = source[index];
                    source.RemoveAt(index);
                    var tuple1 = Tuple.Create(tuple.Item1, tuple.Item2 - 1);
                    var tuple2 = Tuple.Create(tuple.Item1, 1);
                    source.Add(tuple1);
                    source.Add(tuple2);
                }
            }

            // création du résultat
            IEnumerable<Tuple<Regroupement, int>> resultat = source.Where((t, i) => indexes.Contains(i)).ToArray(); // le résultat respecte le même ordre que la source

            // mise à jour de la source
            foreach (int i in indexes.OrderByDescending(i => i))
            {
                source.RemoveAt(i);
            }

            return resultat;
        }

        private static void Separer<T>(IEnumerable<T> source, Func<T, bool> predicat, out List<T> resultatPredicat, out List<T> resultatInverse)
        {
            source = source.Execute();
            resultatPredicat = source.Where(predicat).ToList();
            resultatInverse = source.Where(e => false == predicat(e)).ToList();
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
            ImputationsPourRegroupementCourant = ObtenirInformationImputationsFiltres(ImputationsDuMois, Regroupements, false);
        }

        private void MettreAJourImputationRestantes()
        {
            ImputationRestantes = ObtenirInformationImputationsFiltres(ImputationsPourRegroupementCourant, RegroupementCourant, false);
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
            ImputationPourTaches = ObtenirInformationImputationsFiltres(ImputationPourGroupes, GroupeSelectionne, true)
                .Execute();
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
            ImputationPourTickets = ObtenirInformationImputationsFiltres(ImputationPourTaches, TacheSelectionnee, true)
                .Execute();
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

        private void MettreAJourRegroupementCourantItemSelectionne()
        {
            RegroupementCourantItemSelectionne = ObtenirMiseAJourSelectionItem<IInformationItem<IInformationTacheTfs>, IInformationTacheTfs>(RegroupementCourant, RegroupementCourantItemSelectionne);
        }

        private void MettreAJourImputationsDuRegroupementCourant()
        {
            ImputationsDuRegroupementCourant = ObtenirInformationImputationsFiltres(ImputationsPourRegroupementCourant, RegroupementCourant, true)
                .Execute();
        }

        private void MettreAJourRegroupementCourantTotalHeure()
        {
            double total = ObtenirTotalHeure(ImputationsDuRegroupementCourant);
            RegroupementCourantTotalHeure = (int)total;
        }

        private void MettreAJourRegroupementsItemSelectionne()
        {
            RegroupementsItemSelectionne = ObtenirMiseAJourSelectionItem<Regroupement, string>(Regroupements, RegroupementsItemSelectionne);
        }

        private void MettreAJourRegroupementRapports()
        {
            RegroupementRapports = ObtenirRegroupementVentilles(Regroupements);
        }

        #region Outils

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

        #region ObtenirInformationImputationsFiltres

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltres(IEnumerable<IInformationImputationTfs> source, IEnumerable<IEnumerable<IEnumerable<IInformationItem<IInformationTacheTfs>>>> itemFiltres, bool modeInclusion)
        {
            return ObtenirInformationImputationsFiltresGenerique(source, itemFiltres, modeInclusion, ObtenirInformationImputationsFiltres);
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltres(IEnumerable<IInformationImputationTfs> source, IEnumerable<IEnumerable<IInformationItem<IInformationTacheTfs>>> itemFiltres, bool modeInclusion)
        {
            return ObtenirInformationImputationsFiltresGenerique(source, itemFiltres, modeInclusion, ObtenirInformationImputationsFiltres);
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltres(IEnumerable<IInformationImputationTfs> source, IEnumerable<IInformationItem<IInformationTacheTfs>> itemFiltres, bool modeInclusion)
        {
            //IEnumerable<IInformationImputationTfs> imputations = source;
            itemFiltres = itemFiltres ?? new IInformationItem<IInformationTacheTfs>[0];
            if (false == itemFiltres.Any())
                return modeInclusion ? new IInformationImputationTfs[0] : source;

            return ObtenirInformationImputationsFiltresGenerique(source, itemFiltres, modeInclusion, ObtenirInformationImputationsFiltres);

            //foreach (var itemFiltre in itemFiltres)
            //{
            //    imputations = ObtenirInformationImputationsFiltres(imputations, itemFiltre, modeInclusion);
            //}

            //return imputations;
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltres(IEnumerable<IInformationImputationTfs> source, IInformationItem<IInformationTacheTfs> itemFiltre, bool modeInclusion)
        {
            if (itemFiltre == null)
                return modeInclusion ? new IInformationImputationTfs[0] : source;
            if (itemFiltre.TypeItem == EnumTypeItem.Tous)
                return modeInclusion ? source : new IInformationImputationTfs[0];

            switch (itemFiltre.TypeInformation)
            {
                case EnumTypeInformation.Groupe:
                    return ObtenirInformationImputationsFiltres(source, (GroupeItem)itemFiltre, modeInclusion);
                case EnumTypeInformation.Tache:
                    return ObtenirInformationImputationsFiltres(source, (TacheItem)itemFiltre, modeInclusion);
                case EnumTypeInformation.Ticket:
                    return ObtenirInformationImputationsFiltres(source, (TicketItem)itemFiltre, modeInclusion);
                case EnumTypeInformation.Aucun:
                default:
                    Debug.Fail("Cas non prévus");
                    return source;
            }
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltres(IEnumerable<IInformationImputationTfs> source, GroupeItem groupeItemFiltre, bool modeInclusion)
        {
            IEnumerable<IInformationImputationTfs> imputations;
            EnumTypeItem typeItem = (groupeItemFiltre != null) ? groupeItemFiltre.TypeItem : EnumTypeItem.Tous;
            switch (typeItem)
            {
                case EnumTypeItem.Tous:
                    imputations = modeInclusion ? source : new IInformationImputationTfs[0];
                    break;

                case EnumTypeItem.Entite:
                    string nomGroupe = groupeItemFiltre.Entite.NomGroupement;
                    imputations = source
                        .Where(i => modeInclusion == String.Equals(i.NomGroupement, nomGroupe));
                    break;

                default:
                    imputations = modeInclusion ? new IInformationImputationTfs[0] : source;
                    break;
            }

            return imputations;
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltres(IEnumerable<IInformationImputationTfs> source, TacheItem tacheItemFiltre, bool modeInclusion)
        {
            IEnumerable<IInformationImputationTfs> imputations;
            EnumTypeItem typeItem = (tacheItemFiltre != null) ? tacheItemFiltre.TypeItem : EnumTypeItem.Tous;
            switch (typeItem)
            {
                case EnumTypeItem.Tous:
                    imputations = modeInclusion ? source : new IInformationImputationTfs[0];
                    break;

                case EnumTypeItem.Entite:
                    int numeroTache = tacheItemFiltre.Entite.Numero;
                    imputations = source
                        .Where(i => modeInclusion == (i.Numero == numeroTache));
                    break;

                default:
                    imputations = modeInclusion ? new IInformationImputationTfs[0] : source;
                    break;
            }

            return imputations;
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltres(IEnumerable<IInformationImputationTfs> source, TicketItem ticketItemFiltre, bool modeInclusion)
        {
            IEnumerable<IInformationImputationTfs> imputations;
            EnumTypeItem typeItem = (ticketItemFiltre != null) ? ticketItemFiltre.TypeItem : EnumTypeItem.Tous;
            switch (typeItem)
            {
                case EnumTypeItem.Tous:
                    imputations = modeInclusion ? source : new IInformationImputationTfs[0];
                    break;

                case EnumTypeItem.Entite:
                    int numeroTache = ticketItemFiltre.Entite.Numero;
                    int? numeroComplementaireTache = ticketItemFiltre.Entite.NumeroComplementaire;
                    imputations = source
                        .Where(i => modeInclusion == (i.Numero == numeroTache))
                        .Where(i => modeInclusion == (i.NumeroComplementaire == numeroComplementaireTache));
                    break;

                default:
                    imputations = modeInclusion ? new IInformationImputationTfs[0] : source;
                    break;
            }

            return imputations;
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirInformationImputationsFiltresGenerique<T>(IEnumerable<IInformationImputationTfs> source, IEnumerable<T> itemFiltres, bool modeInclusion, Func<IEnumerable<IInformationImputationTfs>, T, bool, IEnumerable<IInformationImputationTfs>> filtre)
        {
            source = source.Execute();  // énuméré plus d'une fois
            itemFiltres = itemFiltres ?? new T[0];
            IEnumerable<IInformationImputationTfs> resultat;

            if (modeInclusion)
            {
                resultat = new IInformationImputationTfs[0];
                foreach (var itemFiltre in itemFiltres)
                {
                    IEnumerable<IInformationImputationTfs> resultatFiltre = filtre(source, itemFiltre, modeInclusion);
                    resultat = resultat.Concat(resultatFiltre);
                }
                resultat = source
                    .Join(resultat.GroupBy(r => r.DateHorodatage).Select(grp => grp.Key), s => s.DateHorodatage, d => d, (outer, inner) => outer);
            }
            else
            {
                resultat = source;
                foreach (var itemFiltre in itemFiltres)
                {
                    IEnumerable<IInformationImputationTfs> resultatFiltre = filtre(resultat, itemFiltre, modeInclusion);
                    resultat = resultatFiltre;
                }
            }

            return resultat;
        }

        #endregion ObtenirInformationImputationsFiltres

        #endregion Outils

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