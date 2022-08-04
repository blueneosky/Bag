using System.Diagnostics;

namespace SatisfactoryModeler.Models
{
    [DebuggerDisplay("{ToString()}")]
    public struct FluideFlow
    {
        public FluideType Type { get; }
        public double Rate { get; }

        public FluideFlow(FluideType type, double rate)
        {
            this.Type = type;
            this.Rate = rate;
        }

        public static FluideFlow From(FluideType type, double rate)
                => new FluideFlow(type, rate);

        public static FluideFlow? From(FluideType type, double? rate)
            => type != null && rate.HasValue ? From(type, rate.Value) : (FluideFlow?)null;

        public override string ToString() => ToString("[{0}] {1}m3/min");
        public string ToString(string format) => string.Format(format, this.Type, this.Rate);
    }
}
