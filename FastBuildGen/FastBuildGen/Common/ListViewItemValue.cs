using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FastBuildGen.Common
{
    internal class ListViewItemValue<TValue> : ListViewItem
    {
        private readonly TValue _value;

        public ListViewItemValue(TValue value)
        {
            _value = value;
            Text = ToString();
        }

        public TValue Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}