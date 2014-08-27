using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public interface IAjoutImputationTfsFormModele : IEditeurImputationTfsFormModele
    {
        IImputationsCourantesFormModele ImputationsCourantesFormModele { get; }

        void DefinirImputation(ImputationTfsDataEditeur imputationTfs);
    }
}