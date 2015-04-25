using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System
{
    public static class EventHandlerExtension
    {
        public static void Notify(this EventHandler manager, object sender, EventArgs e)
        {
            if (manager != null)
                manager(sender, e);
        }

        public static void Notify(this PropertyChangedEventHandler manager, object sender, PropertyChangedEventArgs e)
        {
            if (manager != null)
                manager(sender, e);
        }
    }
}