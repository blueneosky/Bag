using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Agrégateur de tâche de progression.
    /// Les tâches de progression sont dans un premier temps stocké dans une liste d'attente tant qu'elles n'ont pas démarrées.
    /// Les tâche qui démarre sont placées dans une deuxième liste et sont conservées dans celle-ci tant que toutes les tâches ne se sont pas arrêtés.
    /// Leur états sont agrégés afin de fournir une progression commune.
    /// A l'arrete de la denrière tahce de la liste des tâches en cours, toutes les tâches en cours sont relachées.
    /// 
    /// Des verroux permettent l'utilisation dans un contexte multi-threads.
    /// </summary>
    public class AgregateurTacheProgression : TacheProgressionBase, ITacheProgression
    {

        #region Membres

        private List<ITacheProgression> _tacheProgressionEnAttentes;
        private List<ITacheProgression> _tacheProgressionEnCours;

        private object _verrouStatutProgression = new object();
        private object _verrouTacheProgression = new object();

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public AgregateurTacheProgression()
        {
            _tacheProgressionEnAttentes = new List<ITacheProgression> { };
            _tacheProgressionEnCours = new List<ITacheProgression> { };
        }

        #endregion

        #region Propriete protégés

        /// <summary>
        /// Objet utilisé pour la synchronisation des status des tâches .
        /// </summary>
        protected object VerrouStatutProgression
        {
            get { return _verrouStatutProgression; }
        }

        /// <summary>
        /// Collection des tâches de progression en cours.
        /// </summary>
        protected IEnumerable<ITacheProgression> TacheProgressionEnCours
        {
            get { return _tacheProgressionEnCours; }
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Obtient le status global de la progression.
        /// </summary>
        public override ArgumentStatutProgression StatutProgression
        {
            get
            {
                ArgumentStatutProgression resultat;
                lock (_verrouStatutProgression)
                {
                    int minimumTotal = 0;
                    int? maximumTotal = 0;
                    int progressionTotal = 0;
                    List<string> texteProgressionTotals = new List<string> { };
                    int nombreTacheProgressionEnCours = 0;
                    int nombreTacheProgressionIndeterminee = 0;
                    int nombreTacheProgressionTerminee = 0;
                    int nombreTacheProgression = _tacheProgressionEnCours.Count;

                    foreach (ITacheProgression tacheProgression in _tacheProgressionEnCours)
                    {
                        ArgumentStatutProgression statutProgression = tacheProgression.StatutProgression;
                        int minimum = statutProgression.Minimum;
                        int? maximum = statutProgression.Maximum;
                        int progression = statutProgression.Progression;
                        string texteProgression = statutProgression.TexteProgression;

                        // Note : si une valeur est null dans une somme, le résultat est null

                        // mise en relatif à un minimum de 0
                        int delta = minimum;
                        minimum -= delta;
                        maximum -= delta;
                        progression -= delta;

                        // sommation
                        minimumTotal += minimum;
                        maximumTotal += maximum;
                        progressionTotal += progression;

                        switch (statutProgression.EtatProgression)
                        {
                            case EnumEtatProgression.ProgressionEnCours:
                                nombreTacheProgressionEnCours++;
                                break;
                            case EnumEtatProgression.ProgressionIndeterminee:
                                nombreTacheProgressionIndeterminee++;
                                break;
                            case EnumEtatProgression.Terminee:
                                nombreTacheProgressionTerminee++;
                                break;
                            case EnumEtatProgression.Aucun:
                            case EnumEtatProgression.Initialisee:
                            default:
                                Debug.Fail("Cas non prévus");
                                break;
                        }

                        if (false == String.IsNullOrEmpty(texteProgression))
                            texteProgressionTotals.Add(texteProgression);
                    }

                    if (_tacheProgressionEnCours.Any())
                    {
                        string texteProgressionTotal = null;
                        if (texteProgressionTotals.Any())
                            texteProgressionTotal = texteProgressionTotals.Aggregate((r, t) => String.Concat(r, Environment.NewLine, t));
                        EnumEtatProgression etatProgression = (nombreTacheProgressionIndeterminee == 0)
                                ? (nombreTacheProgressionEnCours == 0)
                                    ? EnumEtatProgression.Terminee
                                    : EnumEtatProgression.ProgressionEnCours
                                : EnumEtatProgression.ProgressionIndeterminee;

                        resultat = new ArgumentStatutProgression(
                            minimumTotal
                            , maximumTotal
                            , progressionTotal
                            , etatProgression
                            , texteProgressionTotal
                            );
                    }
                    else
                    {
                        resultat = new ArgumentStatutProgression(
                            ArgumentProgression.ConstanteDefautMinimum
                            , ArgumentProgression.ConstanteDefautMaximum
                            , ArgumentProgression.ConstanteDefautProgression
                            , EnumEtatProgression.Initialisee
                            , ArgumentProgression.ConstanteDefautTexteProgression
                            );
                    }
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

        #endregion

        #region Méthodes

        /// <summary>
        /// Ajoute une tâche de progression au pool de tâche.
        /// Celle-ci sera retirée lorsque le groupe de progression contenant cette tâche sera terminé.
        /// </summary>
        /// <param name="tacheProgression"></param>
        public void AjouterTacheProgression(ITacheProgression tacheProgression)
        {
            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = AjouterTacheProgressionCore(tacheProgression);
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        #endregion

        #region Méthodes de réceptions d'évennements

        void tacheProgression_StatusProgressionModifiee(object sender, StatutProgressionEventArgs e)
        {
            ITacheProgression tacheProgression = sender as ITacheProgression;
            if (tacheProgression == null)
                return;

            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = TacheProgressionAvecStatusProgressionModifiee(tacheProgression, e);
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        #endregion

        #region Méthode privées

        /// <summary>
        /// Ajoute la tache à la liste de l'agrégateur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        /// <returns></returns>
        protected virtual StatutProgressionEventArgs[] AjouterTacheProgressionCore(ITacheProgression tacheProgression)
        {
            StatutProgressionEventArgs[] events = new StatutProgressionEventArgs[] { };

            // ce verrou permet d'assurer qu'il n'y aura pas de changement intermédiaire durant la gestion de l'inscription
            // et de la mise en place de la tâche dans la liste de l'agrégateur
            lock (tacheProgression.VerrouTacheProgression)
            {
                ArgumentStatutProgression statutProgression = tacheProgression.StatutProgression;
                EnumEtatProgression etatProgression = statutProgression.EtatProgression;

                bool estTacheDejaInscrite = _tacheProgressionEnAttentes
                    .Any(tp => tp == tacheProgression);
                estTacheDejaInscrite = estTacheDejaInscrite || _tacheProgressionEnCours
                    .Any(tp => tp == tacheProgression);
                if (estTacheDejaInscrite)
                    return events;     // pss d'inscription


                // abonnement
                tacheProgression.StatusProgressionModifiee += new EventHandler<StatutProgressionEventArgs>(tacheProgression_StatusProgressionModifiee);


                // mise en tâche d'attente
                _tacheProgressionEnAttentes.Add(tacheProgression);


                if (etatProgression != EnumEtatProgression.Initialisee)
                {
                    ArgumentStatutProgression argument = statutProgression;
                    if (etatProgression == EnumEtatProgression.Terminee)
                    {
                        argument = new ArgumentStatutProgression(
                            statutProgression.Minimum
                            , statutProgression.Maximum
                            , statutProgression.Progression
                            , EnumEtatProgression.ProgressionEnCours // ne pas avoir EnumEtatProgression.Terminee pour un démarrage
                            , statutProgression.TexteProgression
                            );
                    }
                    StatutProgressionEventArgs[] eventsTacheDemarrees = TacheDemarree(tacheProgression, argument);
                    events = events.Concat(eventsTacheDemarrees).ToArray();
                }

                if (etatProgression == EnumEtatProgression.Terminee)
                {
                    StatutProgressionEventArgs[] eventsTacheDemarrees = TacheTerminee(tacheProgression, statutProgression);
                    events = events.Concat(eventsTacheDemarrees).ToArray();
                }
            }

            return events;
        }

        /// <summary>
        /// Gère le démarrage d'une tâche de l'agrégateur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        /// <param name="argumentStatutProgression"></param>
        /// <returns></returns>
        protected virtual StatutProgressionEventArgs[] TacheDemarree(ITacheProgression tacheProgression, ArgumentStatutProgression argumentStatutProgression)
        {
            Debug.Assert(_tacheProgressionEnAttentes.Any(tp => tp == tacheProgression));

            _tacheProgressionEnAttentes.Remove(tacheProgression);

            ArgumentStatutProgression statutProgressionSansNouvelleTache = StatutProgression;
            EnumEtatProgression etatProgressionSansNouvelleTache = statutProgressionSansNouvelleTache.EtatProgression;

            // état e la nouvelle tâche
            int minimumNouvelleTache = argumentStatutProgression.Minimum;
            int? maximumNouvelleTache = argumentStatutProgression.Maximum;
            int progressionNouvelleTache = argumentStatutProgression.Progression;
            EnumEtatProgression etatProgressionNouvelleTache = argumentStatutProgression.EtatProgression;
            Debug.Assert(etatProgressionNouvelleTache != EnumEtatProgression.Initialisee && etatProgressionNouvelleTache != EnumEtatProgression.Terminee);
            string texteProgressionNouvelleTache = argumentStatutProgression.TexteProgression;

            // mise en relatif
            int delta = minimumNouvelleTache;
            minimumNouvelleTache -= delta;
            maximumNouvelleTache -= delta;
            progressionNouvelleTache -= delta;

            // calcul du nouvele état
            int minimumAvecNouvelleTache = statutProgressionSansNouvelleTache.Minimum + minimumNouvelleTache;
            int? maximumAvecNouvelleTache = statutProgressionSansNouvelleTache.Maximum + maximumNouvelleTache;
            int progressionAvecNouvelleTache = statutProgressionSansNouvelleTache.Progression + progressionNouvelleTache;
            EnumEtatProgression etatProgressionAvecNouvelleTache;
            EnumStatutTacheProgression statutTacheProgression;
            switch (etatProgressionSansNouvelleTache)
            {
                case EnumEtatProgression.Initialisee:
                    statutTacheProgression = EnumStatutTacheProgression.Demarrage;
                    etatProgressionAvecNouvelleTache = etatProgressionNouvelleTache;
                    break;
                case EnumEtatProgression.ProgressionEnCours:
                case EnumEtatProgression.ProgressionIndeterminee:
                    statutTacheProgression = EnumStatutTacheProgression.EnCours;
                    etatProgressionAvecNouvelleTache = ((etatProgressionSansNouvelleTache == EnumEtatProgression.ProgressionEnCours)
                        && (etatProgressionNouvelleTache == EnumEtatProgression.ProgressionEnCours))
                        ? EnumEtatProgression.ProgressionEnCours
                        : EnumEtatProgression.ProgressionIndeterminee;
                    break;
                case EnumEtatProgression.Terminee:
                case EnumEtatProgression.Aucun:
                default:
                    Debug.Fail("Cas non gérés");
                    etatProgressionAvecNouvelleTache = EnumEtatProgression.Aucun;
                    statutTacheProgression = EnumStatutTacheProgression.Aucun;
                    break;
            }

            string texteProgressionSansNouvelleTache = statutProgressionSansNouvelleTache.TexteProgression;
            string texteProgressionAvecNouvelleTache = String.IsNullOrEmpty(texteProgressionNouvelleTache)
                ? texteProgressionSansNouvelleTache
                : String.IsNullOrEmpty(texteProgressionSansNouvelleTache)
                    ? texteProgressionNouvelleTache
                    : String.Concat(texteProgressionSansNouvelleTache, Environment.NewLine, texteProgressionNouvelleTache);

            ArgumentStatutProgression statutProgressionAvecNouvelleTache = new ArgumentStatutProgression(
                minimumAvecNouvelleTache
                , maximumAvecNouvelleTache
                , progressionAvecNouvelleTache
                , etatProgressionAvecNouvelleTache
                , texteProgressionAvecNouvelleTache
                );

            _tacheProgressionEnCours.Add(tacheProgression);

            StatutProgressionEventArgs e = new StatutProgressionEventArgs(statutProgressionAvecNouvelleTache, statutTacheProgression);
            StatutProgressionEventArgs[] events = new[] { e };

            return events;
        }

        /// <summary>
        /// Gère la mise à jour d'une tâche de l'agrégateur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        /// <param name="argumentStatutProgression"></param>
        /// <returns></returns>
        protected virtual StatutProgressionEventArgs[] MiseAJourStatusProgression(ITacheProgression tacheProgression, ArgumentStatutProgression argumentStatutProgression)
        {
            // nothing - for now
            StatutProgressionEventArgs e = new StatutProgressionEventArgs(StatutProgression, EnumStatutTacheProgression.EnCours);
            StatutProgressionEventArgs[] events = new[] { e };

            return events;
        }

        /// <summary>
        /// Gère la fin d'une tâche de l'agrégateur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        /// <param name="argumentStatutProgression"></param>
        /// <returns></returns>
        protected virtual StatutProgressionEventArgs[] TacheTerminee(ITacheProgression tacheProgression, ArgumentStatutProgression argumentStatutProgression)
        {
            StatutProgressionEventArgs[] events = new StatutProgressionEventArgs[] { };

            ArgumentStatutProgression statutProgression = StatutProgression;
            EnumStatutTacheProgression statutTacheProgression = statutProgression.EtatProgression == EnumEtatProgression.Terminee
                ? EnumStatutTacheProgression.Arret
                : EnumStatutTacheProgression.EnCours;
            StatutProgressionEventArgs e = new StatutProgressionEventArgs(statutProgression, statutTacheProgression);
            events = events.Concat(new[] { e }).ToArray();

            bool toutesLesTachesTerminees = _tacheProgressionEnCours.All(tp => tp.StatutProgression.EtatProgression == EnumEtatProgression.Terminee);
            if (toutesLesTachesTerminees)
            {
                foreach (ITacheProgression tp in _tacheProgressionEnCours)
                {
                    // désabonnement
                    tacheProgression.StatusProgressionModifiee -= new EventHandler<StatutProgressionEventArgs>(tacheProgression_StatusProgressionModifiee);
                }

                // retirer toutes les tâches
                _tacheProgressionEnCours.Clear();
            }

            return events;
        }

        /// <summary>
        /// Gère la modification d'état d'une tâche de l'agrégateur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual StatutProgressionEventArgs[] TacheProgressionAvecStatusProgressionModifiee(ITacheProgression tacheProgression, StatutProgressionEventArgs e)
        {
            StatutProgressionEventArgs[] events;
            switch (e.StatutTacheProgression)
            {
                case EnumStatutTacheProgression.Demarrage:
                    events = TacheDemarree(tacheProgression, e.StatutProgression);
                    break;
                case EnumStatutTacheProgression.EnCours:
                    events = MiseAJourStatusProgression(tacheProgression, e.StatutProgression);
                    break;
                case EnumStatutTacheProgression.Arret:
                    events = TacheTerminee(tacheProgression, e.StatutProgression);
                    break;

                case EnumStatutTacheProgression.Aucun:
                default:
                    Debug.Fail("Cas non gérés");
                    events = new StatutProgressionEventArgs[] { };
                    break;
            }

            return events;
        }

        #endregion

    }
}
