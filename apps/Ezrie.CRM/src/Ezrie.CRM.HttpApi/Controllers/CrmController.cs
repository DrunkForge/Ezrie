using Ezrie.CRM.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ezrie.CRM.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CrmController : AbpControllerBase
{
	protected CrmController()
	{
		LocalizationResource = typeof(CrmResource);
	}
}
