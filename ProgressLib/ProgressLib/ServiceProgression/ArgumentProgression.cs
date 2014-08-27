using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Représente une progression.
    /// </summary>
    public class ArgumentProgression
    {
        /// <summary>
        /// ConstanteDefautMinimum
        /// </summary>
        public const int ConstanteDefautMinimum = 0;

        /// <summary>
        /// ConstanteDefautMaximum
        /// </summary>
        public static int? ConstanteDefautMaximum = null;

        /// <summary>
        /// ConstanteDefautProgression
        /// </summary>
        public const int ConstanteDefautProgression = 0;

        /// <summary>
        /// ConstanteDefautTexteProgression
        /// </summary>
        public const string ConstanteDefautTexteProgression = null;

        /// <summary>
        /// Minimum.
        /// </summary>
        public int Minimum { get; set; }

        /// <summary>
        /// Maximum.
        /// </summary>
        public int? Maximum { get; set; }

        /// <summary>
        /// Progression.
        /// </summary>
        public int Progression { get; set; }

        /// <summary>
        /// Texte de la progression.
        /// </summary>
        public string TexteProgression { get; set; }
    }
}