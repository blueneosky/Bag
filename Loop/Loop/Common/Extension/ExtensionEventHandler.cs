using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Loop.Common.Extension
{
    public static class ExtensionEventHandler
    {
        public static void Notify(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
                handler.Invoke(sender, e);
        }

        public static void Notify<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (handler != null)
                handler(sender, e);
        }

        public static void Notify(this PropertyChangedEventHandler handler, object sender, PropertyChangedEventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }

        public static void Notifier(this NotifyCollectionChangedEventHandler handler, object sender, NotifyCollectionChangedEventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }
    }
}