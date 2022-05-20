using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ezrie.TenantService.EntityFrameworkCore;

public class TenantServiceHttpApiHostMigrationsDbContext : AbpDbContext<TenantServiceHttpApiHostMigrationsDbContext>
{
    public TenantServiceHttpApiHostMigrationsDbContext(DbContextOptions<TenantServiceHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureTenantService();
    }
}
