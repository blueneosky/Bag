using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;

using IImputationTfsModele = ImputationH31per.Modele.Entite.IImputationTfsNotifiable;

namespace ImputationH31per.Modele
{
    public static class ExtensionImputationH31perControleur
    {
        public static IImputationTfsModele AjouterImputationTfs(this IImputationH31perControleur controleur, IInformationImputationTfs imputationTfs)
        {
            return controleur.AjouterImputationTfs(new[] { imputationTfs }).FirstOrDefault();
        }
    }
}