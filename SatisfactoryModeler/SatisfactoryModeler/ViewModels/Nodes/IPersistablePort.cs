using SatisfactoryModeler.Persistance.Networks;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public interface IPersistablePort : IPersistable
    {
        IPersistable Parent { get; }
        string PortName { get; }
    }

    public interface IPersistablePort<TPort> : IPersistablePort, IPersistable<TPort>
        where TPort : Port
    {
    }
}
