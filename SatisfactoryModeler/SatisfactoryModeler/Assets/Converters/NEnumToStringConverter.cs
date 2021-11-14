using System;

namespace SatisfactoryModeler.Assets.Converters
{
    public class NEnumToStringConverter<TEnum> : DataConverter<TEnum?, string>
        where TEnum : struct
    {
        public static new IDataConverter<TEnum?, string> Default { get; } = new NEnumToStringConverter<TEnum>();

        public override string Convert(TEnum? data)
            => data != null ? Enum.GetName(typeof(TEnum), data) : null;

        public override TEnum? Invert(string data)
            => data != null && Enum.TryParse<TEnum>(data, true, out TEnum result) ? result : default;
    }
}
