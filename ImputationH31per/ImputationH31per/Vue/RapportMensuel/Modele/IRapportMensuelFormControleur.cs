using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public interface IRapportMensuelFormControleur
    {
        void DefinirDateMoisAnnee(DateTime dateTime);

        void DefinirGroupeSelectionne(GroupeItem groupeItem);

        void DefinirTacheSelectionnee(TacheItem tacheItem);

        void DefinirTicketSelectionne(TicketItem ticketItem);
    }
}