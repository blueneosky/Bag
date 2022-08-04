using SatisfactoryModeler.Models;

namespace SatisfactoryModeler.Assets.Converters
{
    public class FluideTypeToIdConverter : DataConverter<FluideType, string>
    {
        public static new IDataConverter<FluideType, string> Default { get; } = new FluideTypeToIdConverter();
     
        public override string Convert(FluideType data) => data?.Id;
        public override FluideType Invert(string data) => FluideType.ById(data);
    }
}
