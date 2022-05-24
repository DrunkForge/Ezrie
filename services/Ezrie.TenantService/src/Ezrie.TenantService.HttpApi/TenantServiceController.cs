using Ezrie.TenantService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ezrie.TenantService;

public abstract class TenantServiceController : AbpControllerBase
{
	protected TenantServiceController()
	{
		LocalizationResource = typeof(TenantServiceResource);
	}
}
