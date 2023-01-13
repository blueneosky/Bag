namespace System;

public static class FluentConditionnamCallExtensions
{
    public static T When<T>(this T src, Func<T, bool> predicate, Func<T, T> whenTrue, Func<T, T> whenFalse)
        => predicate is not null
            ? src.When(predicate(src), whenTrue, whenFalse)
            : throw new ArgumentNullException(nameof(predicate));

    public static T When<T>(this T src, bool condition, Func<T, T> whenTrue, Func<T, T> whenFalse)
    {
        _ = whenTrue ?? throw new ArgumentNullException(nameof(whenTrue));
        _ = whenFalse ?? throw new ArgumentNullException(nameof(whenFalse));

        return condition ? whenTrue(src) : whenFalse(src);
    }

    public static T WhenTrue<T>(this T src, Func<T, bool> predicate, Func<T, T> func)
        => src.When(predicate, func, i => i);

    public static T WhenFalse<T>(this T src, Func<T, bool> predicate, Func<T, T> func)
        => src.When(predicate, i => i, func);

    public static T WhenTrue<T>(this T src, bool condition, Func<T, T> func)
        => src.When(condition, func, i => i);

    public static T WhenFalse<T>(this T src, bool condition, Func<T, T> func)
        => src.When(condition, i => i, func);


    public static T WhenNull<T, TValue>(this T src, Func<T, TValue?> selector, Func<T, T> whenNull)
        where T : notnull => selector is not null
        ? src.WhenNull(selector(src), whenNull)
        : throw new ArgumentNullException(nameof(selector));

    public static T WhenNull<T, TValue>(this T src, TValue? value, Func<T, T> whenNull)
        where T : notnull => src.When(value is null, whenNull, i => i);

    public static T WhenNotNull<T, TValue>(this T src, Func<T, TValue?> selector, Func<T, TValue, T> whenNotNull)
        where T : struct => selector is not null
        ? src.WhenNotNull(selector(src), whenNotNull)
        : throw new ArgumentNullException(nameof(selector));

    public static T WhenNotNull<T, TValue>(this T src, TValue? value, Func<T, TValue, T> whenNotNull)
        where T : notnull => src.When(value is not null, s => whenNotNull(s, value!), i => i);
}