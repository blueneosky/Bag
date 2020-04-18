using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Models
{
    public struct ItemRate
    {
        public ItemTypes Type { get; }
        public double Rate { get; }

        public ItemRate(ItemTypes type, double rate)
        {
            this.Type = type;
            this.Rate = rate;
        }

        public static ItemRate From(ItemTypes type, double rate)
                => new ItemRate(type, rate);

        public static ItemRate? From(ItemTypes? type, double? rate)
            => type.HasValue & rate.HasValue ? From(type.Value, rate.Value) : (ItemRate?)null;
    }
}
