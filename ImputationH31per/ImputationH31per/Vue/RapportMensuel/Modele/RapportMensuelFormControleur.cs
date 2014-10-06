using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;
using ImputationH31per.Modele.Entite;

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
            _modele.AjouterAuRegroupement(groupeItem);
        }

        public void AjouterAuRegroupement(TacheItem tacheItem)
        {
            if (tacheItem == null) return;
            _modele.AjouterAuRegroupement(tacheItem);
        }

        public void AjouterAuRegroupement(TicketItem ticketItem)
        {
            if (ticketItem == null) return;
            _modele.AjouterAuRegroupement(ticketItem);
        }

        public void RetirerDuRegroupement(IInformationItem<IInformationTacheTfs> informationItem)
        {
            if (informationItem == null) return;
            _modele.RetirerDuRegroupement(informationItem);
        }

        #endregion
    }
}