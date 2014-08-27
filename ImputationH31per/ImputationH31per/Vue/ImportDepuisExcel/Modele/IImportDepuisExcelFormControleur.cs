using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Vue.ImportDepuisExcel.Modele
{
    public interface IImportDepuisExcelFormControleur
    {
        void Extraire(string texteImport);

        void Importer();

        void SupprimerImputationTfs(ImputationH31per.Modele.Entite.IImputationTfsNotifiable imputationTfs);
    }
}