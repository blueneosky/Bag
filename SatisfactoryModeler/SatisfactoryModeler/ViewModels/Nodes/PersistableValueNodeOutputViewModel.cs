using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using SatisfactoryModeler.Persistance.Networks;
using System;
using System.Diagnostics;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class PersistableValueNodeOutputViewModel<T> : ValueNodeOutputViewModel<T>, IPersistablePortViewModel<OutputPort>
    {
        static PersistableValueNodeOutputViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeOutputView(), typeof(IViewFor<PersistableValueNodeOutputViewModel<T>>));
            //PersistableViewModelFactory.Instance.Register<OutputPort, ???>
        }

        public Guid Id { get; }
        public string PortName { get; }

        IPersistableNodeViewModel IPersistablePortViewModel.Parent => (IPersistableNodeViewModel)this.Parent;

        public PersistableValueNodeOutputViewModel(string portName, OutputPort source, NodeEndpointEditorViewModel editor)
        {
            Debug.Assert(source == null || source.Name == portName);

            this.PortName = portName;
            this.Id = source?.Id ?? Guid.NewGuid();
            this.Editor = editor;

            if (source == null) return;
            if(source.WithValue)
                this.SetValue(source.Value);
        }

        object IPersistablePortViewModel.Persist() => Persist();
    
        public virtual OutputPort Persist()
        {
            var withValue = !(Value is IObservable<T>);

            return new OutputPort
            {
                Id = this.Id,
                ParentId = this.Parent.CastTo<IPersistableNodeViewModel>().Id,
                Name = this.PortName,
                WithValue = withValue,
                Value = withValue ? this.Value : null,
            };
        }
    }
}
