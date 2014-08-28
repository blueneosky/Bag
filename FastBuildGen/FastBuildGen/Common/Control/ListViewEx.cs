using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.UI;

namespace FastBuildGen.Common.Control
{
    internal class ListViewEx : ListView
    {
        public ListViewEx()
        {
            LoadGlobalDoubleBuffered();
        }

        #region DoubleBuffered

        public virtual bool DoubleBufferedEx
        {
            get { return base.DoubleBuffered && this.GetStyle(ControlStyles.UserPaint); }
            set
            {
                base.DoubleBuffered = value;
                this.SetStyle(ControlStyles.UserPaint, value);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            const int WM_PRINTCLIENT = 792;
            const int PRF_CLIENT = 4;

            if (DoubleBufferedEx)
            {
                Message m = new Message();
                m.HWnd = Handle;
                m.Msg = WM_PRINTCLIENT;
                m.WParam = e.Graphics.GetHdc();
                m.LParam = (IntPtr)PRF_CLIENT;
                DefWndProc(ref m);
                e.Graphics.ReleaseHdc(m.WParam);
            }
            base.OnPaint(e);
        }

        private void LoadGlobalDoubleBuffered()
        {
            DoubleBufferedEx = UIDoubleBufferedModeManager.GlobalDoubleBufferedEx;
            DoubleBuffered = UIDoubleBufferedModeManager.GlobalDoubleBuffered;
        }

        #endregion DoubleBuffered

        protected override void WndProc(ref Message m)
        {
            _wndProcCounter++;
            base.WndProc(ref m);
            _wndProcCounter--;

            if (_wndProcCounter == 0)
            {
                if (_isGlobalSelectionChanged)
                {
                    _isGlobalSelectionChanged = false;
                    OnGlobalSelectionChanged(EventArgs.Empty);
                }
            }
        }

        #region Global Selection

        private bool _isGlobalSelectionChanged = false;

        private int _wndProcCounter = 0;

        public event EventHandler GlobalSelectionChanged;

        protected virtual void OnGlobalSelectionChanged(EventArgs e)
        {
            GlobalSelectionChanged.Notify(this, e);
        }

        protected override void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
        {
            _isGlobalSelectionChanged = true;
            base.OnItemSelectionChanged(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            _isGlobalSelectionChanged = true;
            base.OnSelectedIndexChanged(e);
        }

        #endregion Global Selection
    }
}