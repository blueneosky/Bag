using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProgressLib.ServiceProgression
{

    /// <summary>
    /// Même rôle que AgregateurTacheProgression (hétitage).
    /// Lors de l'arrêt de la dernière tâche, une tâce d'attente est inscrite et exécuté afin de laissé une pause.
    /// Une tâchepeut démarrer duanrt cette pause et continuer l'agrégation sur l'ensemble des tâches (prolongation), à la fin de cette (ou ces) nouvelles tâche une nouvelle pause sera créée.
    /// 
    /// Des verroux permettent l'utilisation dans un contexte multi-threads.
    /// </summary>
    public class AgregateurTacheProgressionAvecPause : AgregateurTacheProgression, ITacheProgression
    {
        #region Membres

        private TacheProgressionMinutee _tacheProgressionPause;
        private HashSet<TacheProgressionMinutee> _tacheProgressionPauses;
        int _tempsPauseMillisecondes;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public AgregateurTacheProgressionAvecPause()
        {
            _tacheProgressionPauses = new HashSet<TacheProgressionMinutee> { };
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Temps de pause à la fin d'une progression.
        /// </summary>
        public int TempsPauseMillisecondes
        {
            get { return _tempsPauseMillisecondes; }
            set
            {
                lock (VerrouStatutProgression)
                {
                    _tempsPauseMillisecondes = Math.Max(0, value);
                }
            }
        }

        #endregion

        #region Méthodes

        #endregion

        #region Méthodes de réceptions d'évennements

        void _tacheProgressionPause_StatusProgressionModifiee(object sender, StatutProgressionEventArgs e)
        {
            TacheProgressionMinutee tacheProgressionMinutee = sender as TacheProgressionMinutee;
            if (tacheProgressionMinutee == null)
                return;

            lock (VerrouTacheProgression)
            {
                lock (VerrouStatutProgression)
                {
                    ProgressionPauseAvecStatusProgressionModifiee(tacheProgressionMinutee, e);
                }
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Ajoute la tache à la liste de l'agrégateur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        /// <returns></returns>
        protected override StatutProgressionEventArgs[] AjouterTacheProgressionCore(ITacheProgression tacheProgression)
        {
            List<StatutProgressionEventArgs> events = new List<StatutProgressionEventArgs> { };

            if (_tacheProgressionPause != null)
            {
                lock (_tacheProgressionPause.VerrouTacheProgression)
                {
                    if (_tacheProgressionPause.EstArretEnCours)
                        RetirerEtDesinscrireTacheProgressionPause();
                }
            }

            if (_tacheProgressionPause == null)
            {
                StatutProgressionEventArgs[] eventInitialiserEtInscrireTacheProgressionPauses = InitialiserEtInscrireTacheProgressionPause();
                events.AddRange(eventInitialiserEtInscrireTacheProgressionPauses);
            }

            StatutProgressionEventArgs[] eventAjoutTacheProgressions = base.AjouterTacheProgressionCore(tacheProgression);
            events.AddRange(eventAjoutTacheProgressions);

            return events.ToArray();
        }

        /// <summary>
        /// Gère la fin d'une tâche de l'agrégateur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        /// <param name="argumentStatutProgression"></param>
        /// <returns></returns>
        protected override StatutProgressionEventArgs[] TacheTerminee(ITacheProgression tacheProgression, ArgumentStatutProgression argumentStatutProgression)
        {
            bool estArretTacheMinutee = _tacheProgressionPauses.Contains(tacheProgression);
            if (false == estArretTacheMinutee)
            {
                // observation de la fin d'une tâche (non minuté)

                // toutes les tâches non minutée
                bool toutesLesTachesTerminees = TacheProgressionEnCours
                    .Where(tp => false == _tacheProgressionPauses.Contains(tp))
                    .All(tp => tp.StatutProgression.EtatProgression == EnumEtatProgression.Terminee);
                if (toutesLesTachesTerminees && _tacheProgressionPause != null)
                {
                    Debug.Assert(_tacheProgressionPause != null);
                    lock (_tacheProgressionPause.VerrouTacheProgression)
                    {
                        Debug.Assert(_tacheProgressionPause.EstArretEnCours == false);
                        _tacheProgressionPause.TerminerAvecPause(_tempsPauseMillisecondes);
                    }
                }
            }

            StatutProgressionEventArgs[] events = base.TacheTerminee(tacheProgression, argumentStatutProgression);

            return events;
        }

        /// <summary>
        /// Progressions the pause avec status progression modifiee.
        /// </summary>
        /// <param name="tacheProgressionMinutee">The tache progression minutee.</param>
        /// <param name="e">The <see cref="StatutProgressionEventArgs"/> instance containing the event data.</param>
        private void ProgressionPauseAvecStatusProgressionModifiee(TacheProgressionMinutee tacheProgressionMinutee, StatutProgressionEventArgs e)
        {
            if ((tacheProgressionMinutee == _tacheProgressionPause)
                && (e.StatutTacheProgression == EnumStatutTacheProgression.Arret))
            {
                RetirerEtDesinscrireTacheProgressionPause();
            }
        }

        private StatutProgressionEventArgs[] InitialiserEtInscrireTacheProgressionPause()
        {
            StatutProgressionEventArgs[] events;

            Debug.Assert(_tacheProgressionPause == null);

            _tacheProgressionPause = new TacheProgressionMinutee();
            _tacheProgressionPauses.Add(_tacheProgressionPause);
            _tacheProgressionPause.StatusProgressionModifiee += new EventHandler<StatutProgressionEventArgs>(_tacheProgressionPause_StatusProgressionModifiee);
            events = AjouterTacheProgressionCore(_tacheProgressionPause);
            _tacheProgressionPause.Demarrer();

            return events;
        }

        private void RetirerEtDesinscrireTacheProgressionPause()
        {
            Debug.Assert(_tacheProgressionPause != null);
            Debug.Assert(_tacheProgressionPause.EstArretEnCours);

            _tacheProgressionPause.StatusProgressionModifiee -= new EventHandler<StatutProgressionEventArgs>(_tacheProgressionPause_StatusProgressionModifiee);
            _tacheProgressionPause = null;
        }

        #endregion

    }
}
