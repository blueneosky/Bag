using SatisfactoryModeler.Persistance.Configurations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SatisfactoryModeler.Models
{
    [DebuggerDisplay("Item {Id}")]
    public sealed class ItemType
    {
        private static readonly Lazy<List<ItemType>> _allCache
            = new Lazy<List<ItemType>>(() => Rules.Instance.Configuration.ItemTypes.Select(Create).ToList());
        private static readonly Lazy<IDictionary<string, ItemType>> _typeByIds
            = new Lazy<IDictionary<string, ItemType>>(() => All.ToDictionary(it => it.Id));

        public static IReadOnlyList<ItemType> All => _allCache.Value;

        public static ItemType ById(string id)
            => _typeByIds.Value.TryGetValue(id, out ItemType result) ? result : null;

        private static ItemType Create(CfgItemType source) => new ItemType(source);

        private readonly CfgItemType source;

        private ItemType(CfgItemType source) => this.source = source;

        public string Id => this.source.Id;

        public string Label => this.source.Label;

        public int StackSize => this.source.StackSize;

        public int SinkValue => this.source.SinkValue;
 
        public override string ToString() => Label;
    }
}
