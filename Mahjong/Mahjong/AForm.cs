using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mahjong
{
    public class AForm : Form
    {
        #region Update

        private int _updateCounter = 0;

        protected bool IsUpdating { get { return _updateCounter > 0; } }

        protected virtual void BeginUpdate()
        {
            _updateCounter++;
            Debug.Assert(_updateCounter < 20);
        }

        protected virtual void EndUpdate()
        {
            _updateCounter--;
            Debug.Assert(_updateCounter >= 0);
        }

        #endregion Update

        #region Sync

        protected void EnsureSync(Action action)
        {
            if (InvokeRequired)
            {
                BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        #endregion
    }
}