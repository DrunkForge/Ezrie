using Ezrie.TenantService.Localization;
using Volo.Abp.Application.Services;

namespace Ezrie.TenantService;

public abstract class TenantServiceAppService : ApplicationService
{
	protected TenantServiceAppService()
	{
		LocalizationResource = typeof(TenantServiceResource);
		ObjectMapperContext = typeof(TenantServiceApplicationModule);
	}
}
