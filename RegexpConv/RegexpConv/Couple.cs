using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexpConv
{
    [Serializable]
    public class Couple
    {
        public Couple()
        {
        }

        public Couple(string item1, string item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public string Item1 { get; set; }

        public string Item2 { get; set; }
    }
}