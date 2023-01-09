using Alphonse.WebApi.Models;

namespace Alphonse.WebApi.Setup;

public static class OptionsSetup
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<AlphonseSettings>()
            .Bind(builder.Configuration.GetSection("Alphonse"))
            .Validate(s => !string.IsNullOrWhiteSpace(s.JwtSecretKey), $"Missing {nameof(AlphonseSettings.JwtSecretKey)} in Alphonse settings")
            .Validate(s => !string.IsNullOrWhiteSpace(s.DataDirPath), $"Missing {nameof(AlphonseSettings.DataDirPath)} in Alphonse settings")
            .Validate(s => !string.IsNullOrWhiteSpace(s.DbPath), $"Missing {nameof(AlphonseSettings.DbPath)} in Alphonse settings")
            .Validate(s => !string.IsNullOrWhiteSpace(s.FallbackAdminUserName), $"Missing {nameof(AlphonseSettings.FallbackAdminUserName)} in Alphonse settings")
            .Validate(s => !string.IsNullOrWhiteSpace(s.FallbackAdminUserPass), $"Missing {nameof(AlphonseSettings.FallbackAdminUserPass)} in Alphonse settings")
            .Validate(s => !string.IsNullOrWhiteSpace(s.AlphonseListenerUserName), $"Missing {nameof(AlphonseSettings.AlphonseListenerUserName)} in Alphonse settings")
            .Validate(s => !string.IsNullOrWhiteSpace(s.AlphonseListenerUserPass), $"Missing {nameof(AlphonseSettings.AlphonseListenerUserPass)} in Alphonse settings")
            .Validate(s => s.AnonymousUserRights?.All(r => Enum.TryParse<AccessRights>(r, out var _)) ?? true,
                $"Invalid value(s) in {nameof(AlphonseSettings.AnonymousUserRights)} in Alphonse setting")
            .ValidateOnStart();
    }
}