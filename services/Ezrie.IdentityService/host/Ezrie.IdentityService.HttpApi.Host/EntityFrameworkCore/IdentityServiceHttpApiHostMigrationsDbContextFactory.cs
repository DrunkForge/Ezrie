using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ezrie.IdentityService.EntityFrameworkCore;

public class IdentityServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityServiceHttpApiHostMigrationsDbContext>
{
    public IdentityServiceHttpApiHostMigrationsDbContext CreateDbContext(String[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IdentityServiceHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("IdentityService"));

        return new IdentityServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
