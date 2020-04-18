namespace SatisfactoryModeler.Models
{
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
            => rate.HasValue ? From(type, rate.Value) : (FluideFlow?)null;
    }
}
