using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using SatisfactoryModeler.Persistance.Networks;
using System;
using System.Diagnostics;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class PersistableValueNodeInputViewModel<T> : ValueNodeInputViewModel<T>, IPersistablePort<InputPort>
    {
        static PersistableValueNodeInputViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeInputView(), typeof(IViewFor<PersistableValueNodeInputViewModel<T>>));
            //PersistableViewModelFactory.Instance.Register<InputPort, ???>
        }

        public Guid Id { get; }
        public string PortName { get; }

        IPersistable IPersistablePort.Parent => (IPersistable) this.Parent;

        public PersistableValueNodeInputViewModel(string portName, InputPort source, NodeEndpointEditorViewModel editor)
        {
            Debug.Assert(source == null || source.Name == portName);

            this.PortName = portName;
            this.Id = source?.Id ?? Guid.NewGuid();
            this.Editor = editor;

            if (source == null) return;
            if(source.WithValue)
                this.SetValue(source.Value);
        }

        object IPersistable.Persist(object port) => Persist((InputPort)port);

        public virtual InputPort Persist(InputPort port)
        {
            Debug.Assert(port == null);

            var withValue = !(Value is IObservable<T>);

            return new InputPort
            {
                Id = this.Id,
                ParentId = this.Parent.CastTo<IPersistable>().Id,
                Name = this.PortName,
                WithValue = withValue,
                Value = withValue ? this.Value : default,
            };
        }
    }
}