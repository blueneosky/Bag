using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using SatisfactoryModeler.Persistance.Networks;
using System;
using System.Diagnostics;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class PersistableValueNodeInputViewModel<T> : ValueNodeInputViewModel<T>, IPersistablePortViewModel<InputPort>
    {
        static PersistableValueNodeInputViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeInputView(), typeof(IViewFor<PersistableValueNodeInputViewModel<T>>));
            //PersistableViewModelFactory.Instance.Register<InputPort, ???>
        }

        public Guid Id { get; }
        public string PortName { get; }

        IPersistableNodeViewModel IPersistablePortViewModel.Parent => (IPersistableNodeViewModel) this.Parent;

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

        object IPersistablePortViewModel.Persist() => this.Persist();

        public virtual InputPort Persist()
        {
            var withValue = !(Value is IObservable<T>);
            // TODO ALPHA persiste with convertion

            return new InputPort
            {
                Id = this.Id,
                ParentId = this.Parent.CastTo<IPersistableNodeViewModel>().Id,
                Name = this.PortName,
                WithValue = withValue,
                Value = withValue ? this.Value : default,
            };
        }
    }
}