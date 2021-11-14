using SatisfactoryModeler.Persistance.Configurations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SatisfactoryModeler.Models
{
    [DebuggerDisplay("Fluide {Id}")]
    public sealed class FluideType
    {
        private static readonly Lazy<List<FluideType>> _allCache
            = new Lazy<List<FluideType>>(() => Rules.Instance.Configuration.FluideTypes.Select(Create).ToList());
        private static readonly Lazy<IDictionary<string, FluideType>> _typeByIdsCache
            = new Lazy<IDictionary<string, FluideType>>(() => All.ToDictionary(ft => ft.Id));

        public static IReadOnlyList<FluideType> All => _allCache.Value;

        public static FluideType ById(string id)
            => _typeByIdsCache.Value.TryGetValue(id, out FluideType result) ? result : null;

        private static FluideType Create(CfgFluideType source) => new FluideType(source);

        private readonly CfgFluideType source;

        private FluideType(CfgFluideType source) => this.source = source;

        public string Id => this.source.Id;

        public string Label => this.source.Label;

        public override string ToString() => this.Label;
    }
}
