using SatisfactoryModeler.Models;

namespace SatisfactoryModeler.Assets.Converters
{
    public class MiningOreTypeToIdConverter : DataConverter<MiningOreType, string>
    {
        public static new IDataConverter<MiningOreType, string> Default { get; } = new MiningOreTypeToIdConverter();
       
        public override string Convert(MiningOreType data) => data?.Id;
        public override MiningOreType Invert(string data) => MiningOreType.ById(data);
    }
}
