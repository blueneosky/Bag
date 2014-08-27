using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using FastBuildGen.Common.UI;

namespace FastBuildGen.Common.Control
{
    internal class BaseUserControl : UserControl
    {
        private const int WS_EX_COMPOSITED = 0x02000000;

        private int _updateCounter;

        public BaseUserControl()
        {
            LoadGlobalDoubleBuffered();
        }

        protected bool IsUpdating
        {
            get { return _updateCounter > 0; }
        }

        protected void BeginUpdate()
        {
            _updateCounter++;
        }

        protected override void Dispose(bool disposing)
        {
            PartialDispose(disposing);
            base.Dispose(disposing);
        }

        protected void EndUpdate()
        {
            _updateCounter--;
        }

        protected virtual void PartialDispose(bool disposing)
        {
        }

        protected void ValidationWithErrorProvider(Action action, System.Windows.Forms.Control control, ErrorProvider errorProvider, CancelEventArgs e, ErrorIconAlignment? errorIconAlignment = null)
        {
            try
            {
                action();
                if ((errorProvider != null) && (false == String.IsNullOrEmpty(errorProvider.GetError(control))))
                    errorProvider.SetError(control, null);   // supprimer l'ancienne erreur
            }
            catch (Exception exception)
            {
                if (errorIconAlignment.HasValue)
                    errorProvider.SetIconAlignment(control, errorIconAlignment.Value);
                if ((errorProvider != null))
                    errorProvider.SetError(control, exception.Message);
                if (e != null)
                    e.Cancel = true;
            }
        }

        #region DoubleBuffered

        public virtual bool DoubleBufferedEx
        {
            get { return DoubleBuffered && this.GetStyle(ControlStyles.UserPaint); }
            set
            {
                base.DoubleBuffered = value;
                this.SetStyle(ControlStyles.UserPaint, value);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (DoubleBufferedEx)
                    cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }

        protected override bool DoubleBuffered
        {
            get { return base.DoubleBuffered; }
            set
            {
                if (value)
                {
                    base.DoubleBuffered = true;
                }
                else
                {
                    DoubleBufferedEx = false;
                }
            }
        }

        private void LoadGlobalDoubleBuffered()
        {
            DoubleBufferedEx = UIDoubleBufferedModeManager.GlobalDoubleBufferedEx;
            DoubleBuffered = UIDoubleBufferedModeManager.GlobalDoubleBuffered;
        }

        #endregion DoubleBuffered
    }
}