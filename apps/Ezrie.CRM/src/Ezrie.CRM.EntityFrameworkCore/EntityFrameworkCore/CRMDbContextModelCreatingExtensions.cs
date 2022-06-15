using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Ezrie.CRM.EntityFrameworkCore;

public static class CRMDbContextModelCreatingExtensions
{
    public static void ConfigureCRM(this ModelBuilder modelBuilder)
	{

	}

    public static void CreateCrmDbContextModel(this ModelBuilder modelBuilder)
    {
        Check.NotNull(modelBuilder, nameof(modelBuilder));

		modelBuilder.ConfigureCRM();
		modelBuilder.ConfigureAuditLogging();
		modelBuilder.ConfigureFeatureManagement();
		modelBuilder.ConfigureIdentity();
		modelBuilder.ConfigureIdentityServer();
		modelBuilder.ConfigurePermissionManagement();
		modelBuilder.ConfigureSettingManagement();
		modelBuilder.ConfigureTenantManagement();
	}
}
