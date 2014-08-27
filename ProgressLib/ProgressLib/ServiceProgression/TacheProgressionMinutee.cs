using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Permet de créer une tâche de progression (sans progression) avec un arrêt minuté.
    /// 
    /// Des verroux permettent l'utilisation dans un contexte multi-threads.
    /// </summary>
    internal sealed class TacheProgressionMinutee : TacheProgressionBase, ITacheProgression
    {

        #region Membres

        private EnumEtatProgression _etatProgression;
        private bool _estArretEnCours;

        private object _verrouStatutProgression = new object();
        private object _verrouTacheProgression = new object();

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public TacheProgressionMinutee()
        {
            _etatProgression = EnumEtatProgression.Initialisee;
        }

        #endregion

        #region ITacheProgression

        /// <summary>
        /// Minimum.
        /// </summary>
        public override ArgumentStatutProgression StatutProgression
        {
            get
            {
                ArgumentStatutProgression resultat;
                lock (_verrouStatutProgression)
                {
                    resultat = new ArgumentStatutProgression(0, 1, 1, _etatProgression, null);
                }
                return resultat;
            }
        }

        /// <summary>
        /// Objet utilisé pour la synchronisation des tâches. A UTILISER AVEC PRECAUTION.
        /// </summary>
        public override object VerrouTacheProgression
        {
            get { return _verrouTacheProgression; }
        }

        /// <summary>
        /// Obtient l'état indiquant si un arrêt a été programmé sur la tâche.
        /// Retourne également vrai si la tâche est terminé.
        /// </summary>
        public bool EstArretEnCours
        {
            get
            {
                bool resultat;
                lock (_verrouStatutProgression)
                {
                    resultat = _estArretEnCours;
                }
                return resultat;
            }
        }

        #endregion

        #region Méthodes public

        /// <summary>
        /// Démarrer la progression.
        /// </summary>
        public void Demarrer()
        {
            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = DemarrerCore();
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        /// <summary>
        /// Terminer la tâche avec la pause.
        /// La tâche est automatique amené à 100% (progression est définit à la valeur Maximum)
        /// </summary>
        public void TerminerAvecPause(int tempsPauseMillisecondes)
        {
            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = TerminerAvecPauseCore(tempsPauseMillisecondes);
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        /// <summary>
        /// Terminer la tâche.
        /// La tâche est automatique amené à 100% (progression est définit à la valeur Maximum)
        /// </summary>
        public void Terminer()
        {
            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = TerminerCore();
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        #endregion

        #region Méthodes privées

        private StatutProgressionEventArgs[] DemarrerCore()
        {
            if (_etatProgression == EnumEtatProgression.ProgressionEnCours || _etatProgression == EnumEtatProgression.ProgressionIndeterminee)
            {
                Debug.Fail("La tâche de progression est déjà démarrée.");
                return new StatutProgressionEventArgs[] { };
            }

            _etatProgression = EnumEtatProgression.ProgressionEnCours;

            StatutProgressionEventArgs e = new StatutProgressionEventArgs(StatutProgression, EnumStatutTacheProgression.Demarrage);
            return new[] { e };
        }

        private StatutProgressionEventArgs[] TerminerAvecPauseCore(int tempsPauseMillisecondes)
        {
            StatutProgressionEventArgs[] events = new StatutProgressionEventArgs[] { };
            if (_estArretEnCours || (_etatProgression == EnumEtatProgression.Terminee))
            {
                Debug.Fail("Arret déjà programmé/effectué");
                return events;
            }

            _estArretEnCours = true;

            Action action = delegate
            {
                Thread.Sleep(tempsPauseMillisecondes);
                this.Terminer();
            };
            Thread thread = new Thread(new ThreadStart(action));
            thread.Start();
            // attend son exécution
            while (!thread.IsAlive) ;

            return events;
        }

        private StatutProgressionEventArgs[] TerminerCore()
        {
            List<StatutProgressionEventArgs> events = new List<StatutProgressionEventArgs> { };

            Debug.Assert(_etatProgression == EnumEtatProgression.ProgressionEnCours);

            _etatProgression = EnumEtatProgression.Terminee;
            _estArretEnCours = true;    // assuré la cohérence de l'état

            StatutProgressionEventArgs e = new StatutProgressionEventArgs(StatutProgression, EnumStatutTacheProgression.Arret);
            events.Add(e);

            return events.ToArray();
        }

        #endregion

    }
}
