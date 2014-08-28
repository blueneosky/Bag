using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common
{
    public class ListBoxItem<TValue>
    {
        private readonly TValue _value;

        public ListBoxItem(TValue value)
        {
            _value = value;
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