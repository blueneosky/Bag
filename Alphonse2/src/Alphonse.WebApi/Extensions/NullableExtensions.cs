namespace Alphonse.WebApi.Extensions;

public static class NullableExtensions
{
    public static TResult TernaryNullable<T, TResult>(this T? source, Func<TResult> whenNull, Func<T, TResult> whenNotNull)
        where T : struct
        => source is null ? whenNull() : whenNotNull(source.Value);

    public static TResult TernaryNullable<T, TResult>(this T? source, Func<TResult> whenNull, Func<T, TResult> whenNotNull)
        where T : class
        => source is null ? whenNull() : whenNotNull(source);
}
