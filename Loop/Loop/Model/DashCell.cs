using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loop.Model
{
    public class DashCell : ICell
    {
        #region Members

        private EnumDash _dash;

        #endregion Members

        #region ctor

        public DashCell(EnumDash dash)
        {
            _dash = dash;
        }

        #endregion ctor

        #region ICell Membres

        public int? Value
        {
            get { return (int)_dash; }
        }

        #endregion ICell Membres
    }
}