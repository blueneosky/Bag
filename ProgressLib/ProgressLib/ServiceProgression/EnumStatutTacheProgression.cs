using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Statut utilisé par les évennement de ITacheProgression .
    /// </summary>
    public enum EnumStatutTacheProgression
    {
        /// <summary>
        /// Valeur invalide
        /// </summary>
        Aucun = 0,

        /// <summary>
        /// La tâche démarre
        /// </summary>
        Demarrage,

        /// <summary>
        /// La tâche est en cours de progression
        /// </summary>
        EnCours,

        /// <summary>
        /// La tâche s'arrète.
        /// </summary>
        Arret,

    }
}
