using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Gestionnaire permettant d'inscrire des tâches de progression ainsi que d'obtenir la progression courante.
    /// 
    /// Des verroux permettent l'utilisation dans un contexte multi-threads.
    /// </summary>
    public class GestionnaireTacheProgression
    {

        #region Singloton

        private static GestionnaireTacheProgression _instance;
        private static object _verrouInstance = new object();

        /// <summary>
        /// Instance
        /// </summary>
        public static GestionnaireTacheProgression Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_verrouInstance)
                    {
                        if (_instance == null)
                            _instance = new GestionnaireTacheProgression();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Membres

        private AgregateurTacheProgressionAvecPause _agregateurTacheProgression;

        #endregion

        #region Constructeur

        private GestionnaireTacheProgression()
        {
            _agregateurTacheProgression = new AgregateurTacheProgressionAvecPause();
        }

        #endregion

        #region Propriete

        /// <summary>
        /// Tache de progression géré par le gestionnaire.
        /// </summary>
        public ITacheProgression TacheProgression
        {
            get { return _agregateurTacheProgression; }
        }

        /// <summary>
        /// Temps de pause à la fin d'une progression.
        /// </summary>
        public int TempsPauseMillisecondes
        {
            get { return _agregateurTacheProgression.TempsPauseMillisecondes; }
            set { _agregateurTacheProgression.TempsPauseMillisecondes = value; }
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Inscrit une tâche auprès du gestionnaire.
        /// </summary>
        /// <param name="tacheProgression"></param>
        public void InscrireTache(ITacheProgression tacheProgression)
        {
            _agregateurTacheProgression.AjouterTacheProgression(tacheProgression);
        }

        /// <summary>
        /// Créer et inscrit une nouvelle tâche auprès du gestionnaire.
        /// Static pour des raison de facilité d'écriture.
        /// </summary>
        /// <returns></returns>
        public TacheProgression CreerEtInscrireTache()
        {
            TacheProgression tacheProgression = new TacheProgression();

            InscrireTache(tacheProgression);

            return tacheProgression;
        }

        #endregion
    }
}
