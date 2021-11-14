using DynamicData;
using NodeNetwork.ViewModels;
using SatisfactoryModeler.Persistance.Networks;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public static class PersistableExtensions
    {
        #region Nodes
        
        public static IEnumerable<TNode> Persist<TNode>(this IEnumerable<IPersistableNodeViewModel<TNode>> nodeViewModels)
            where TNode : BaseNode
            => nodeViewModels?.Select(n => n?.Persist());

        public static IEnumerable<BaseNode> Persist(this IEnumerable<NodeViewModel> nodeViewModels)
            => nodeViewModels?.OfType<IPersistableNodeViewModel<BaseNode>>().Persist();

        public static IEnumerable<BaseNode> Persist(this ISourceList<NodeViewModel> sourceList)
            => sourceList?.Items.Persist();

        #endregion

        #region Ports

        public static IEnumerable<TPort> Persist<TPort>(this IEnumerable<IPersistablePortViewModel<TPort>> portViewModels)
           where TPort : Port
           => portViewModels?.Select(p => p?.Persist());

        public static IEnumerable<InputPort> Persist(this IEnumerable<NodeInputViewModel> inputViewModels)
            => inputViewModels?.OfType<IPersistablePortViewModel<InputPort>>().Persist();

        public static IEnumerable<InputPort> Persist(this ISourceList<NodeInputViewModel> sourceList)
            => sourceList?.Items.Persist();

        public static IEnumerable<OutputPort> Persist(this IEnumerable<NodeOutputViewModel> inputViewModels)
            => inputViewModels?.OfType<IPersistablePortViewModel<OutputPort>>().Persist();

        public static IEnumerable<OutputPort> Persist(this ISourceList<NodeOutputViewModel> sourceList)
            => sourceList?.Items.Persist();


        #endregion

    }
}
