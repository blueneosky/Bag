namespace SatisfactoryModeler.Assets.Converters
{
    public interface IDataConverter
    {
        object Convert(object data);
        object Invert(object data);
    }

    public interface IDataConverter<T1, T2> : IDataConverter
    {
        T2 Convert(T1 data);
        T1 Invert(T2 data);
    }
}
