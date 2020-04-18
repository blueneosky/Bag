using SatisfactoryModeler.Models;
using SatisfactoryModeler.Persistance.Networks;
using SatisfactoryModeler.ViewModels.Editors;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public abstract class ProducerNodeViewModel<TProducerNode> : PersistableNodeViewModel<TProducerNode>
        where TProducerNode : ProducerNode
    {
        public PersistableValueNodeInputViewModel<int?> Override { get; }

        public PersistableValueNodeOutputViewModel<ItemRate?> Output { get; }

        protected ProducerNodeViewModel(TProducerNode source) : base(source)
        {
            Override = CreateInput<int?>("Override", source, new IntegerValueEditorViewModel(100, 0, 250));
            Override.Name = "Override (%)";
            Override.Port.IsVisible = false;

            Output = CreateOutput<ItemRate?>("Output", source, null);
            Output.Name = "output";
        }
    }

}
