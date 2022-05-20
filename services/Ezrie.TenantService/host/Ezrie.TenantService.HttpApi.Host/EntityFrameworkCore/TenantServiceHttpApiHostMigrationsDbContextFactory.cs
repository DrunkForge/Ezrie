using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ezrie.TenantService.EntityFrameworkCore;

public class TenantServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<TenantServiceHttpApiHostMigrationsDbContext>
{
    public TenantServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TenantServiceHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("TenantService"));

        return new TenantServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
