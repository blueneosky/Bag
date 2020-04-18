using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public static class PersistableExtensions
    {
        public static TPersisted Persist<TPersisted>(this IPersistable<TPersisted> persistable)
            where TPersisted : class
            => persistable?.Persist(null);

        public static IEnumerable<TPersisted> Persist<TPersisted>(this IEnumerable<IPersistable<TPersisted>> persistables)
            where TPersisted : class
            => persistables?.Select(Persist);

        public static object Persist(this IPersistable persistable)
            => persistable?.Persist(null);

        public static IEnumerable<TPersisted> Persist<TPersisted>(this IEnumerable<IPersistable> persistables)
            where TPersisted : class
            => persistables?.Persist().OfType<TPersisted>();

        public static IEnumerable<object> Persist(this IEnumerable<IPersistable> persistables)
            => persistables?.Select(Persist);
    }
}
