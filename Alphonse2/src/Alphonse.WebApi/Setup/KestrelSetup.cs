using Microsoft.Extensions.Options;

namespace Alphonse.WebApi.Setup;

public static class KestrelSetup
{
    public static void ConfigureKestrel(this WebApplicationBuilder builder)
    {
        // setup listen port
        builder.WebHost.UseKestrel((webHostBuilderContext, kestrelServerOptions) =>
        {
            var serviceProvider = kestrelServerOptions.ApplicationServices;

            var alphonseSettings = serviceProvider.GetService<IOptions<AlphonseSettings>>()!.Value;

            var port = alphonseSettings.ListenPort;
            if (port is null)
            {
                var logger = serviceProvider.GetService<ILogger<AlphonseSettings>>();
                port = 6001;    // default
                logger?.LogWarning($"{nameof(AlphonseSettings.ListenPort)} was not defined - fallback to {{ListenPort}}", port.Value);
            }

            kestrelServerOptions.ListenLocalhost(port.Value, listenOptions =>
            {
                if(alphonseSettings.WithKestrelConnectionLogging)
                    listenOptions.UseConnectionLogging();
            });
        });

    }
}