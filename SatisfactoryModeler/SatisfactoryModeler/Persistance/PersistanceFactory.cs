using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Persistance
{
    public class PersistanceFactory
    {
        public static PersistanceFactory Instance { get; } = new PersistanceFactory();
        private PersistanceFactory() { }

        public IPersistanceEngine<TRoot> GetDefault<TRoot>() => Get<TRoot>("json");

        public IPersistanceEngine<TRoot> Get<TRoot>(string engine)
        {
            switch (engine.ToLowerInvariant())
            {
                case "json": return new JsonPersistanceEngine<TRoot>();
                case "xml": return new XmlPersistaanceEngine<TRoot>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(engine), $"Unsupported engine {engine}");
            }
        }
    }
}
