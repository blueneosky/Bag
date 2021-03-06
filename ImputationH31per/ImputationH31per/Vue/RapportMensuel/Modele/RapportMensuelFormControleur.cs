﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public class RapportMensuelFormControleur : IRapportMensuelFormControleur
    {
        #region Membres

        private readonly IRapportMensuelFormModele _modele;

        #endregion Membres

        #region ctor

        public RapportMensuelFormControleur(IRapportMensuelFormModele modele)
        {
            this._modele = modele;
        }

        #endregion ctor

        #region IRapportMensuelFormControleur Membres

        public void DefinirDateMoisAnnee(DateTimeOffset dateTime)
        {
            _modele.DateMoisAnnee = dateTime;
        }

        public void DefinirGroupeSelectionne(GroupeItem groupe)
        {
            _modele.GroupeSelectionne = groupe;
        }

        public void DefinirTacheSelectionnee(TacheItem tache)
        {
            _modele.TacheSelectionnee = tache;
        }

        public void DefinirTicketSelectionne(TicketItem ticket)
        {
            _modele.TicketSelectionne = ticket;
        }

        public void AjouterAuRegroupement(GroupeItem groupeItem)
        {
            if (groupeItem == null) return;
            AjouterAuRegroupementCeur(groupeItem);
        }

        public void AjouterAuRegroupement(TacheItem tacheItem)
        {
            if (tacheItem == null) return;
            if (tacheItem.TypeItem == EnumTypeItem.Tous)
            {
                AjouterAuRegroupement(_modele.GroupeSelectionne);
            }
            else
            {
                AjouterAuRegroupementCeur(tacheItem);
            }
        }

        public void AjouterAuRegroupement(TicketItem ticketItem)
        {
            if (ticketItem == null) return;
            if (ticketItem.TypeItem == EnumTypeItem.Tous)
            {
                AjouterAuRegroupement(_modele.TacheSelectionnee);
            }
            else
            {
                AjouterAuRegroupementCeur(ticketItem);
            }
        }

        private void AjouterAuRegroupementCeur(IInformationItem<IInformationTacheTfs> informationItem)
        {
            _modele.AjouterAuRegroupement(informationItem);
            if (_modele.RegroupementCourant.Count() == 1)
            {
                // premier item -> nommage auto
                DefinirNomRegroupementCourant(informationItem.Libelle);
            }
        }

        public void RetirerDuRegroupement(IInformationItem<IInformationTacheTfs> informationItem)
        {
            if (informationItem == null) return;
            _modele.RetirerDuRegroupement(informationItem);
        }

        public void DefinirRegroupementCourantItemSelectionne(IInformationItem<IInformationTacheTfs> informationItem)
        {
            _modele.RegroupementCourantItemSelectionne = informationItem;
        }

        public void DefinirNomRegroupementCourant(string nom)
        {
            nom = _modele.Regroupements.Select(r => r.Nom).NomUnique(nom);
            _modele.RegroupementCourant.Nom = nom;
        }

        public void RegroupementsItemSelectionne(Regroupement regroupement)
        {
            _modele.RegroupementsItemSelectionne = regroupement;
        }

        public void AjouterRegroupementCourant()
        {
            _modele.AjouterRegroupementCourant();
        }

        public void RetirerDeRegroupements(Regroupement regroupement)
        {
            if (regroupement == null) return;
            _modele.RetirerDeRegroupements(regroupement);
        }

        #endregion IRapportMensuelFormControleur Membres
    }
}