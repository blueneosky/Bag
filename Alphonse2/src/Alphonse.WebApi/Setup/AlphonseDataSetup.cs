using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Alphonse.WebApi.Services;
using Alphonse.WebApi.Models;

namespace Alphonse.WebApi.Setup;

public static class AlphonseDataSetup
{
    public static void SetupAlphonseData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            using var logginScope = logger.BeginScope("Data Setup");
            logger.LogInformation("[Data Setup] Starting ...");
            Setup(logger,
                serviceProvider.GetService<IOptions<AlphonseSettings>>()?.Value,
                new Lazy<AlphonseDbContext>(() => serviceProvider.GetRequiredService<AlphonseDbContext>()),
                new Lazy<IWebHostEnvironment>(() => serviceProvider.GetRequiredService<IWebHostEnvironment>()),
                new Lazy<IUserService>(() => serviceProvider.GetRequiredService<IUserService>()));
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
            Lazy<IWebHostEnvironment> environment,
            Lazy<IUserService> userService
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
        logger.LogDebug("Alphonse Settings updating ...");
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
            logger.LogInformation("Migration in progress ...");
            dbContext.Value.Database.Migrate();
            logger.LogInformation("Migration DONE");
        }
        logger.LogDebug("Alphonse DbContext checking DONE");


        // add regular users
        logger.LogDebug("Alphonse mandatory users checking ...");
        Task.Run(async () =>
        {
            if (!(await userService.Value.GetAllUsersAsync(AccessRights.Admin)).Any())
            {
                logger.LogInformation("Missing admin user, adding root/root ...");
                await userService.Value.CreateAsync(
                    settings.FallbackAdminUserName,
                    settings.FallbackAdminUserPass,
                    AccessRights.Admin);
                logger.LogInformation("Missing admin user, adding root/root DONE");
            }

            var alphonseUser = await userService.Value.GetUserAsync(settings.AlphonseListenerUserName);    // note : checked at startup (see OptionsSetup)
            const AccessRights constExpectedAlphonseRights = AccessRights.UserSelfRead | AccessRights.PhonebookRead | AccessRights.CallHistoryCreate;
            if (alphonseUser is null)
            {
                logger.LogInformation("Missing Alphonse.Listener user, adding '{User}' ...", settings.AlphonseListenerUserName);
                alphonseUser = await userService.Value.CreateAsync(
                    settings.AlphonseListenerUserName,
                    settings.AlphonseListenerUserPass,
                    constExpectedAlphonseRights);
                logger.LogInformation("Missing Alphonse.Listener user, adding '{User}' DONE", settings.AlphonseListenerUserName);
            }
            if (!(await userService.Value.TryValidateAsync(alphonseUser.Name, settings.AlphonseListenerUserPass)).success)
            {
                logger.LogInformation("Updating Alphonse.Listener pass ...");
                alphonseUser = await userService.Value.UpdatePasswordAsync(alphonseUser.Name, settings.AlphonseListenerUserPass);
                logger.LogInformation("Updating Alphonse.Listener pass DONE");
            }
            if (alphonseUser.Rights != constExpectedAlphonseRights)
            {
                logger.LogInformation("Updating Alphonse.Listener access rights ...");
                alphonseUser = await userService.Value.UpdateRightsAsync(alphonseUser.Name, constExpectedAlphonseRights);
                logger.LogInformation("Updating Alphonse.Listener access rights ...");
            }
        }).Wait();
        logger.LogDebug("Alphonse mandatory users checking DONE");
    }
}