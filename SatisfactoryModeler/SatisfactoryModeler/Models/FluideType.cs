using SatisfactoryModeler.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SatisfactoryModeler.Models
{
    public sealed class FluideType
    {
        private static readonly Lazy<FluideType[]> _allCache = new Lazy<FluideType[]>(() => Rules.Instance.FluideTypes);
        private static readonly Lazy<IDictionary<string, FluideType>> _typeByIdsCache
            = new Lazy<IDictionary<string, FluideType>>(() => All.ToDictionary(ft => ft.Id));
        private static readonly Lazy<IDictionary<string, FluideType>> _typeByCategoriesCache
            = new Lazy<IDictionary<string, FluideType>>(() => All.ToDictionary(ft => ft.Category));

        public static FluideType[] All => _allCache.Value;

        public static FluideType ById(string id)
            => _typeByIdsCache.Value.TryGetValue(id, out FluideType result) ? result : null;
        public static FluideType ByCategory(string category)
            => _typeByCategoriesCache.Value.TryGetValue(category, out FluideType result) ? result : null;

        [XmlAttribute("Id")]
        public string Id { get;  set; }

        [XmlAttribute("Cat")]
        public string Category { get;  set; }

        [XmlAttribute("Lbl")]
        public string Label { get;  set; }
    }
}
