using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FastBuildGen.Common.UI
{
    public static class UIDoubleBufferedModeManager
    {
        private static bool _globalDoubleBuffered;

        private static bool _globalDoubleBufferedEx;

        public static bool GlobalDoubleBuffered
        {
            get { return _globalDoubleBuffered; }
            set
            {
                if (Application.OpenForms.Count > 0)
                    throw new UIException("Must be done before Form shown.");
                if (value == _globalDoubleBuffered)
                    return;

                if (value)
                {
                    _globalDoubleBuffered = true;
                }
                else
                {
                    GlobalDoubleBufferedEx = false;
                }
            }
        }

        public static bool GlobalDoubleBufferedEx
        {
            get { return _globalDoubleBufferedEx; }
            set
            {
                if (Application.OpenForms.Count > 0)
                    throw new UIException("Must be done before Form shown.");
                if (value == _globalDoubleBufferedEx)
                    return;
                _globalDoubleBuffered = value;
                _globalDoubleBufferedEx = value;
            }
        }
    }
}