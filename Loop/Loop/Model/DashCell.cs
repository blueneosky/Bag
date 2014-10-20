using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loop.Model
{
    public class DashCell : ICell
    {
        #region Members

        private int _withDash;

        #endregion Members

        #region ctor

        public DashCell(int withDash)
        {
            _withDash = withDash;
        }

        #endregion ctor

        #region ICell Membres

        public int? Value
        {
            get { return _withDash; }
        }

        #endregion ICell Membres
    }
}