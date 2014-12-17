using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildLogAnalyse
{
    public class DataLogSet
    {

        private DateTime _start;

        public DateTime Start
        {
            get { return _start; }
            set { _start = value; }
        }

        private DateTime _stop;

        public DateTime Stop
        {
            get { return _stop; }
            set { _stop = value; }
        }

        private int _indiceSurJour;

        public int IndiceSurJour
        {
            get { return _indiceSurJour; }
            set { _indiceSurJour = value; }
        }

        //public DateTime Ellapsed { get { return Stop.Subtract(Start.TimeOfDay); } }
        public TimeSpan Ellapsed { get { return Stop.Subtract(Start); } }

    }
}
