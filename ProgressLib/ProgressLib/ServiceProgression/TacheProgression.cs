using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Tâche de progression utilisable pour représenter une progression.
    /// Demarrer(...), Modifier(...) et Terminer() pour controler l'état de la progression.
    /// 
    /// Des verroux permettent l'utilisation dans un contexte multi-threads.
    /// </summary>
    public class TacheProgression : TacheProgressionBase, ITacheProgression, IDisposable
    {

        #region Membres privées

        private int _minimum;
        private int? _maximum;
        private int _progression;
        private string _texteProgression;
        private EnumEtatProgression _etatProgression;

        private object _verrouStatutProgression = new object();
        private object _verrouTacheProgression = new object();

        #endregion

        #region Constructeur

        /// <summary>
        /// Contructeur.
        /// </summary>
        public TacheProgression()
        {
            _etatProgression = EnumEtatProgression.Initialisee;
        }

        #endregion

        #region ITacheProgression

        /// <summary>
        /// Minimum.
        /// </summary>
        public override ArgumentStatutProgression StatutProgression
        {
            get
            {
                ArgumentStatutProgression resultat;
                lock (_verrouStatutProgression)
                {
                    resultat = new ArgumentStatutProgression(
                        _minimum
                        , _maximum
                        , _progression
                        , _etatProgression
                        , _texteProgression
                    );
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

        #region Méthodes public

        /// <summary>
        /// Démarrer la progression.
        /// </summary>
        public void Demarrer()
        {
            Demarrer(new ArgumentProgression());
        }

        /// <summary>
        /// Démarrer la progression.
        /// </summary>
        /// <param name="argumentProgression"></param>
        public void Demarrer(ArgumentProgression argumentProgression)
        {
            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = DemarrerCore(argumentProgression);
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        /// <summary>
        /// Terminer la tâche.
        /// La tâche est automatique amené à 100% (progression est définit à la valeur Maximum)
        /// </summary>
        public void Terminer()
        {
            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = TerminerCore();
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        /// <summary>
        /// Modife la progression en cours.
        /// </summary>
        /// <param name="argumentProgression"></param>
        public void Modifier(ArgumentProgression argumentProgression)
        {
            lock (_verrouTacheProgression)
            {
                StatutProgressionEventArgs[] events;
                lock (_verrouStatutProgression)
                {
                    events = ModifierCore(argumentProgression);
                }
                NotifierStatusProgressionModifiee(events);
            }
        }

        #endregion

        #region Méthodes privées

        private StatutProgressionEventArgs[] DemarrerCore(ArgumentProgression argumentProgression)
        {
            if (_etatProgression == EnumEtatProgression.ProgressionEnCours || _etatProgression == EnumEtatProgression.ProgressionIndeterminee)
            {
                Debug.Fail("La tâche de progression est déjà démarrée.");
                return new StatutProgressionEventArgs[] { };
            }

            int minimum = argumentProgression.Minimum;
            int? maximum = argumentProgression.Maximum;
            if (maximum.HasValue)
            {
                _minimum = Math.Min(minimum, maximum.Value);
                _maximum = Math.Max(minimum, maximum.Value);
            }
            else
            {
                _minimum = minimum;
                _maximum = maximum;
            }

            int progression = argumentProgression.Progression;
            _progression = progression;
            _progression = Math.Max(_progression, _minimum);
            if (_maximum.HasValue)
                _progression = Math.Min(_progression, _maximum.Value);

            _etatProgression = _maximum.HasValue ? EnumEtatProgression.ProgressionEnCours : EnumEtatProgression.ProgressionIndeterminee;

            _texteProgression = argumentProgression.TexteProgression;

            StatutProgressionEventArgs e = new StatutProgressionEventArgs(StatutProgression, EnumStatutTacheProgression.Demarrage);
            return new[] { e };

        }

        private StatutProgressionEventArgs[] TerminerCore()
        {
            List<StatutProgressionEventArgs> events = new List<StatutProgressionEventArgs> { };

            ArgumentProgression argumentProgression = StatutProgression.ObtenirProgression();
            argumentProgression.Maximum = argumentProgression.Maximum ?? 1;
            Debug.Assert(argumentProgression.Maximum.HasValue);
            argumentProgression.Progression = argumentProgression.Maximum.Value;
            StatutProgressionEventArgs[] modifieEvents = ModifierCore(argumentProgression);
            events.AddRange(modifieEvents);
            Debug.Assert(_etatProgression == EnumEtatProgression.ProgressionEnCours);

            _etatProgression = EnumEtatProgression.Terminee;

            StatutProgressionEventArgs e = new StatutProgressionEventArgs(StatutProgression, EnumStatutTacheProgression.Arret);
            events.Add(e);

            return events.ToArray();
        }

        private StatutProgressionEventArgs[] ModifierCore(ArgumentProgression argumentProgression)
        {
            StatutProgressionEventArgs[] events = new StatutProgressionEventArgs[] { };
            bool estMiseAJourStatusProgression = false;

            // pas de modification d'une tâche de progression si celle-ci n'est pas démarrée
            if ((_etatProgression != EnumEtatProgression.ProgressionEnCours) && (_etatProgression != EnumEtatProgression.ProgressionIndeterminee))
                return events;

            if (_minimum != argumentProgression.Minimum)
            {
                _minimum = argumentProgression.Minimum;
                estMiseAJourStatusProgression = true;
            }

            if (_maximum != argumentProgression.Maximum)
            {
                _maximum = argumentProgression.Maximum;
                estMiseAJourStatusProgression = true;
            }

            if (_progression != argumentProgression.Progression)
            {
                _progression = argumentProgression.Progression;
                estMiseAJourStatusProgression = true;
            }

            if (false == String.Equals(_texteProgression, argumentProgression.TexteProgression))
            {
                _texteProgression = argumentProgression.TexteProgression;
                estMiseAJourStatusProgression = true;
            }

            if (estMiseAJourStatusProgression)
            {
                bool avecValeurMaximum = _maximum.HasValue;

                if (avecValeurMaximum && _minimum > _maximum)
                {
                    _maximum = _minimum;
                }

                if (_progression < _minimum)
                {
                    _progression = _minimum;
                }
                if (avecValeurMaximum && _progression > _maximum)
                {
                    _progression = _maximum.Value;
                }

                if (_etatProgression == EnumEtatProgression.ProgressionEnCours)
                {
                    if (false == avecValeurMaximum)
                    {
                        _etatProgression = EnumEtatProgression.ProgressionIndeterminee;
                    }
                }
                else if (_etatProgression == EnumEtatProgression.ProgressionIndeterminee)
                {
                    if (avecValeurMaximum)
                    {
                        _etatProgression = EnumEtatProgression.ProgressionEnCours;
                    }
                }

                StatutProgressionEventArgs e = new StatutProgressionEventArgs(StatutProgression, EnumStatutTacheProgression.EnCours);
                events = new[] { e };
            }

            return events;
        }

        #endregion

        #region IDisposable

        // Track whether Dispose has been called.
        private bool disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    lock (_verrouTacheProgression)
                    {
                        StatutProgressionEventArgs[] events = new StatutProgressionEventArgs[] { };
                        lock (_verrouStatutProgression)
                        {
                            ArgumentStatutProgression statutProgression = StatutProgression;
                            EnumEtatProgression etatProgression = statutProgression.EtatProgression;
                            if (etatProgression == EnumEtatProgression.Initialisee)
                            {
                                // démarrer avant de terminer la progression
                                StatutProgressionEventArgs[] eventDemarrers = DemarrerCore(new ArgumentProgression());
                                events = events.Concat(eventDemarrers).ToArray();
                            }
                            if ((etatProgression == EnumEtatProgression.Initialisee)
                                || (etatProgression == EnumEtatProgression.ProgressionEnCours)
                                || (etatProgression == EnumEtatProgression.ProgressionIndeterminee))
                            {
                                // terminer la progression
                                StatutProgressionEventArgs[] eventTerminers = TerminerCore();
                                events = events.Concat(eventTerminers).ToArray();
                            }
                        }
                        NotifierStatusProgressionModifiee(events);
                    }
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                // ... no unanaged ressources

                // Note disposing has been done.
                disposed = true;

            }
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~TacheProgression()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
        #endregion
    }
}
