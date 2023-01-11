namespace Alphonse.Listener;

public sealed class AsyncAccessBox<T>
    where T : class?
{
    private T? _value;
    private readonly AsyncLock _lock = new();

    public AsyncAccessBox(T initialValue)
    {
        this._value = initialValue;
    }

    private T VolatilGetValue() => Interlocked.CompareExchange(ref this._value, null, null)!;
    private void VolatilSetValue(T value) => Interlocked.Exchange(ref this._value, value);

    public T Value
    {
        get => this.Read();
        set => this.Write(value);
    }

    public T ReadNow() => VolatilGetValue();

    public void Use(Action<T> action, CancellationToken token = default)
        => this._lock.Run(() =>
        {
            var value = this.VolatilGetValue();
            action(value);
        }, token);

    public Task UseAsync(Func<T, Task> task, CancellationToken token = default)
        => this._lock.RunAsync(() =>
        {
            var value = this.VolatilGetValue();
            return task(value);
        }, token);

    public void Change(Func<T, T> setter, CancellationToken token = default)
        => this._lock.Run(() =>
        {
            var value = this.VolatilGetValue();
            value = setter(value!);
            this.VolatilSetValue(value);
        }, token);

    public Task ChangeAsync(Func<T, Task<T>> task, CancellationToken token = default)
        => this._lock.RunAsync(async () =>
        {
            var value = this.VolatilGetValue();
            value = await task(value!).ConfigureAwait(false);
            this.VolatilSetValue(value);
        }, token);
}

public static class AsyncAccessBoxExtensions
{
    public static bool TryRead<T>(this AsyncAccessBox<T> box, out T value, CancellationToken? token = default)
        where T : class?
    {
        try
        {
            value = box.Read(token ?? new CancellationTokenSource(TimeSpan.FromMilliseconds(1)).Token);
            return true;
        }
        catch (Exception)
        {
            value = default!;
            return false;
        }
    }

    public static T Read<T>(this AsyncAccessBox<T> box, CancellationToken token = default)
        where T : class?
    {
        T value = default!;
        box.Use(v => value = v, token);
        return value;
    }

    public static async Task<T> ReadAsync<T>(this AsyncAccessBox<T> box, CancellationToken token = default)
        where T : class?
    {
        T value = default!;
        await box.UseAsync(v => Task.FromResult(value = v), token);
        return value;
    }

    public static void Write<T>(this AsyncAccessBox<T> box, T newValue, CancellationToken token = default) where T : class?
        => box.Change(_ => newValue, token);

    public static Task WriteAsync<T>(this AsyncAccessBox<T> box, T newValue, CancellationToken token = default) where T : class?
        => box.ChangeAsync(_ => Task.FromResult(newValue), token);
}