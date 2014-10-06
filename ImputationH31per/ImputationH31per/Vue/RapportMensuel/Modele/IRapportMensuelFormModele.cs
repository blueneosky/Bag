using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public interface IRapportMensuelFormModele : INotifyPropertyChanged
    {
        IImputationH31perModele ImputationH31perModele { get; }

        DateTimeOffset DateMoisAnnee { get; set; }

        IEnumerable<IInformationImputationTfs> ImputationsDuMois { get; }

        IEnumerable<IInformationImputationTfs> ImputationsPourRegroupementCourant { get; }

        IEnumerable<IInformationImputationTfs> ImputationRestantes { get; }

        IEnumerable<IInformationImputationTfs> ImputationPourGroupes { get; }

        IEnumerable<GroupeItem> Groupes { get; }

        GroupeItem GroupeSelectionne { get; set; }

        IEnumerable<IInformationImputationTfs> ImputationPourTaches { get; }

        IEnumerable<TacheItem> Taches { get; }

        TacheItem TacheSelectionnee { get; set; }

        IEnumerable<IInformationImputationTfs> ImputationPourTickets { get; }

        IEnumerable<TicketItem> Tickets { get; }

        TicketItem TicketSelectionne { get; set; }

        IEnumerable<IInformationItem<IInformationTacheTfs>> ItemsRegroupementCourant { get; }

        IInformationItem<IInformationTacheTfs> ItemRegroupementCourantSelectionne { get; set; }

        IEnumerable<IInformationTacheTfs> ImputationsDuRegroupementCourant { get; }

        //------------------

        void AjouterAuRegroupement(IInformationItem<IInformationTacheTfs> item);

        void RetirerDuRegroupement(IInformationItem<IInformationTacheTfs> item);
    }
}