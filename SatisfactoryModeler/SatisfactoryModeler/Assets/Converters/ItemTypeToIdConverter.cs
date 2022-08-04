using SatisfactoryModeler.Models;

namespace SatisfactoryModeler.Assets.Converters
{
    public class ItemTypeToIdConverter : DataConverter<ItemType, string>
    {
        public static new IDataConverter<ItemType, string> Default { get; } = new ItemTypeToIdConverter();
    
        public override string Convert(ItemType data) => data?.Id;
        public override ItemType Invert(string data) => ItemType.ById(data);
    }
}
