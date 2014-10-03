using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

#warning TODO - point ALPHA - implémenter !
        public void DefinirDateMoisAnnee(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void DefinirGroupeSelectionne(Entite.GroupeItem groupeItem)
        {
            throw new NotImplementedException();
        }

        public void DefinirTacheSelectionnee(Entite.TacheItem tacheItem)
        {
            throw new NotImplementedException();
        }

        public void DefinirTicketSelectionne(Entite.TicketItem ticketItem)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}