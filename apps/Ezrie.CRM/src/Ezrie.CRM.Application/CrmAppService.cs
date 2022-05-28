using System;
using System.Collections.Generic;
using System.Text;
using Ezrie.CRM.Localization;
using Volo.Abp.Application.Services;

namespace Ezrie.CRM;

/* Inherit your application services from this class.
 */
public abstract class CrmAppService : ApplicationService
{
	protected CrmAppService()
	{
		LocalizationResource = typeof(CrmResource);
	}
}
