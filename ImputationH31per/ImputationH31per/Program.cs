using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ImputationH31per.Data;
using ImputationH31per.Modele;
using ImputationH31per.Util;
using ImputationH31per.Vue.Main;
using ImputationH31per.Vue.Main.Modele;

namespace ImputationH31per
{
    internal static class Program
    {
        private static IImputationH31perModele _modeleApplication;
        private static FileStream _globalTraceLog;
        private static TextWriterTraceListener _globalTraceListener;

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Trace.WriteLine("Exception (Application) non gérée");
            Trace.WriteLine(e.Exception);

            MessageBox.Show("Exception innatendue levée - le programme va s'arreter.");

            EnregistrerDonneeSecoursSafe();
            FermerLoggeurSafe();

            Application.Exit();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.WriteLine("Exception (Domaine) non gérée");
            Trace.WriteLine(e.ExceptionObject);

            MessageBox.Show("Exception innatendue levée - le programme va s'arreter.");

            EnregistrerDonneeSecoursSafe();
            FermerLoggeurSafe();

            Application.Exit();
        }

        private static void EnregistrerDonneeSecoursSafe()
        {
            if (_modeleApplication != null)
            {
                try
                {
                    Trace.WriteLine("Sauvegarde de secours en cours");

                    IServiceData serviceSecours = FabriqueServiceData.ServiceSecours;
                    IImputationH31perControleur controleurApplication = new ImputationH31perControleur(_modeleApplication);
                    controleurApplication.EnregistrerData(serviceSecours);

                    Trace.WriteLine("Sauvegarde de secours terminée");
                }
                catch (Exception exception)
                {
                    Trace.WriteLine("Exception levée durant la sauvegarde de secours");
                    Trace.WriteLine(exception);
                }
            }
        }

        private static void FermerLoggeurSafe()
        {
            try
            {
                Trace.Flush();
                Debug.Flush();
                _globalTraceListener.Flush();
                _globalTraceLog.Flush();
                _globalTraceLog.Close();

            }
            catch (Exception)
            {
                Debug.Fail("Fermeture du Loggeur échouée");
            }
        }

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Trace et Debug loggeur
            _globalTraceLog = new FileStream(FabriqueServiceData.CheminFichierLog, FileMode.Append, FileAccess.Write);
            _globalTraceListener = new TextWriterTraceListener(_globalTraceLog);
            Trace.Listeners.Add(_globalTraceListener);
            //Debug.Listeners.Add(_globalTraceListener);   // en mode release, aucun log ne sortira pour les instruction Debug.XXX(...)

            Trace.WriteLine("");
            Trace.WriteLine("===== " + Application.ProductName + " - " + DateTimeOffset.UtcNow.ToLocalTime().ToString() + " =====");
            Debug.WriteLine("Avec log [Debug]");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

#warning TODO - point DELTA - faire sauvegarde périodique

#warning TODO - clarifier les DateTimeOffsetet la local/utc

            /************ Zone teste ****************/

            /****************************************/

            // initialisation du servide de paramètre de fenêtre
            IServiceData servicePreference = FabriqueServiceData.ServicePrefParDefaut;
            ServicePreferenceModele.Instance.ChargerPreference(servicePreference);

            IImputationH31perModele modeleApplication = new ImputationH31perModele();
            _modeleApplication = modeleApplication;

            // initialisation du modele
            IImputationH31perControleur controleurApplication = new ImputationH31perControleur(modeleApplication);
            controleurApplication.ChargerDataParDefaut();

            IIHFormModele formModele = ServicePreferenceModele.Instance.ObtenirIHFormModele(ServicePreferenceModele.ConstanteNomFormMainForm);
            MainFormModele mainFormModele = new MainFormModele(modeleApplication);
            MainFormControleur mainFormControleur = new MainFormControleur(mainFormModele);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(MainForm.Nouveau(formModele, mainFormModele, mainFormControleur));

            ServicePreferenceModele.Instance.EnregistrerPreference();

            FermerLoggeurSafe();
        }
    }
}