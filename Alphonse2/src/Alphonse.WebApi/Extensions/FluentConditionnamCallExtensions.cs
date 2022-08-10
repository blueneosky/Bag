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



    public static T WhenNonNull<T, TValue>(this T src, Func<T, TValue?> nullableSelector,
        Func<T, T> whenNonNull, Func<T, T> whenNull)
        where TValue : struct
        => nullableSelector is not null
            ? src.WhenNonNull(nullableSelector(src), whenNonNull, whenNull)
            : throw new ArgumentNullException(nameof(nullableSelector));

    public static T WhenNonNull<T, TValue>(this T src, TValue? nullableValue,
        Func<T, T> whenNonNull, Func<T, T> whenNull)
        where TValue : struct
        => src.When(nullableValue.HasValue, whenNonNull, whenNull);

    public static T WhenNonNull<T, TValue>(this T src, Func<T, TValue?> nullableSelector, Func<T, T> func)
        where TValue : struct
        => src.WhenNonNull(nullableSelector, func, i => i);

    public static T WhenNull<T, TValue>(this T src, Func<T, TValue?> nullableSelector, Func<T, T> func)
        where TValue : struct
        => src.WhenNonNull(nullableSelector, i => i, func);

    public static T WhenNonNull<T, TValue>(this T src, TValue? nullableValue, Func<T, T> func)
        where TValue : struct
        => src.WhenNonNull(nullableValue, func, i => i);

    public static T WhenNull<T, TValue>(this T src, TValue? nullableValue, Func<T, T> func)
        where TValue : struct
        => src.WhenNonNull(nullableValue, i => i, func);



    public static T WhenNonNull<T, TValue>(this T src, Func<T, TValue?> nullableSelector,
        Func<T, TValue, T> whenNonNull, Func<T, T> whenNull)
        where TValue : struct
        => nullableSelector is not null
            ? src.WhenNonNull(nullableSelector(src), whenNonNull, whenNull)
            : throw new ArgumentNullException(nameof(nullableSelector));

    public static T WhenNonNull<T, TValue>(this T src, TValue? nullableValue,
        Func<T, TValue, T> whenNonNull, Func<T, T> whenNull)
        where TValue : struct
        => whenNonNull is not null
            ? src.WhenNonNull(nullableValue, i => whenNonNull(i, nullableValue!.Value), whenNull)
            : throw new ArgumentNullException(nameof(whenNonNull));

    public static T WhenNonNull<T, TValue>(this T src, Func<T, TValue?> nullableSelector, Func<T, TValue, T> whenNonNull)
        where TValue : struct
        => src.WhenNonNull(nullableSelector, whenNonNull, i => i);

    public static T WhenNonNull<T, TValue>(this T src, TValue? nullableValue, Func<T, TValue, T> whenNonNull)
        where TValue : struct
        => src.WhenNonNull(nullableValue, whenNonNull, i => i);
}