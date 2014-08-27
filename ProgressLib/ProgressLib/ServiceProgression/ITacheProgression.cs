using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Interface d'une tâche de progression.
    /// </summary>
    public interface ITacheProgression
    {

        /// <summary>
        /// Obtient le status global de la progression.
        /// </summary>
        ArgumentStatutProgression StatutProgression { get; }

        /// <summary>
        /// L'état de la progression a été modifié.
        /// </summary>
        event EventHandler<StatutProgressionEventArgs> StatusProgressionModifiee;

        /// <summary>
        /// Objet utilisé pour la synchronisation des tâches. A UTILISER AVEC PRECAUTION.
        /// </summary>
        object VerrouTacheProgression { get; }
    }
}
