using Ezrie.IdentityService.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Modularity;

namespace Ezrie.IdentityService;

[DependsOn(typeof(AbpIdentityAspNetCoreModule))]
public class IdentityServiceAspNetCoreModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		PreConfigure<IdentityBuilder>(identityBuilder => { identityBuilder.AddSignInManager<EzrieSignInManager>(); });
	}
}
