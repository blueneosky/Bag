using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public interface IRapportMensuelFormControleur
    {
        void DefinirDateMoisAnnee(DateTimeOffset dateTime);

        void DefinirGroupeSelectionne(GroupeItem groupeItem);

        void DefinirTacheSelectionnee(TacheItem tacheItem);

        void DefinirTicketSelectionne(TicketItem ticketItem);

        void AjouterAuRegroupement(GroupeItem groupeItem);

        void AjouterAuRegroupement(TacheItem tacheItem);

        void AjouterAuRegroupement(TicketItem ticketItem);

        void RetirerDuRegroupement(IInformationItem<IInformationTacheTfs> informationItem);

        void DefinirRegroupementCourantItemSelectionne(IInformationItem<IInformationTacheTfs> informationItem);
    }
}