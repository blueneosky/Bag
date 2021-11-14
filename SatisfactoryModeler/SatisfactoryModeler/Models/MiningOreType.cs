using SatisfactoryModeler.Persistance.Configurations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SatisfactoryModeler.Models
{
    [DebuggerDisplay("MiningOre {Id}")]
    public sealed class MiningOreType
    {
        private static readonly Lazy<List<MiningOreType>> _allCache
            = new Lazy<List<MiningOreType>>(() => Rules.Instance.Configuration.MiningOreTypes.Split(';').Select(Create).ToList());
        private static readonly Lazy<IDictionary<string, MiningOreType>> _typeByIds
            = new Lazy<IDictionary<string, MiningOreType>>(() => All.ToDictionary(ot => ot.Id));

        public static IReadOnlyList<MiningOreType> All => _allCache.Value;

        public static MiningOreType ById(string id)
            => _typeByIds.Value.TryGetValue(id, out MiningOreType result) ? result : null;

        private static MiningOreType Create(string oreTypeId) => new MiningOreType(ItemType.ById(oreTypeId));

        private readonly ItemType source;

        private MiningOreType(ItemType source) => this.source = source;

        public string Id => this.source.Id;

        public string Label => this.source.Label;

        public override string ToString() => Label;

        public static explicit operator MiningOreType(ItemType itemType) => ById(itemType?.Id);
        public static implicit operator ItemType(MiningOreType oreType) => oreType?.source;
    }
}
