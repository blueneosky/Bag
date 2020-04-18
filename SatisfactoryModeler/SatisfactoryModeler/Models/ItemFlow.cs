using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Models
{
    public struct ItemFlow
    {
        public ItemType Type { get; }
        public double Rate { get; }

        public ItemFlow(ItemType type, double rate)
        {
            this.Type = type;
            this.Rate = rate;
        }

        public static ItemFlow From(ItemType type, double rate)
                => new ItemFlow(type, rate);

        public static ItemFlow? From(ItemType? type, double? rate)
            => type.HasValue & rate.HasValue ? From(type.Value, rate.Value) : (ItemFlow?)null;
    }
}
