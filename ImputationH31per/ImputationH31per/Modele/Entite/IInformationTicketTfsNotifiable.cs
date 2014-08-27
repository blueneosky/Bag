using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface IInformationTicketTfsNotifiable : IInformationTicketTfs, IInformationTacheTfsNotifiable, INotifyPropertyChanged
    {
    }
}