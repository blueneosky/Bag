﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface IInformationImputationTfsNotifiable : IInformationImputationTfs, IInformationTicketTfsNotifiable, INotifyPropertyChanged
    {
    }
}