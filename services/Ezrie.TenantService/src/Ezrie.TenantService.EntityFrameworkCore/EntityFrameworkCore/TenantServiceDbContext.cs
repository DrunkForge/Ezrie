using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Ezrie.TenantService.EntityFrameworkCore;

[ConnectionStringName(TenantServiceDbProperties.ConnectionStringName)]
public class TenantServiceDbContext : AbpDbContext<TenantServiceDbContext>, ITenantServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public TenantServiceDbContext(DbContextOptions<TenantServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTenantService();
    }
}
