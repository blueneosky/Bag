using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Argument d'évennement pour notifier la modification du statut de progression d'un ITacheProgression
    /// </summary>
    public class StatutProgressionEventArgs : EventArgs
    {

        #region Membres

        private ArgumentStatutProgression _statutProgression;
        private EnumStatutTacheProgression _statutTacheProgression;

        #endregion

        #region Construteur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="statutProgression"></param>
        /// <param name="statutTacheProgression"></param>
        public StatutProgressionEventArgs(ArgumentStatutProgression statutProgression, EnumStatutTacheProgression statutTacheProgression)
        {
            _statutProgression = statutProgression;
            _statutTacheProgression = statutTacheProgression;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Statut de la progression.
        /// </summary>
        public ArgumentStatutProgression StatutProgression
        {
            get { return _statutProgression; }
        }

        /// <summary>
        /// Statut de la tâche de progression.
        /// </summary>
        public EnumStatutTacheProgression StatutTacheProgression
        {
            get { return _statutTacheProgression; }
        }

        #endregion
    }
}
