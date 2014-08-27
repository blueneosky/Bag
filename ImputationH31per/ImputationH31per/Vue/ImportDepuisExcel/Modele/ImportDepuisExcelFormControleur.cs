using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Vue.ImportDepuisExcel.Modele
{
    public class ImportDepuisExcelFormControleur : IImportDepuisExcelFormControleur
    {
        private readonly IImputationH31perControleur _ImputationH31perControleur;
        private readonly IImportDepuisExcelFormModele _modele;

        public ImportDepuisExcelFormControleur(IImportDepuisExcelFormModele modele)
            : this(modele, new ImputationH31perControleur(modele.ImputationH31perModele))
        {
        }

        public ImportDepuisExcelFormControleur(
           IImportDepuisExcelFormModele modele
           , IImputationH31perControleur ImputationH31perControleur)
        {
            _modele = modele;
            _ImputationH31perControleur = ImputationH31perControleur;
        }

        public void Extraire(string texteImport)
        {
            ExtraireCore(texteImport);
        }

        public void Importer()
        {
            IEnumerable<IImputationTfsNotifiable> imputationTfsAImporters = _modele.ImputationTfss
                .OrderBy(i => i.DateHorodatage, ImputationTfs.ComparateurDateHorodatageCroissant);

            _ImputationH31perControleur.AjouterImputationTfs(imputationTfsAImporters);
        }

        public void SupprimerImputationTfs(ImputationH31per.Modele.Entite.IImputationTfsNotifiable imputationTfs)
        {
            _modele.SupprimerImputationTfs(imputationTfs);
        }

        #region Core

        private void ExtraireCore(string texteImport)
        {
            const int ConstanteHeureLimiteImputation = 17;  // heure

            try
            {
                _modele.NettoyerImputationTfs();

                IEnumerable<string> ligneImports = texteImport.Split(new[] { '\n', '\r' })
                       .Where(l => l.Length > 0);
                IEnumerable<IImputationTfsNotifiable> imputationTfss = ligneImports
                    .Select((ligneImport, indiceLigne) => ExtraireLigneCore(ligneImport, indiceLigne))
                    .OrderBy(i => i, ImputationTfs.ComparateurImputationTfsCroissant);

                DateTimeOffset dernierDate = DateTimeOffset.MinValue;
                foreach (IImputationTfsNotifiable iteratuerImputationTfs in imputationTfss)
                {
                    IImputationTfsNotifiable imputationTfs = iteratuerImputationTfs;
                    DateTimeOffset date = imputationTfs.DateHorodatage;
                    bool estDateChangee = false;

                    int heure = date.Hour;
                    if (heure < ConstanteHeureLimiteImputation)
                    {
                        // recalage pour amener l'impute ) 17h
                        date = date.AddHours(ConstanteHeureLimiteImputation - heure);
                        estDateChangee = true;
                    }

                    if (date <= dernierDate)
                    {
                        date = dernierDate.AddMinutes(2);
                        estDateChangee = true;
                    }

                    dernierDate = date;

                    if (estDateChangee)
                    {
                        IImputationTfsNotifiable nouvelleImputationTfs = new ImputationTfsData(imputationTfs, date);
                        nouvelleImputationTfs.DefinirProprietes(imputationTfs);
                        imputationTfs = nouvelleImputationTfs;
                    }

                    // mise à jour du modele (ajout)
                    _modele.AjouterImputationTfs(imputationTfs);
                }
            }
            catch (IHException e)
            {
                _modele.NettoyerImputationTfs();
                throw e;
            }
            catch (Exception e)
            {
                _modele.NettoyerImputationTfs();
                throw new IHException("Erreur innatendu : " + e.Message);
            }
        }

        private IImputationTfsNotifiable ExtraireLigneCore(string ligneImport, int indiceLigne)
        {
            int numeroLigne = indiceLigne + 1;
            // textes des colonnes
            string[] colonnes = ligneImport.Split(new[] { '\t' });

            // textes individuels
            string texteDateHorodatage = null;
            string texteNumero = null;
            string texteNom = null;
            string texteNumeroComplementaire = null;
            string texteNomComplementaire = null;
            string texteEstim = null;
            string texteDateEstim = null;
            string texteConsommee = null;
            string texteDateConsommee = null;

            // extraction des textes individuels
            int indiceColonne = 0;
            try
            {
                texteDateHorodatage = colonnes[indiceColonne]; indiceColonne++;
                texteNumero = colonnes[indiceColonne]; indiceColonne++;
                texteNom = colonnes[indiceColonne]; indiceColonne++;
                texteNumeroComplementaire = colonnes[indiceColonne]; indiceColonne++;
                texteNomComplementaire = colonnes[indiceColonne]; indiceColonne++;
                texteEstim = colonnes[indiceColonne]; indiceColonne++;
                texteDateEstim = colonnes[indiceColonne]; indiceColonne++;
                texteConsommee = colonnes[indiceColonne]; indiceColonne++;
                texteDateConsommee = colonnes[indiceColonne]; indiceColonne++;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IHException("Nombre de colonne (" + (indiceColonne) + ") innatendu en ligne " + numeroLigne);
            }

            // données individuels
            DateTimeOffset dateHorodatage;
            int numero;
            string nom;
            int? numeroComplementaire;
            string nomComplementaire;
            double? estim;
            DateTimeOffset? dateEstim;
            double? consommee;
            DateTimeOffset? dateConsommee;

            // extraction des données
            indiceColonne = 0;
            try
            {
                dateHorodatage = ObtenirDate(texteDateHorodatage);
                numero = ObtenirEntier(texteNumero);
                nom = texteNom;
                numeroComplementaire = ObtenirEntierNullable(texteNumeroComplementaire);
                nomComplementaire = texteNomComplementaire;
                estim = ObtenirDoubleNullable(texteEstim);
                dateEstim = ObtenirDateNullable(texteDateEstim);
                consommee = ObtenirDoubleNullable(texteConsommee);
                dateConsommee = ObtenirDateNullable(texteDateConsommee);
            }
            catch (IHException e)
            {
                throw new IHException("Ligne (" + numeroLigne + ";" + (indiceColonne + 1) + ") : " + e.Message);
            }

            // construction de l'imputation
            ImputationTfsData imputationTfs = new ImputationTfsData(numero, numeroComplementaire, dateHorodatage);

            if (estim.HasValue != dateEstim.HasValue)
                throw new Exception("Ligne (" + numeroLigne + ") : valeur et date Estimation incohérente {" + estim + ";" + dateEstim + "}");
            if (consommee.HasValue != dateConsommee.HasValue)
                throw new Exception("Ligne (" + numeroLigne + ") : valeur et date Consommée incohérente {" + consommee + ";" + dateConsommee + "}");
            bool estAvecEstim = estim.HasValue;

            imputationTfs.Nom = nom;
            imputationTfs.NomComplementaire = nomComplementaire;
            imputationTfs.EstTacheAvecEstim = estAvecEstim;
            if (estim.HasValue)
            {
                imputationTfs.EstimCourant = estim;
                imputationTfs.DateEstimCourant = dateEstim;
            }
            if (consommee.HasValue)
            {
                imputationTfs.SommeConsommee = consommee;
                imputationTfs.DateSommeConsommee = dateConsommee;
            }

            return imputationTfs;
        }

        #endregion Core

        #region Méthodes utilitaires

        private static DateTimeOffset ObtenirDate(string texte)
        {
            DateTimeOffset resultat;
            try
            {
                resultat = ObtenirDateNullable(texte).Value;
            }
            catch (Exception)
            {
                throw new IHException("\"" + texte + "\" n'est pas une date valide.");
            }

            return resultat;
        }

        private static DateTimeOffset? ObtenirDateNullable(string texte)
        {
            DateTimeOffset? resultat = null;
            if (!String.IsNullOrEmpty(texte))
            {
                DateTime valeur;
                bool succes = DateTime.TryParse(texte, out valeur);
                if (false == succes)
                {
                    throw new IHException("\"" + texte + "\" n'est pas une date nullable valide.");
                }
                resultat = valeur.ToDateTimeOffset();
            }

            return resultat;
        }

        private static double ObtenirDouble(string texte)
        {
            double resultat;
            try
            {
                resultat = ObtenirEntierNullable(texte).Value;
            }
            catch (Exception)
            {
                throw new IHException("\"" + texte + "\" n'est pas un entier valide.");
            }

            return resultat;
        }

        private static double? ObtenirDoubleNullable(string texte)
        {
            double? resultat = null;
            if (!String.IsNullOrEmpty(texte))
            {
                double valeur;
                bool succes = Double.TryParse(texte, out valeur);
                if (false == succes)
                {
                    throw new IHException("\"" + texte + "\" n'est pas un entier nullable valide.");
                }
                resultat = valeur;
            }

            return resultat;
        }

        private static int ObtenirEntier(string texte)
        {
            int resultat;
            try
            {
                resultat = ObtenirEntierNullable(texte).Value;
            }
            catch (Exception)
            {
                throw new IHException("\"" + texte + "\" n'est pas un entier valide.");
            }

            return resultat;
        }

        private static int? ObtenirEntierNullable(string texte)
        {
            int? resultat = null;
            if (!String.IsNullOrEmpty(texte))
            {
                int valeur;
                bool succes = Int32.TryParse(texte, out valeur);
                if (false == succes)
                {
                    throw new IHException("\"" + texte + "\" n'est pas un entier nullable valide.");
                }
                resultat = valeur;
            }

            return resultat;
        }

        #endregion Méthodes utilitaires
    }
}