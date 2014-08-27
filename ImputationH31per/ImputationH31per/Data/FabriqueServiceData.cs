using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Data.Xml;

namespace ImputationH31per.Data
{
    public static class FabriqueServiceData
    {
        #region Config par défaut

        private const string ConstanteNomDossierData = ".ImputationH31per";
        private const string ConstanteNomFichierData = "ImputationH31per.Data";
        private const string ConstanteNomFichierLog = "ImputationH31lper.log";
        private const string ConstanteNomFichierPref = "ImputationH31per.Pref";
        private const string ConstanteNomFichierSecours = "_ImputationH31per.Rescue";
        private static string _cheminFichierDataDefaut;
        private static string _cheminFichierLog;
        private static string _cheminFichierPrefDefaut;
        private static string _cheminFichierSecours;

        public static string CheminFichierLog
        {
            get
            {
                if (String.IsNullOrEmpty(_cheminFichierLog))
                    _cheminFichierLog = ObtenirCheminFichierLog();
                return _cheminFichierLog;
            }
        }

        private static string CheminFichierDataDefaut
        {
            get
            {
                if (String.IsNullOrEmpty(_cheminFichierDataDefaut))
                    _cheminFichierDataDefaut = ObtenirCheminFichierDataParDefaut();
                return _cheminFichierDataDefaut;
            }
        }

        private static string CheminFichierPrefDefaut
        {
            get
            {
                if (String.IsNullOrEmpty(_cheminFichierPrefDefaut))
                    _cheminFichierPrefDefaut = ObtenirCheminFichierPrefParDefaut();
                return _cheminFichierPrefDefaut;
            }
        }

        private static string CheminFichierSecours
        {
            get
            {
                if (String.IsNullOrEmpty(_cheminFichierSecours))
                    _cheminFichierSecours = ObtenirCheminFichierSecours();
                return _cheminFichierSecours;
            }
        }

        private static string ObtenirCheminBaseFichier()
        {
            //string cheminBase = Application.LocalUserAppDataPath;   // idéalement => fuck !
            string cheminBase = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            cheminBase = Path.Combine(cheminBase, ConstanteNomDossierData);

            Debug.Assert(!String.IsNullOrEmpty(Application.StartupPath));    // pour éviter tout problème de nettoyage de using
#if DEBUG
            cheminBase = Application.StartupPath;
#endif

            return cheminBase;
        }

        private static string ObtenirCheminFichier(string fichier)
        {
            string cheminBase = ObtenirCheminBaseFichier();
            string resultat = Path.Combine(cheminBase, fichier);

            return resultat;
        }

        private static string ObtenirCheminFichierDataParDefaut()
        {
            return ObtenirCheminFichier(ConstanteNomFichierData);
        }

        private static string ObtenirCheminFichierLog()
        {
            return ObtenirCheminFichier(ConstanteNomFichierLog);
        }

        private static string ObtenirCheminFichierPrefParDefaut()
        {
            return ObtenirCheminFichier(ConstanteNomFichierPref);
        }

        private static string ObtenirCheminFichierSecours()
        {
            return ObtenirCheminFichier(ConstanteNomFichierSecours);
        }

        #endregion Config par défaut

        private static IServiceData _serviceDataParDefaut;
        private static IServiceData _servicePrefParDefaut;
        private static IServiceData _serviceSecours;

        public static IServiceData ServiceDataParDefaut
        {
            get
            {
                if (_serviceDataParDefaut == null)
                    _serviceDataParDefaut = ObtenirServiceDataXml(CheminFichierDataDefaut, "Défaut");
                return _serviceDataParDefaut;
            }
        }

        public static IServiceData ServicePrefParDefaut
        {
            get
            {
                if (_servicePrefParDefaut == null)
                    _servicePrefParDefaut = ObtenirServiceDataXml(CheminFichierPrefDefaut, null);
                return _servicePrefParDefaut;
            }
        }

        public static IServiceData ServiceSecours
        {
            get
            {
                if (_serviceSecours == null)
                    _serviceSecours = ObtenirServiceDataXml(CheminFichierSecours, null);
                return _serviceSecours;
            }
        }

        public static IServiceData ObtenirServiceDataXml(string cheminFichier, string nomService)
        {
            ServiceDataXml service = new ServiceDataXml(cheminFichier);
            service.Nom = nomService;

            return service;
        }
    }
}