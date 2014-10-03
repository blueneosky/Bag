using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        #endregion

    }
}