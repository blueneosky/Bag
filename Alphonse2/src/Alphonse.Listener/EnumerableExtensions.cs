namespace Alphonse.Listener;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> VariableBatch<T>(this IEnumerable<T> source, IEnumerable<int> batchSizes)
    {
        using var batchIt = batchSizes.GetEnumerator();
        if (!batchIt.MoveNext())
            yield break;

        var remaining = batchIt.Current;
        var batch = new List<T>(remaining);
        foreach (var item in source)
        {
            batch.Add(item);
            if (--remaining > 0)
                continue;

            yield return batch;

            if (!batchIt.MoveNext())
                yield break;
                
            remaining = batchIt.Current;
            batch = new List<T>(remaining);
        }

        if (batch.Any())
            yield return batch;
    }
}