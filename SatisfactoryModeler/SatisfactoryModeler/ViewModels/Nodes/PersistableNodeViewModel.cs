using NodeNetwork.ViewModels;
using SatisfactoryModeler.Models;
using SatisfactoryModeler.Persistance.Networks;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public abstract class PersistableNodeViewModel<TBaseNode> : NodeViewModel, IPersistable<TBaseNode>
        where TBaseNode : BaseNode
    {
        public Guid Id { get; }

        protected PersistableNodeViewModel(TBaseNode source)
        {
            Id = source?.Id ?? Guid.NewGuid();

            if (source == null) return;
            this.Position = source.Position;
            this.IsCollapsed = source.IsCollapsed;
        }

        object IPersistable.Persist(object instance)
            => Persist((TBaseNode)instance);

        public virtual TBaseNode Persist(TBaseNode baseNode)
        {
            baseNode = baseNode ?? Activator.CreateInstance<TBaseNode>();

            baseNode.Id = this.Id;
            baseNode.Position = this.Position;
            baseNode.IsCollapsed = this.IsCollapsed;

            baseNode.Inputs = this.Inputs.Items.OfType<IPersistable>().Persist<InputPort>().ToArray();
            baseNode.Outputs = this.Outputs.Items.OfType<IPersistable>().Persist<OutputPort>().ToArray();

            return baseNode;
        }

        protected static PersistableValueNodeInputViewModel<T> CreateInput<T>(string portName, BaseNode parentSource, NodeEndpointEditorViewModel editor)
        {
            var port = parentSource?.Inputs.FirstOrDefault(ip => ip.Name == portName);
            return new PersistableValueNodeInputViewModel<T>(portName, port, editor);
        }

        protected static PersistableValueNodeOutputViewModel<T> CreateOutput<T>(string portName, BaseNode parentSource, NodeEndpointEditorViewModel editor)
        {
            var port = parentSource?.Outputs.FirstOrDefault(ip => ip.Name == portName);
            return new PersistableValueNodeOutputViewModel<T>(portName, port, editor);
        }

        protected static void SetupDynamicOutput<TOutput>(PersistableValueNodeOutputViewModel<TOutput> outputViewModel,
            Func<TOutput, ItemType?> currentItemTypeExtractor,
            Func<TOutput, double?> currentItemRateExtractor,
            string format, string fallback)
        {
            outputViewModel.Value.Subscribe(v =>
            {
                var itemType = currentItemTypeExtractor(v);
                var itemRate = currentItemRateExtractor(v);
                outputViewModel.Name = itemRate.HasValue && itemRate.HasValue
                    ? string.Format(format, itemType, itemRate)
                    : fallback;
            });

        }
    }
}
