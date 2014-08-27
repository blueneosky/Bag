using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ImputationH31per.Vue.EditeurImputationTfs.Modele
{
    public interface IEditeurImputationTfsFormModele : IEditeurImputationTfsChoixSourceModele, INotifyPropertyChanged
    {
        bool EstNumeroImputationTfsModifiable { get; }

        ImputationTfsDataEditeur ImputationTfs { get; }
    }
}