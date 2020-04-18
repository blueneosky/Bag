using SatisfactoryModeler.Persistance.Networks;
using SatisfactoryModeler.Persistance.Objects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class PersistableViewModelFactory
    {
        public static PersistableViewModelFactory Instance { get; } = new PersistableViewModelFactory();
        private PersistableViewModelFactory() { }

        private ConcurrentDictionary<Type, Func<object, IPersistable>> instanceFactoryByPersistedTypes
            = new ConcurrentDictionary<Type, Func<object, IPersistable>>();

        public void Register<TPersisted, TPersistable>(Func<TPersisted, TPersistable> instanceFactory)
            where TPersistable : IPersistable
            => instanceFactoryByPersistedTypes.AddOrUpdate(
                typeof(TPersisted),
                _ => n => instanceFactory((TPersisted)n),
                (_, __) => throw new InvalidOperationException($"'{typeof(TPersisted)}' already registered !"));

        public IPersistable Create(BaseNode node)
            => instanceFactoryByPersistedTypes.TryGetValue(node.GetType(), out Func<object, IPersistable> instanceFactory)
                ? instanceFactory(node)
                : throw new InvalidOperationException();
    }
}
