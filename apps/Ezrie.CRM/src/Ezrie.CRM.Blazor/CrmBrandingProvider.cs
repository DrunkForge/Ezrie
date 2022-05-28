using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ezrie.CRM;

[Dependency(ReplaceServices = true)]
public class CrmBrandingProvider : DefaultBrandingProvider
{
	public override String AppName => "EzrieCRM";
}
