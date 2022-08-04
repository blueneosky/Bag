using SatisfactoryModeler.Persistance.Networks;
using System;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public interface IPersistableNodeViewModel
    {
        Guid Id { get; }

        object Persist();
    }

    public interface IPersistableNodeViewModel<out TNode> : IPersistableNodeViewModel
        where TNode : BaseNode
    {
        new TNode Persist();
    }
}
