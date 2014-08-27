using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Représente le statut de progresison d'une ITacheProgression
    /// </summary>
    public class ArgumentStatutProgression
    {

        private int _minimum;
        private int? _maximum;
        private int _progression;
        private EnumEtatProgression _etatProgression;
        private string _texteProgression;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="progression"></param>
        /// <param name="etatProgression"></param>
        public ArgumentStatutProgression(int minimum, int? maximum, int progression, EnumEtatProgression etatProgression, string texteProgression)
        {
            _minimum = minimum;
            _maximum = maximum;
            _progression = progression;
            _etatProgression = etatProgression;
            _texteProgression = texteProgression;
        }

        /// <summary>
        /// Minimum.
        /// </summary>
        public int Minimum
        {
            get { return _minimum; }
        }

        /// <summary>
        /// Maximum.
        /// </summary>
        public int? Maximum
        {
            get { return _maximum; }
        }

        /// <summary>
        /// Progression.
        /// </summary>
        public int Progression
        {
            get { return _progression; }
        }

        /// <summary>
        /// Etat de la progression.
        /// </summary>
        public EnumEtatProgression EtatProgression
        {
            get { return _etatProgression; }
        }

        /// <summary>
        /// Texte de la progression.
        /// </summary>
        public string TexteProgression
        {
            get { return _texteProgression; }
        }

        /// <summary>
        /// Obtient l'ArgumentProgression issu des données.
        /// </summary>
        /// <returns></returns>
        public ArgumentProgression ObtenirProgression()
        {
            return new ArgumentProgression()
            {
                Minimum = Minimum,
                Maximum = Maximum,
                Progression = Progression,
                TexteProgression = TexteProgression,
            };
        }
    }
}
