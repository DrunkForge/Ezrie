using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

#nullable disable

namespace Ezrie.TenantService.EntityFrameworkCore;

[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName(TenantServiceDbProperties.ConnectionStringName)]
public class TenantServiceDbContext : AbpDbContext<TenantServiceDbContext>,
	ITenantServiceDbContext,
	ITenantManagementDbContext
{
	/* Add DbSet for each Aggregate Root here. Example:
	 * public DbSet<Question> Questions { get; set; }
	 */

	public TenantServiceDbContext(DbContextOptions<TenantServiceDbContext> options)
		: base(options)
	{

	}

	// TenantManagement
	public DbSet<Tenant> Tenants { get; set; }
	public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.CreateTenantServiceModel();
	}
}
