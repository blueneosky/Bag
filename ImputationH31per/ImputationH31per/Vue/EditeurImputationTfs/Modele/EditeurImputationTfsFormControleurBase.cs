using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ImputationH31per.Util;

namespace ImputationH31per.Vue.EditeurImputationTfs.Modele
{
    public abstract class EditeurImputationTfsFormControleurBase : IEditeurImputationTfsFormControleur
    {
        #region Constantes

        private static Regex ConstanteRegexDiffNombre = new Regex(@"(\d+(?:[,.]\d+)?)");
        private static Regex ConstanteRegexDiffTexte = new Regex(@"([\+\-])");

        #endregion Constantes

        #region Membres

        private readonly IEditeurImputationTfsFormModele _modele;

        #endregion Membres

        #region ctor

        protected EditeurImputationTfsFormControleurBase(IEditeurImputationTfsFormModele modele)
        {
            this._modele = modele;
        }

        #endregion ctor

        #region Méthodes

        #region Abstract

        public abstract void DefinirNumeroEtNumeroComplementaire(int? numero, int? numeroComplementaire);

        #endregion Abstract

        public void DefinirCommentaire(string commentaire)
        {
            _modele.ImputationTfs.Commentaire = commentaire;
        }

        public virtual void DefinirDateEstimCourant(DateTimeOffset dateEstimCourant)
        {
            _modele.ImputationTfs.DateEstimCourant = dateEstimCourant;
        }

        public void DefinirDates(DateTimeOffset date)
        {
            if (_modele.ImputationTfs.EstDateEstimCourantModifiable)
                DefinirDateEstimCourant(date);
            if (_modele.ImputationTfs.EstDateSommeConsommeeModifiable)
                DefinirDateSommeConsommee(date);
        }

        public virtual void DefinirDateSommeConsommee(DateTimeOffset dateSommeConsommee)
        {
            _modele.ImputationTfs.DateSommeConsommee = dateSommeConsommee;
        }

        public virtual void DefinirEstimCourant(string estimCourant)
        {
            if (String.IsNullOrEmpty(estimCourant))
            {
                DefinirEstimCourant((double?)null);
            }
            else
            {
                double valeur;

                double? diffValeur = ObtenirDiff(estimCourant);
                if (diffValeur.HasValue)
                {
                    valeur = (_modele.ImputationTfs.EstimCourant ?? 0) + diffValeur.Value;
                }
                else
                {
                    bool succes = Double.TryParse(estimCourant, out valeur);
                    if (false == succes)
                    {
                        throw new IHException("Valeur incorrecte");
                    }
                }

                DefinirEstimCourant(valeur);
            }
        }

        public virtual void DefinirEstimCourant(double? estimCourant)
        {
            _modele.ImputationTfs.EstimCourant = estimCourant;
            if (_modele.ImputationTfs.EstimCourant.HasValue)
            {
                if (_modele.ImputationTfs.DateEstimCourant == null)
                    _modele.ImputationTfs.DateEstimCourant = DateTimeOffset.UtcNow;
            }
            else
            {
                _modele.ImputationTfs.DateEstimCourant = null;
            }
        }

        public virtual void DefinirEstTacheAvecEstim(bool estTacheAvecEstim)
        {
            _modele.ImputationTfs.EstTacheAvecEstim = estTacheAvecEstim;
        }

        public virtual void DefinirNom(string nom)
        {
            _modele.ImputationTfs.Nom = nom;
        }

        public virtual void DefinirNomComplementaire(string nomComplementaire)
        {
            _modele.ImputationTfs.NomComplementaire = nomComplementaire;
        }

        public void DefinirNomGroupement(string nomGroupement)
        {
            _modele.ImputationTfs.NomGroupement = nomGroupement;
        }

        public virtual void DefinirNumeroEtNumeroComplementaire(string numero, string numeroComplementaire)
        {
            int? valeurNumero = null;
            if (false == String.IsNullOrEmpty(numero))
            {
                int valeur;
                bool succes = Int32.TryParse(numero, out valeur);
                if (false == succes)
                {
                    throw new IHException("Numéro non valide");
                }
                valeurNumero = valeur;
            }

            int? valeurNumeroComplementaire = null;
            if (false == String.IsNullOrEmpty(numeroComplementaire))
            {
                int valeur;
                bool succes = Int32.TryParse(numeroComplementaire, out valeur);
                if (false == succes)
                {
                    throw new IHException("Numéro complémentaire non valide");
                }
                valeurNumeroComplementaire = valeur;
            }

            DefinirNumeroEtNumeroComplementaire(valeurNumero, valeurNumeroComplementaire);
        }

        public virtual void DefinirSommeConsommee(string sommeConsommee)
        {
            if (String.IsNullOrEmpty(sommeConsommee))
            {
                DefinirSommeConsommee((double?)null);
            }
            else
            {
                double valeur;

                double? diffValeur = ObtenirDiff(sommeConsommee);
                if (diffValeur.HasValue)
                {
                    valeur = (_modele.ImputationTfs.SommeConsommee ?? 0) + diffValeur.Value;
                }
                else
                {
                    bool succes = Double.TryParse(sommeConsommee, out valeur);
                    if (false == succes)
                    {
                        throw new IHException("Valeur incorrecte");
                    }
                }

                DefinirSommeConsommee(valeur);
            }
        }

        public virtual void DefinirSommeConsommee(double? sommeConsommee)
        {
            _modele.ImputationTfs.SommeConsommee = sommeConsommee;
            if (_modele.ImputationTfs.SommeConsommee.HasValue)
            {
                if (_modele.ImputationTfs.DateSommeConsommee == null)
                    _modele.ImputationTfs.DateSommeConsommee = DateTimeOffset.UtcNow;
            }
            else
            {
                _modele.ImputationTfs.DateSommeConsommee = null;
            }
        }

        private static double? ObtenirDiff(string texte)
        {
            string[] mots = ConstanteRegexDiffTexte.Split(texte);

            if (mots.Length != 3)
                return null;

            string videMots = mots[0];
            string signe = mots[1];
            string motNombre = mots[2];

            if (false == String.IsNullOrWhiteSpace(videMots))
                return null;

            string[] nombres = ConstanteRegexDiffNombre.Split(motNombre);

            if (nombres.Length != 3)
                return null;

            string vide1Nombres = nombres[0];
            string nombre = nombres[1];
            string vide2Nombres = nombres[2];

            if ((false == String.IsNullOrWhiteSpace(vide1Nombres))
                || (false == String.IsNullOrWhiteSpace(vide2Nombres)))
                return null;

            double diffSign = signe == "+" ? 1 : -1;
            double diffNombre;
            bool success = Double.TryParse(nombre, out diffNombre);
            if (false == success)
                return null;

            double resultat = diffSign * diffNombre;

            return resultat;
        }

        #endregion Méthodes
    }
}