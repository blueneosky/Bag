using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgressLib.ServiceProgression;

namespace ProgressTest
{
    class ProgressionManagerTestHelper : IDisposable
    {
        private Button _start;
        private Button _stop;
        private Button _reset;
        private TrackBar _trackBar;
        private CheckBox _checkbox;
        private TextBox _textBox;
        private ProgressTestHelper _progressTestHelper;

        private TacheProgression _tacheProgression;
        private ArgumentProgression _argumentProgression;
        private int maximum;


        public ProgressionManagerTestHelper(Button start, Button stop, Button reset, TrackBar trackBar, CheckBox checkBox, TextBox textBox, ProgressBar progressBar, Label labelProgression)
        {
            this._tacheProgression = new TacheProgression();

            this._start = start;
            this._stop = stop;
            this._reset = reset;
            this._trackBar = trackBar;
            this._checkbox = checkBox;
            this._textBox = textBox;
            this._progressTestHelper = new ProgressTestHelper(progressBar, labelProgression, false);

            _start.Enabled = true;
            _stop.Enabled = false;
            _reset.Enabled = false;
            _trackBar.Enabled = false;
            _checkbox.Enabled = false;
            _textBox.Enabled = true;

            _start.Click += new EventHandler(_start_Click);
            _stop.Click += new EventHandler(_stop_Click);
            _reset.Click += new EventHandler(_reset_Click);
            _trackBar.Scroll += new EventHandler(_trackBar_Scroll);
            _checkbox.CheckedChanged += new EventHandler(_checkbox_CheckedChanged);
            _textBox.TextChanged += new EventHandler(_textBox_TextChanged);

        }

        void _textBox_TextChanged(object sender, EventArgs e)
        {
            if (_argumentProgression == null || _tacheProgression == null)
                return;

            _argumentProgression.TexteProgression = _textBox.Text;
            _tacheProgression.Modifier(_argumentProgression);
        }

        void _checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkbox.Checked)
            {
                _argumentProgression.Maximum = null;
            }
            else
            {
                _argumentProgression.Maximum = maximum;
            }
            _tacheProgression.Modifier(_argumentProgression);
        }

        void _trackBar_Scroll(object sender, EventArgs e)
        {
            _argumentProgression.Progression = _trackBar.Value;
            _tacheProgression.Modifier(_argumentProgression);
        }

        void _reset_Click(object sender, EventArgs e)
        {
            _stop.Enabled = false;
            _reset.Enabled = false;
            _trackBar.Enabled = false;
            _checkbox.Enabled = false;
            _progressTestHelper.Reset();

            ArgumentStatutProgression statusProgression = _tacheProgression.StatutProgression;
            if (statusProgression.EtatProgression == EnumEtatProgression.ProgressionEnCours
                || statusProgression.EtatProgression == EnumEtatProgression.ProgressionIndeterminee)
            {
                _tacheProgression.Terminer();
            }

            _start.Enabled = true;

        }

        void _start_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int minimum = random.Next(100);
            maximum = random.Next(1, 200) + minimum;
            int progression = minimum;
            string texteProgression = _textBox.Text;

            _start.Enabled = false;
            _stop.Enabled = true;
            _reset.Enabled = true;
            _trackBar.Enabled = true;
            _checkbox.Enabled = true;

            _trackBar.Minimum = minimum;
            _trackBar.Maximum = maximum;
            _trackBar.Value = progression;

            CreerTacheProgression();

            _argumentProgression = new ArgumentProgression()
            {
                Minimum = minimum,
                Maximum = _checkbox.Checked ? (int?)null : (int?)maximum,
                Progression = progression,
                TexteProgression = texteProgression,
            };
            _tacheProgression.Demarrer(_argumentProgression);

        }

        void _stop_Click(object sender, EventArgs e)
        {
            _stop.Enabled = false;
            _trackBar.Enabled = false;
            _checkbox.Enabled = false;

            _tacheProgression.Terminer();
        }

        public void CreerTacheProgression()
        {
            _progressTestHelper.DetacherTacheProgression();

            _tacheProgression = new TacheProgression();

            _progressTestHelper.AttacherProgression(_tacheProgression);
            GestionnaireTacheProgression.Instance.InscrireTache(_tacheProgression);
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
                    _progressTestHelper.Dispose();
                    if (_tacheProgression != null)
                        _tacheProgression.Dispose();
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
        ~ProgressionManagerTestHelper()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion

    }
}
