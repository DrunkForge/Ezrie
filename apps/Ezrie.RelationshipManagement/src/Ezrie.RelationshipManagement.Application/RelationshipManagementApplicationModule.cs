using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Ezrie.RelationshipManagement;

[DependsOn(typeof(RelationshipManagementDomainModule))]
[DependsOn(typeof(RelationshipManagementApplicationContractsModule))]
[DependsOn(typeof(AbpAccountApplicationModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
[DependsOn(typeof(AbpIdentityApplicationModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
[DependsOn(typeof(AbpTenantManagementApplicationModule))]
public class RelationshipManagementApplicationModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddMaps<RelationshipManagementApplicationModule>();
		});
	}
}
