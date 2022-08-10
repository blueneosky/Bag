using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Alphonse.WebApi.Setup;

public static class AlphonseDataSetup
{
    public static void SetupAlphonseData(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        // settings checks
        var alphonseSettings = serviceProvider.GetService<IOptions<AlphonseSettings>>()?.Value;
        if (alphonseSettings is null)
            throw new InvalidOperationException("Missing Alphonse settings");

        if (string.IsNullOrWhiteSpace(alphonseSettings.DataBasePath))
            throw new InvalidOperationException("Missing DbPath in Alphonse settings");

        if (string.IsNullOrWhiteSpace(alphonseSettings.DbPath))
            throw new InvalidOperationException("Missing DbPath in Alphonse settings");

        // update settings
        if (!Path.IsPathRooted(alphonseSettings.DataBasePath))
        {
            var environment = serviceProvider.GetService<IWebHostEnvironment>();
            if (environment!.IsDevelopment())
            {
                // re-root under binaries folder
                var appDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                alphonseSettings.DataBasePath = Path.Join(appDir, alphonseSettings.DataBasePath);
            }
        }
        Directory.CreateDirectory(alphonseSettings.DataBasePath);


        // auto-migrate db context
        using var db = serviceProvider.GetService<AlphonseDbContext>();
        Debug.Assert(db is not null);
        if (db.Database.GetPendingMigrations().Any())
        {
            var dbLogger = serviceProvider.GetService<ILogger<AlphonseDbContext>>();
            dbLogger?.LogInformation("Migration in progress...");
            db.Database.Migrate();
            dbLogger?.LogInformation("Migration DONE");
        }
    }
}