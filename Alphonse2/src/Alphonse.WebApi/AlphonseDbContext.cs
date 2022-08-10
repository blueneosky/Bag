using Alphonse.WebApi.Dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alphonse.WebApi;

public class AlphonseDbContext : DbContext
{
    // any modifications must be followed by these commands before run
    // > dotnet ef migrations add <migrationName>
    // //> dotnet ef database update

    public DbSet<CallHistoryDbo> CallHistories => Set<CallHistoryDbo>();
    public DbSet<PhoneNumberDbo> PhoneNumbers => Set<PhoneNumberDbo>();


    public AlphonseDbContext(DbContextOptions<AlphonseDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<CallHistoryDbo>()
            .Property(e => e.Timestamp)
            .HasConversion(new DateTimeOffsetToBinaryConverter());
    }
}