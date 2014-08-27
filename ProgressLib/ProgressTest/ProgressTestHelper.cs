using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using ProgressLib.ServiceProgression;
using System.Diagnostics;

namespace ProgressTest
{
    class ProgressTestHelper : IDisposable
    {
        private ProgressBar _progressBar;
        private Label _labelProgression;
        private bool _estAvecMasquageImmediatApresProgression;

        private ITacheProgression _tacheProgression;

        public ProgressTestHelper(ProgressBar progressBar, Label labelProgression, bool estAvecMasquageImmediatApresProgression)
        {
            this._progressBar = progressBar;
            this._labelProgression = labelProgression;
            this._estAvecMasquageImmediatApresProgression = estAvecMasquageImmediatApresProgression;

            Action action = delegate
            {
                _progressBar.Visible = false;
                _labelProgression.Visible = false;
            };
            ModifierControle(_progressBar, action);
        }

        public void AttacherProgression(ITacheProgression tacheProgression)
        {
            DetacherTacheProgression();

            _tacheProgression = tacheProgression;
            _tacheProgression.StatusProgressionModifiee += new EventHandler<StatutProgressionEventArgs>(_tacheProgression_StatusProgressionModifiee);
        }

        public void DetacherTacheProgression()
        {
            if (_tacheProgression != null)
            {
                _tacheProgression.StatusProgressionModifiee -= new EventHandler<StatutProgressionEventArgs>(_tacheProgression_StatusProgressionModifiee);

                _tacheProgression = null;
            }

        }

        public void Reset()
        {
            Action action = delegate
            {
                _progressBar.Visible = false;
                _labelProgression.Visible = false;
            };
            ModifierControle(_progressBar, action);
        }

        void _tacheProgression_StatusProgressionModifiee(object sender, StatutProgressionEventArgs e)
        {
            Action action = null;
            switch (e.StatutTacheProgression)
            {
                case EnumStatutTacheProgression.Demarrage:
                    action = delegate
                    {
                        _progressBar.Visible = true;
                        _labelProgression.Visible = true;
                    };
                    break;
                case EnumStatutTacheProgression.EnCours:
                    // rien
                    break;
                case EnumStatutTacheProgression.Arret:
                    if (_estAvecMasquageImmediatApresProgression)
                    {
                        action = delegate
                        {
                            _progressBar.Visible = false;
                            _labelProgression.Visible = false;
                        };
                    }
                    break;

                case EnumStatutTacheProgression.Aucun:
                default:
                    Debug.Fail("Cas non géré");
                    break;
            }
            if (action != null)
                ModifierControle(_progressBar, action);

            ArgumentStatutProgression statutProgression = e.StatutProgression;

            switch (statutProgression.EtatProgression)
            {
                case EnumEtatProgression.ProgressionEnCours:
                case EnumEtatProgression.Terminee:
                    action = delegate { _progressBar.Style = ProgressBarStyle.Blocks; };
                    break;
                case EnumEtatProgression.ProgressionIndeterminee:
                    action = delegate { _progressBar.Style = ProgressBarStyle.Marquee; };
                    break;

                case EnumEtatProgression.Initialisee:
                case EnumEtatProgression.Aucun:
                default:
                    Debug.Fail("Cas non gérés");
                    break;
            }
            if (action != null)
                ModifierControle(_progressBar, action);

            if (statutProgression.EtatProgression != EnumEtatProgression.ProgressionIndeterminee)
            {
                action = delegate
                {
                    _progressBar.Minimum = statutProgression.Minimum;
                    _progressBar.Maximum = statutProgression.Maximum ?? 1;
                    _progressBar.Value = statutProgression.Progression;
                };
                ModifierControle(_progressBar, action);
            }

            action = delegate
            {
                if (String.Equals(_labelProgression.Text, statutProgression.TexteProgression))
                    return;
                _labelProgression.Text = statutProgression.TexteProgression;
            };
            ModifierControle(_labelProgression, action);
        }

        private void ModifierControle(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

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
                    DetacherTacheProgression();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                // rien

                // Note disposing has been done.
                disposed = true;

            }
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~ProgressTestHelper()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion

    }
}
