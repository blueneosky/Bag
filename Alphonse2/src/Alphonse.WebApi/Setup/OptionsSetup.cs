using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphonse.WebApi.Setup;

public static class OptionsSetup
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<AlphonseSettings>()
            .Bind(builder.Configuration.GetSection("Alphonse"))
            .Validate(s => !string.IsNullOrWhiteSpace(s.DataDirPath), $"Missing {nameof(AlphonseSettings.DataDirPath)} in Alphonse settings")
            .Validate(s => !string.IsNullOrWhiteSpace(s.DbPath), $"Missing {nameof(AlphonseSettings.DbPath)} in Alphonse settings")
            .ValidateOnStart();
    }

    private static void Configure(AlphonseSettings settings)
    {

    }
}