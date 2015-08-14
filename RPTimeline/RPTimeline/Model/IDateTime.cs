using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RPTimeline.Model
{
    public interface IDateTime : INotifyPropertyChanged
    {
        ICalendar Calendar { get; }

        long?[] Components{get; set;}

        //string 
    }
}
