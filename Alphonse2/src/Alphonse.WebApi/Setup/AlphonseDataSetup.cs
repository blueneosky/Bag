using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Alphonse.WebApi.Setup;

public static class AlphonseDataSetup
{
    public static void SetupAlphonseData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var logger = serviceProvider.GetService<ILogger<Program>>()!;

        try
        {
            using var logginScope = logger.BeginScope("Data Setup");
            logger.LogInformation("[Data Setup] Starting ...");
            Setup(logger,
                serviceProvider.GetService<IOptions<AlphonseSettings>>()?.Value,
                serviceProvider.GetService<AlphonseDbContext>()!,
                new Lazy<IWebHostEnvironment>(() => serviceProvider.GetRequiredService<IWebHostEnvironment>()));
            logger.LogInformation("[Data Setup] Starting DONE");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "[Data Setup] Unexpected error !");
            throw;
        }
    }

    private static void Setup(ILogger logger,
            AlphonseSettings? settings,
            AlphonseDbContext dbContext,
            Lazy<IWebHostEnvironment> environment
            )
        {
            // settings checks
            logger.LogDebug("Alphonse Settings checking ...");
            if (settings is null)
                throw new InvalidOperationException("Missing Alphonse settings");
            logger.LogDebug("Alphonse Settings: {Settings}", JsonSerializer.Serialize(settings));

            if (string.IsNullOrWhiteSpace(settings.DataBasePath))
                throw new InvalidOperationException($"Missing {nameof(AlphonseSettings.DataBasePath)} in Alphonse settings");

            if (string.IsNullOrWhiteSpace(settings.DbPath))
                throw new InvalidOperationException($"Missing {nameof(AlphonseSettings.DbPath)} in Alphonse settings");

            logger.LogDebug("Alphonse Settings checking DONE");


            // update settings
            logger.LogDebug("Alphonse Settings updating...");
            if (!Path.IsPathRooted(settings.DataBasePath))
            {
                if (environment.Value.IsDevelopment())
                {
                    // re-root under binaries folder
                    var appDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    settings.DataBasePath = Path.Join(appDir, settings.DataBasePath);
                    logger.LogInformation($"{nameof(AlphonseSettings.DataBasePath)} update with value {{DataBasePath}}", settings.DataBasePath);
                }
            }
            Directory.CreateDirectory(settings.DataBasePath);
            logger.LogDebug("Alphonse Settings updating DONE");


            // auto-migrate db context
            logger.LogDebug("Alphonse DbContext checking ...");
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                logger.LogInformation("Migration in progress...");
                dbContext.Database.Migrate();
                logger.LogInformation("Migration DONE");
            }
            logger.LogDebug("Alphonse DbContext checking DONE");
        }
}