using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    internal static class EventHandlerExtension
    {
        public static void Notify(this EventHandler manager, object sender, EventArgs e)
        {
            if (manager != null)
                manager(sender, e);
        }
    }
}