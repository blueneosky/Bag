using System.Diagnostics;

namespace SatisfactoryModeler.Models
{
    [DebuggerDisplay("{ToString()}")]
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

        public static ItemFlow? From(ItemType type, double? rate)
            => type != null & rate.HasValue ? From(type, rate.Value) : (ItemFlow?)null;

        public override string ToString() => ToString("[{0}] {1}/min");
        public string ToString(string format) => string.Format(format, this.Type, this.Rate);
    }
}
