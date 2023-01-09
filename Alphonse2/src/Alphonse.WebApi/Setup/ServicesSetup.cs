using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Alphonse.WebApi.Setup;

public static class ServicesSetup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024, }));
        builder.Services.AddSingleton<IPasswordHasher<string>, PasswordHasher<string>>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ISessionService, SessionService>();
    }
}
