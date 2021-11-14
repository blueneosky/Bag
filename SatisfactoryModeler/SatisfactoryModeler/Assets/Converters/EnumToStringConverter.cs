using System;

namespace SatisfactoryModeler.Assets.Converters
{
    public class EnumToStringConverter<TEnum> : DataConverter<TEnum, string>
        where TEnum : struct
    {
        public static new IDataConverter<TEnum, string> Default { get; } = new EnumToStringConverter<TEnum>();
     
        public override string Convert(TEnum data)
            => Enum.GetName(typeof(TEnum), data);

        public override TEnum Invert(string data)
            => data != null && Enum.TryParse<TEnum>(data, true, out TEnum result) ? result : default;
    }
}
