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
        Task.Run(() => SetupUsers(logger, settings, userService.Value)).Wait();
        logger.LogDebug("Alphonse mandatory users checking DONE");
    }

    private static async Task SetupUsers(
        ILogger logger,
        AlphonseSettings settings,
        IUserService userService
        )
    {
        await SetupAdminUser().ConfigureAwait(false);
        await SetupListenerServiceUSer().ConfigureAwait(false);

        //======================================================

        async Task SetupAdminUser()
        {
            if ((await userService.GetAllUsersAsync(AccessRoleService.ROLE_ADMIN)).Any())
                return;

            logger.LogInformation("Missing admin user, 'FallbackAdmin' user setup ...");

            var currentAdminUser = await userService.GetUserAsync(settings.FallbackAdminUserName);
            if (currentAdminUser is null)
            {
                currentAdminUser = await userService.CreateAsync(
                    settings.FallbackAdminUserName,
                    settings.FallbackAdminUserPass,
                    AccessRoleService.ROLE_ADMIN);
                logger.LogInformation("Missing admin user, 'FallbackAdmin' CREATED");

                return;
            }

            // check role
            if (currentAdminUser.AccessRole != AccessRoleService.ROLE_ADMIN)
            {
                logger.LogInformation("Missing 'admin' role for 'FallbackAdmin' ...");
                currentAdminUser = await userService.UpdateRightsAsync(currentAdminUser.Name, AccessRoleService.ROLE_ADMIN);
            }

            // check pass
            var (validated, _) = await userService.TryValidateAsync(currentAdminUser.Name, settings.FallbackAdminUserPass);
            if (!validated)
            {
                logger.LogInformation("Mismatch pass for 'FallbackAdmin' ...");
                currentAdminUser = await userService.UpdatePasswordAsync(currentAdminUser.Name, settings.FallbackAdminUserPass);
            }

            logger.LogInformation("Missing admin user, 'FallbackAdmin' UPDATED");
        }

        async Task SetupListenerServiceUSer()
        {
            var serviceUser = await userService.GetUserAsync(settings.AlphonseListenerUserName);    // note : checked at startup (see OptionsSetup)
            if (serviceUser is null)
            {
                logger.LogInformation("Missing Alphonse.Listener user, adding '{User}' ...", settings.AlphonseListenerUserName);
                serviceUser = await userService.CreateAsync(
                    settings.AlphonseListenerUserName,
                    settings.AlphonseListenerUserPass,
                    AccessRoleService.ROLE_SERVICE_LISTENER);
                logger.LogInformation("Missing Alphonse.Listener user, adding '{User}' DONE", settings.AlphonseListenerUserName);
            }
            if (!(await userService.TryValidateAsync(serviceUser.Name, settings.AlphonseListenerUserPass)).success)
            {
                logger.LogInformation("Updating Alphonse.Listener pass ...");
                serviceUser = await userService.UpdatePasswordAsync(serviceUser.Name, settings.AlphonseListenerUserPass);
                logger.LogInformation("Updating Alphonse.Listener pass DONE");
            }
            if (serviceUser.AccessRole != AccessRoleService.ROLE_SERVICE_LISTENER)
            {
                logger.LogInformation("Updating Alphonse.Listener access rights ...");
                serviceUser = await userService.UpdateRightsAsync(serviceUser.Name, AccessRoleService.ROLE_SERVICE_LISTENER);
                logger.LogInformation("Updating Alphonse.Listener access rights DONE");
            }
            
        }
    }
}