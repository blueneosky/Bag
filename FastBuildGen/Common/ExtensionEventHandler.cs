using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common
{
    public static class ExtensionEventHandler
    {
        public static void Notify(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        public static void Notify<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        public static void Notify(this PropertyChangedEventHandler eventHandler, object sender, PropertyChangedEventArgs e)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        public static void Notify(this NotifyCollectionChangedEventHandler eventHandler, object sender, NotifyCollectionChangedEventArgs e)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }
    }
}