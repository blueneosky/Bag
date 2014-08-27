using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Util
{
    public class WeakEventHandler<TOwner, TEventArgs, THandler>
        where TOwner : class
        where TEventArgs : EventArgs
    {
        // Fields
        private THandler _handler;

        private Action<WeakEventHandler<TOwner, TEventArgs, THandler>> onDetachAction;
        private Action<TOwner, object, TEventArgs> onEventAction;
        private WeakReference ownerReference;

        // Methods
        public WeakEventHandler(
            TOwner owner
            , Action<TOwner, object, TEventArgs> onEventActionStatic
            , Action<WeakEventHandler<TOwner, TEventArgs, THandler>> onDetachActionStatic
            , Func<WeakEventHandler<TOwner, TEventArgs, THandler>, THandler> createHandlerFunction)
        {
            this.ownerReference = new WeakReference(owner);
            this.onEventAction = onEventActionStatic;
            this.onDetachAction = onDetachActionStatic;
            this.Handler = createHandlerFunction(this);
        }

        // Properties
        public THandler Handler
        {
            get
            {
                return this._handler;
            }
            private set
            {
                this._handler = value;
            }
        }

        public void OnEvent(object source, TEventArgs eventArgs)
        {
            TOwner target = this.ownerReference.Target as TOwner;
            if (target != null)
            {
                this.onEventAction(target, source, eventArgs);
            }
            else
            {
                this.onDetachAction(this);
            }
        }
    }
}