using SatisfactoryModeler.Persistance.Networks;
using System;
using System.Collections.Concurrent;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class PersistableViewModelFactory
    {
        public static PersistableViewModelFactory Instance { get; } = new PersistableViewModelFactory();
        private PersistableViewModelFactory() { }

        private ConcurrentDictionary<Type, Func<BaseNode, IPersistableNodeViewModel>> nodeViewModelFactoryByPersistedTypes
            = new ConcurrentDictionary<Type, Func<BaseNode, IPersistableNodeViewModel>>();

        public void Register<TPersisted, TPersistable>(Func<TPersisted, TPersistable> nodeViewModelFactory)
            where TPersisted : BaseNode
            where TPersistable : IPersistableNodeViewModel
            => nodeViewModelFactoryByPersistedTypes.AddOrUpdate(
                typeof(TPersisted),
                _ => (n => nodeViewModelFactory((TPersisted)n)),
                (_, __) => throw new InvalidOperationException($"'{typeof(TPersisted)}' already registered !"));

        public IPersistableNodeViewModel Create(BaseNode node)
            => nodeViewModelFactoryByPersistedTypes.TryGetValue(node.GetType(), out Func<BaseNode, IPersistableNodeViewModel> instanceFactory)
                ? instanceFactory(node)
                : throw new InvalidOperationException();
    }
}
