using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPTimeline.Model
{
    public interface ICalendar
    {
        IDateTime Origin { get; set; }

        IDateTimeComponentInfo[] DateTimeComponentInfo { get; set; }
    }
}
