using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Alphonse.Listener;

public class ServiceFactory<T> where T : notnull
{
        private readonly IServiceProvider _provider;
    public ServiceFactory(IServiceProvider provider)
    {
            this._provider = provider;
        
    }

    public T GetRequired() => _provider.GetRequiredService<T>();
    public T Get() => _provider.GetRequiredService<T>();

    public static implicit operator T(ServiceFactory<T> factory) => factory.GetRequired();
}
