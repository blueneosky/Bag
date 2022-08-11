namespace Alphonse.Listener;

public sealed class AsyncLock
{
    private readonly SemaphoreSlim _gate = new (1, 1);

    public void Run(Action action) => this.Run(action, CancellationToken.None);

    public void Run(Action action, CancellationToken token)
    {
        this._gate.Wait(token);
        try
        {
            action();
        }
        finally
        {
            this._gate.Release();
        }
    }

    public T Run<T>(Func<T> func) => this.Run(func, CancellationToken.None);

    public T Run<T>(Func<T> func, CancellationToken token)
    {
        this._gate.Wait(token);
        try
        {
            return func();
        }
        finally
        {
            this._gate.Release();
        }
    }

    public Task RunAsync(Func<Task> task) => this.RunAsync(task, CancellationToken.None);

    public async Task RunAsync(Func<Task> task, CancellationToken token)
    {
        await this._gate.WaitAsync(token).ConfigureAwait(false);
        try
        {
            await task().ConfigureAwait(false);
        }
        finally
        {
            this._gate.Release();
        }
    }

    public Task<T> RunAsync<T>(Func<Task<T>> task) => this.RunAsync(task, CancellationToken.None);
    
    public async Task<T> RunAsync<T>(Func<Task<T>> task, CancellationToken token)
    {
        await this._gate.WaitAsync(token).ConfigureAwait(false);
        try
        {
            return await task().ConfigureAwait(false);
        }
        finally
        {
            this._gate.Release();
        }
    }
}