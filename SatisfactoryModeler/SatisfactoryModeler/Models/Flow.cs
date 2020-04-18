using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Models
{
    public struct Flow
    {
        public ItemType Type { get; }
        public double Rate { get; }

        public Flow(ItemType type, double rate)
        {
            this.Type = type;
            this.Rate = rate;
        }

        public static Flow From(ItemType type, double rate)
                => new Flow(type, rate);

        public static Flow? From(ItemType? type, double? rate)
            => type.HasValue & rate.HasValue ? From(type.Value, rate.Value) : (Flow?)null;
    }
}
