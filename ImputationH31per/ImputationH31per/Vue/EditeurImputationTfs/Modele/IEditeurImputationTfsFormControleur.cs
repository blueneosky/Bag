using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Vue.EditeurImputationTfs.Modele
{
    public interface IEditeurImputationTfsFormControleur
    {
        #region Méthodes

        void DefinirCommentaire(string commentaire);

        void DefinirDateEstimCourant(DateTimeOffset dateEstimCourant);

        void DefinirDates(DateTimeOffset date);

        void DefinirDateSommeConsommee(DateTimeOffset dateSommeConsommee);

        void DefinirEstimCourant(string estimCourant);

        void DefinirEstimCourant(double? estimCourant);

        void DefinirEstTacheAvecEstim(bool estTacheAvecEstim);

        void DefinirNom(string nom);

        void DefinirNomComplementaire(string nom);

        void DefinirNomGroupement(string nomGroupement);

        void DefinirNumeroEtNumeroComplementaire(int? numero, int? numeroComplementaire);

        void DefinirNumeroEtNumeroComplementaire(string numero, string numeroComplementaire);

        void DefinirSommeConsommee(string sommeConsommee);

        void DefinirSommeConsommee(double? sommeConsommee);

        #endregion Méthodes
    }
}