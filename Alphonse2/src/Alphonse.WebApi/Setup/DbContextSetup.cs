using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Alphonse.WebApi.Setup;

public static class DbContextSetup
{
    public static void ConfigureDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AlphonseDbContext>(SetOptions);

        //====================================================================

        void SetOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            var alphonseSettings = serviceProvider.GetService<IOptions<AlphonseSettings>>()!.Value;

            var dbPath = alphonseSettings.DbPath;
            if (!Path.IsPathRooted(dbPath))
                dbPath = Path.Join(alphonseSettings.DataBasePath, dbPath);

            dbContextOptionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}