using NLog;
using NLog.Web;

namespace Alphonse.WebApi.Setup;

public static class NLogSetup
{
    public static void ConfigureNLog(this WebApplicationBuilder builder)
    {
        var setupBuilder = NLog.LogManager.Setup()
            .LoadConfigurationFromAppSettings();
        setupBuilder.LogFactory.AutoShutdown = false;   // Unhook from AppDomain, to depend on host
        var logger = setupBuilder.GetCurrentClassLogger();
        logger.Debug("NLog loaded");

        var logging = builder.Logging;

        // configure Logging with NLog
        // source : https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-6
        // NLog.LogManager.AutoShutdown = false; 
        logging.ClearProviders();
        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

        builder.Host.UseNLog(new NLogAspNetCoreOptions() { ShutdownOnDispose = true });
    }
}