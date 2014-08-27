using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public interface IImputationsCourantesFormControleur
    {
        bool AjouterImputationTfs();

        void Enregistrer();

        void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs);
    }
}