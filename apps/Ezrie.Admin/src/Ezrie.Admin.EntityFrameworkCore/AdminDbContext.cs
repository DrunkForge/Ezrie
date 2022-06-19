using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

#nullable disable

namespace Ezrie.Admin;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName(AdminDbProperties.ConnectionStringName)]
public class AdminDbContext : AbpDbContext<AdminDbContext>, IIdentityDbContext, ITenantManagementDbContext
{
	public AdminDbContext(DbContextOptions<AdminDbContext> options)
		: base(options)
	{

	}

	//Identity
	public DbSet<IdentityUser> Users { get; set; }
	public DbSet<IdentityRole> Roles { get; set; }
	public DbSet<IdentityClaimType> ClaimTypes { get; set; }
	public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
	public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
	public DbSet<IdentityLinkUser> LinkUsers { get; set; }

	// Tenant Management
	public DbSet<Tenant> Tenants { get; set; }
	public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.CreateAdminModel();
	}
}
