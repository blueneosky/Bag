namespace Alphonse.Listener;

public sealed class AsyncLock
{
    private readonly SemaphoreSlim __gate = new(1, 1);

    private void LockWait(CancellationToken token) => this.__gate.Wait(token);
    private Task LockWaitAsync(CancellationToken token) => this.__gate.WaitAsync(token);
    private void LockRelease() => this.__gate.Release();

    public T Run<T>(Func<T> func, CancellationToken token = default)
    {
        this.LockWait(token);
        try
        {
            return func();
        }
        finally
        {
            this.LockRelease();
        }
    }

    public async Task RunAsync(Func<Task> task, CancellationToken token = default)
    {
        await this.LockWaitAsync(token).ConfigureAwait(false);
        try
        {
            await task().ConfigureAwait(false);
        }
        finally
        {
            this.LockRelease();
        }
    }

    public async Task<T> RunAsync<T>(Func<Task<T>> task, CancellationToken token = default)
    {
        await this.LockWaitAsync(token).ConfigureAwait(false);
        try
        {
            return await task().ConfigureAwait(false);
        }
        finally
        {
            this.LockRelease();
        }
    }
}

public static class AsyncLockExtensions
{
    public static void Run(this AsyncLock asyncLock, Action action, CancellationToken token = default)
        => asyncLock.Run(() => { action(); return default(object); }, token);
}