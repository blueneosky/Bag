using SatisfactoryModeler.Persistance.Networks;
using System;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    // TODO move everithing into generic with 'out'
    public interface IPersistablePortViewModel
    {
        Guid Id { get; }
        IPersistableNodeViewModel Parent { get; }
        string PortName { get; }
     
        object Persist();
    }

    public interface IPersistablePortViewModel<out TPort> : IPersistablePortViewModel
        where TPort : Port
    {
        new TPort Persist();
    }
}
