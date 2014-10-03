using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;
using System.ComponentModel;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public interface IRapportMensuelFormModele : INotifyPropertyChanged
    {
        IImputationH31perModele ImputationH31perModele { get; }

        DateTime DateMoisAnnee { get; set; }

        IEnumerable<GroupeItem> Groupes { get; }

        IEnumerable<TacheItem> Taches { get; }

        IEnumerable<TicketItem> Tickets { get; }

        //------------------

        GroupeItem GroupeSelectionne { get; set; }

        TacheItem TacheSelectionnee { get; set; }

        TicketItem TicketSelectionne { get; set; }
    }
}