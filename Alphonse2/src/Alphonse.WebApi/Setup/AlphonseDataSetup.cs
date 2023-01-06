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
                new Lazy<AlphonseDbContext>(() => serviceProvider.GetRequiredService<AlphonseDbContext>()),
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
            Lazy<AlphonseDbContext> dbContext,
            Lazy<IWebHostEnvironment> environment
            )
        {
            // settings checks
            logger.LogDebug("Alphonse Settings checking ...");
            if (settings is null)
                throw new InvalidOperationException("Missing Alphonse settings");
            logger.LogDebug("Alphonse Settings: {Settings}", JsonSerializer.Serialize(settings));

            if (string.IsNullOrWhiteSpace(settings.DataDirPath))
                throw new InvalidOperationException($"Missing {nameof(AlphonseSettings.DataDirPath)} in Alphonse settings");

            if (string.IsNullOrWhiteSpace(settings.DbPath))
                throw new InvalidOperationException($"Missing {nameof(AlphonseSettings.DbPath)} in Alphonse settings");

            logger.LogDebug("Alphonse Settings checking DONE");


            // update settings
            logger.LogDebug("Alphonse Settings updating...");
            if (!Path.IsPathRooted(settings.DataDirPath)
                && environment.Value.IsDevelopment())
            {
                // re-root under binaries folder
                var appDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                settings.DataDirPath = Path.Join(appDir, settings.DataDirPath);
                logger.LogInformation($"{nameof(AlphonseSettings.DataDirPath)} update with value {{DataDirPath}}", settings.DataDirPath);

                Directory.CreateDirectory(settings.DataDirPath);
            }
            logger.LogDebug("Alphonse Settings updating DONE");


            // auto-migrate db context
            logger.LogDebug("Alphonse DbContext checking ...");
            if (dbContext.Value.Database.GetPendingMigrations().Any())
            {
                logger.LogInformation("Migration in progress...");
                dbContext.Value.Database.Migrate();
                logger.LogInformation("Migration DONE");
            }
            logger.LogDebug("Alphonse DbContext checking DONE");
        }
}