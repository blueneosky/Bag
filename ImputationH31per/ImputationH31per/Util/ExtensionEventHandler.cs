using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ImputationH31per.Util
{
    public static class ExtensionEventHandler
    {
        public static void Notifier(this EventHandler gestionnaire, object sender, EventArgs e)
        {
            if (gestionnaire != null)
                gestionnaire.Invoke(sender, e);
        }

        public static void Notifier<TEventArgs>(this EventHandler<TEventArgs> gestionnaire, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (gestionnaire != null)
                gestionnaire(sender, e);
        }

        public static void Notifier(this PropertyChangedEventHandler gestionnaire, object sender, PropertyChangedEventArgs e)
        {
            if (gestionnaire != null)
                gestionnaire(sender, e);
        }

        public static void Notifier(this NotifyCollectionChangedEventHandler gestionnaire, object sender, NotifyCollectionChangedEventArgs e)
        {
            if (gestionnaire != null)
                gestionnaire(sender, e);
        }
    }
}