using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loop.Model
{
    public class NumberCell : ICell
    {
        #region Members

        private int _value;

        #endregion Members

        #region ctor

        public NumberCell(int value)
        {
            _value = value;
        }

        #endregion ctor

        #region ICell Membres

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion ICell Membres
    }
}