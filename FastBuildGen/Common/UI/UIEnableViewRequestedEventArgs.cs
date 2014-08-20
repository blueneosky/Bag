using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UI
{
    public class UIEnableViewRequestedEventArgs : EventArgs
    {
        private bool _canceled;

        private object _param;

        public UIEnableViewRequestedEventArgs()
            : this(null)
        {
        }

        public UIEnableViewRequestedEventArgs(object param)
            : this(false, param)
        {
        }

        private UIEnableViewRequestedEventArgs(bool canceld, object param)
        {
            Canceled = canceld;
            Param = param;
        }

        public bool Canceled
        {
            get { return _canceled; }
            set { _canceled = value; }
        }

        public object Param
        {
            get { return _param; }
            set { _param = value; }
        }
    }
}