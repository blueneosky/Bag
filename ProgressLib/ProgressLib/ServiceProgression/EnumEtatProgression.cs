using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Ressence les différents états de la tâche de progression.
    /// </summary>
    public enum EnumEtatProgression
    {
        /// <summary>
        /// Aucun (invalide)
        /// </summary>
        Aucun,

        /// <summary>
        /// Avant démarrage.
        /// </summary>
        Initialisee,
        /// <summary>
        /// En cours de progreession.
        /// </summary>
        ProgressionEnCours,
        /// <summary>
        /// Démarré mais sans borne min/max ou de progression.
        /// </summary>
        ProgressionIndeterminee,
        /// <summary>
        /// Progression terminée.
        /// </summary>
        Terminee,

    }
}
