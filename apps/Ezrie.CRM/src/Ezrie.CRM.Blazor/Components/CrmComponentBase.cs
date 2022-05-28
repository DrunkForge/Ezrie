using Ezrie.CRM.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ezrie.CRM.Components;

public abstract class CrmComponentBase : AbpComponentBase
{
	protected CrmComponentBase()
	{
		LocalizationResource = typeof(CrmResource);
	}
}
