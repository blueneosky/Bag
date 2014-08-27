using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImputationH31per.Data;
using ImputationH31per.Data.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Modele
{
    public class ServicePreferenceModele
    {
        public const string ConstanteNomFormAjouterImputationTfs = "AjouterImputationTfs";
        public const string ConstanteNomFormImportExcel = "ImportExcel";
        public const string ConstanteNomFormImputationsCourantes = "ImputationsCourantes";
        public const string ConstanteNomFormMainForm = "MainForm";
        public const string ConstanteNomFormModifierImputationTfs = "ModifierImputationTfs";
        public const string ConstanteNomFormRapportMail = "RapportMail";
        public const string ConstanteNomFormRapportMensuel = "RapportMensuel";

        #region Singloton

        private static ServicePreferenceModele _instance;

        public static ServicePreferenceModele Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServicePreferenceModele();
                return _instance;
            }
        }

        #endregion Singloton

        private static Dictionary<string, DatIHFormParametre> _parametreParNoms;
        private IServiceData _serviceDataPreference;

        private ServicePreferenceModele()
        {
            _parametreParNoms = new Dictionary<string, DatIHFormParametre>();
        }

        public void ChargerPreference(IServiceData serviceDataPreference)
        {
            _serviceDataPreference = serviceDataPreference;
            if (_serviceDataPreference == null)
                return;

            IDatData<IDatTacheTfs, IDatIHFormParametre> preferences = _serviceDataPreference.ObtenirData();
            IEnumerable<IDatIHFormParametre> ihFormParametres = preferences.IHFormParametres;

            _parametreParNoms.Clear();
            foreach (IDatIHFormParametre ihfp in ihFormParametres)
            {
                DatIHFormParametre parametre = new DatIHFormParametre(ihfp.Nom);
                parametre.EstAgrandi = ihfp.EstAgrandi;
                parametre.Localisation = ihfp.Localisation;
                parametre.Taille = ihfp.Taille;
                _parametreParNoms[parametre.Nom] = parametre;
            }
        }

        public void EnregistrerIHFormModele(IIHFormModele modele)
        {
            string nom = modele.Nom;
            Debug.Assert(!String.IsNullOrEmpty(nom));

            DatIHFormParametre ihfp;
            bool succes = _parametreParNoms.TryGetValue(nom, out ihfp);
            if (false == succes)
            {
                ihfp = new DatIHFormParametre(nom);
                _parametreParNoms.Add(nom, ihfp);
            }
            ihfp.EstAgrandi = modele.EstAgrandi;
            ihfp.Localisation = modele.Localisation;
            ihfp.Taille = modele.Taille;
        }

        public void EnregistrerPreference()
        {
            if (_serviceDataPreference == null)
                return;

            IEnumerable<DatIHFormParametre> parametres = _parametreParNoms.Values;
            DatData data = new DatData();
            data.IHFormParametres = parametres;
            _serviceDataPreference.EnregistrerData(data);
        }

        public IIHFormControleur ObtenirIHFormControleur(IIHFormModele modele)
        {
            if (modele == null)
                return null;

            IIHFormControleur resultat = new IHFormControleur(modele);

            return resultat;
        }

        public IIHFormModele ObtenirIHFormModele(string nom)
        {
            if (String.IsNullOrEmpty(nom))
                return null;

            IHFormModele resultat = new IHFormModele(nom);
            DatIHFormParametre ihfp;
            bool succes = _parametreParNoms.TryGetValue(nom, out ihfp);
            if (succes)
            {
                resultat.EstAgrandi = ihfp.EstAgrandi;
                resultat.Localisation = ihfp.Localisation;
                resultat.Taille = ihfp.Taille;
                resultat.EstDefini = true;
            }
            else
            {
                resultat.EstDefini = false;
            }

            return resultat;
        }
    }
}