using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Text;

namespace RPTimeline.Model
{
    public class DateTime : IDateTime
    {
        #region ctor

        public DateTime(ICalendar calendar)
        {
        }

        #endregion ctor

        #region IDateTime Members

        public ICalendar Calendar
        {
            get { throw new NotImplementedException(); }
        }

        private long?[] _components;

        public long?[] Components
        {
            get { return _components; }
            set
            {
                if (_components == value) return;
                _components = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPropertyChanged.IDateTime.Components));
            }
        }

        #endregion IDateTime Members

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var manager = PropertyChanged;
            if (manager != null)
                manager(sender, e);
        }

        #endregion INotifyPropertyChanged Members
    }
}