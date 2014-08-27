using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Permet de rediriger une progression vers une des progressions de sortie. (démultiplexeur)
    /// Ceci permet de basculer entre plusieurs affichage de progression à partir d'une source (ex : GestionnaireTacheProgression).
    /// Les tâches de sortie sont proprement arrêtées lors d'un basculement d'une tâche à une autre (arrêt avec basculement).
    /// 
    /// Des verroux permettent l'utilisation dans un contexte multi-threads.
    /// </summary>
    public class SelecteurTacheProgression
    {

        #region Membres

        private ITacheProgression _tacheProgressionEntree;

        private Dictionary<string, TacheProgression> _tacheProgressionSortiesParNoms;
        private TacheProgression _tacheProgressionSortieCourante;
        private string _nomTacheProgressionSortieCourante;

        private object _verrouSelecteurProgression = new object();

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="tacheProgressionEntree"></param>
        public SelecteurTacheProgression(ITacheProgression tacheProgressionEntree)
        {
            _tacheProgressionEntree = tacheProgressionEntree;
            _tacheProgressionEntree.StatusProgressionModifiee += new EventHandler<StatutProgressionEventArgs>(_tacheProgressionEntree_StatusProgressionModifiee);

            _tacheProgressionSortiesParNoms = new Dictionary<string, TacheProgression> { };
        }

        #endregion

        #region Propriétes

        /// <summary>
        /// Obtient ou définit la tâche de progression courante de sortie.
        /// </summary>
        public string NomTacheProgressionSortieCourante
        {
            get
            {
                string resultat;
                lock (_verrouSelecteurProgression)
                {
                    resultat = _nomTacheProgressionSortieCourante;
                }
                return resultat;
            }
            set
            {
                lock (_verrouSelecteurProgression)
                {
                    DefinirNomTacheProgressionSortieCourante(value);
                }
            }
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Créer et obtient une nouvelle tâche de progression de sortie et l'associe au nom.
        /// </summary>
        /// <param name="nomTacheProgressionSortie"></param>
        /// <returns></returns>
        public ITacheProgression CreerTacheProgressionSortie(string nomTacheProgressionSortie)
        {
            ITacheProgression tacheProgression;
            lock (_verrouSelecteurProgression)
            {
                tacheProgression = CreerTacheProgressionSortieCore(nomTacheProgressionSortie);
            }
            return tacheProgression;
        }

        #endregion

        #region Méthodes de réceptions d'évennements

        void _tacheProgressionEntree_StatusProgressionModifiee(object sender, StatutProgressionEventArgs e)
        {
            ITacheProgression tacheProgression = sender as ITacheProgression;
            if (tacheProgression == null)
                return;

            lock (_verrouSelecteurProgression)
            {
                TacheProgressionEntreeAvecStatusProgressionModifiee(e);
            }
        }

        #endregion

        #region Méthodes privés

        private ITacheProgression CreerTacheProgressionSortieCore(string nomTacheProgressionSortie)
        {
            Debug.Assert(!String.IsNullOrEmpty(nomTacheProgressionSortie));

            TacheProgression tacheProgression;
            if (_tacheProgressionSortiesParNoms.TryGetValue(nomTacheProgressionSortie, out tacheProgression))
                return tacheProgression;

            tacheProgression = new TacheProgression();
            _tacheProgressionSortiesParNoms[nomTacheProgressionSortie] = tacheProgression;

            return tacheProgression;
        }

        private void DefinirNomTacheProgressionSortieCourante(string nomTacheProgressionSortie)
        {
            if ((nomTacheProgressionSortie != null) && (false == _tacheProgressionSortiesParNoms.ContainsKey(nomTacheProgressionSortie)))
            {
                Debug.Fail("Nom de tâche invalide - tâche courante dessélectionnée");
                nomTacheProgressionSortie = null;
            }

            if (String.Equals(nomTacheProgressionSortie, _nomTacheProgressionSortieCourante))
                return; // rien a mettre à jour

            lock (_tacheProgressionEntree.VerrouTacheProgression)
            {
                if (_tacheProgressionSortieCourante != null)
                {
                    // Dessélection de la tâche courante

                    // arret si nécessaire de l'ancienne tâche de sortie
                    ArgumentStatutProgression status = _tacheProgressionSortieCourante.StatutProgression;
                    switch (status.EtatProgression)
                    {
                        case EnumEtatProgression.Initialisee:
                        case EnumEtatProgression.Terminee:
                            // rien à faire
                            break;
                        case EnumEtatProgression.ProgressionEnCours:
                        case EnumEtatProgression.ProgressionIndeterminee:
                            _tacheProgressionSortieCourante.Terminer();
                            break;
                        case EnumEtatProgression.Aucun:
                        default:
                            Debug.Fail("Cas non prévus");
                            break;
                    }

                    _tacheProgressionSortieCourante = null;
                }

                if (nomTacheProgressionSortie != null)
                {
                    // Sélection de la tâche suivante
                    _tacheProgressionSortieCourante = _tacheProgressionSortiesParNoms[nomTacheProgressionSortie];
                    Debug.Assert(_tacheProgressionSortieCourante.StatutProgression.EtatProgression == EnumEtatProgression.Initialisee
                        || _tacheProgressionSortieCourante.StatutProgression.EtatProgression == EnumEtatProgression.Terminee);

                    ArgumentStatutProgression status = _tacheProgressionEntree.StatutProgression;

                    // mise à jour de la nouvelle tâche par rapport à la tâche d'entrée
                    switch (status.EtatProgression)
                    {
                        case EnumEtatProgression.Initialisee:
                        case EnumEtatProgression.Terminee:
                            // rien à faire
                            break;
                        case EnumEtatProgression.ProgressionEnCours:
                        case EnumEtatProgression.ProgressionIndeterminee:
                            _tacheProgressionSortieCourante.Demarrer(status.ObtenirProgression());
                            break;
                        case EnumEtatProgression.Aucun:
                        default:
                            break;
                    }
                }

                _nomTacheProgressionSortieCourante = nomTacheProgressionSortie;
            }
        }

        private void TacheProgressionEntreeAvecStatusProgressionModifiee(StatutProgressionEventArgs e)
        {
            if (_tacheProgressionSortieCourante == null)
                return; // pas de retransmition d'events

            switch (e.StatutTacheProgression)
            {
                case EnumStatutTacheProgression.Demarrage:
                    _tacheProgressionSortieCourante.Demarrer(e.StatutProgression.ObtenirProgression());
                    break;
                case EnumStatutTacheProgression.EnCours:
                    _tacheProgressionSortieCourante.Modifier(e.StatutProgression.ObtenirProgression());
                    break;
                case EnumStatutTacheProgression.Arret:
                    _tacheProgressionSortieCourante.Terminer();
                    break;
                case EnumStatutTacheProgression.Aucun:
                default:
                    Debug.Fail("Cas non prévus");
                    break;
            }
        }

        #endregion
    }
}
