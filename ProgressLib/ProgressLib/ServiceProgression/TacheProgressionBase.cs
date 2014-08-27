using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Regroupe m'ensemble des fonctionnalité de base utilisé pour les tâches de progression.
    /// 
    /// Des verroux permettent l'utilisation dans un contexte multi-threads.
    /// </summary>
    public abstract class TacheProgressionBase : ITacheProgression
    {
        #region Membres

        private Guid _identifiantTacheProgression= Guid.NewGuid();

        #endregion

        #region Constructeur

        /// <summary>
        /// Contructeur.
        /// </summary>
        protected TacheProgressionBase()
        {
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Identifiant unique de tâche de progression.
        /// </summary>
        internal Guid IdentifiantTacheProgression
        {
            get { return _identifiantTacheProgression; }
        }

        #endregion

        #region ITacheProgression

        /// <summary>
        /// Obtient le status global de la progression.
        /// </summary>
        public abstract ArgumentStatutProgression StatutProgression { get; }

        /// <summary>
        /// L'état de la progression a été modifié.
        /// </summary>
        public event EventHandler<StatutProgressionEventArgs> StatusProgressionModifiee;

        /// <summary>
        /// Objet utilisé pour la synchronisation des tâches. A UTILISER AVEC PRECAUTION.
        /// </summary>
        public abstract object VerrouTacheProgression { get; }

        #endregion

        #region Notifications

        /// <summary>
        /// Appelée pour notifier StatusProgressionModifiee.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifierStatusProgressionModifiee(StatutProgressionEventArgs e)
        {
            NotifierEvennement(StatusProgressionModifiee, e);
        }

        private void NotifierEvennement<TEventArgs>(EventHandler<TEventArgs> gestionnaire, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (gestionnaire != null)
                gestionnaire(this, e);
        }

        /// <summary>
        /// Appelé pour notifier un lot de StatusProgressionModifiee.
        /// </summary>
        /// <param name="events"></param>
        protected virtual void NotifierStatusProgressionModifiee(StatutProgressionEventArgs[] events)
        {
            foreach (StatutProgressionEventArgs e in events)
            {
                if (e != null)
                    NotifierStatusProgressionModifiee(e);
            }
        }

        #endregion

        #region Ovverides

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Object.ReferenceEquals(this, obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return _identifiantTacheProgression.GetHashCode();
        }

        #endregion
    }
}
