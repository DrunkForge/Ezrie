using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ezrie;

[Dependency(ReplaceServices = true)]
public class EzrieBrandingProvider : DefaultBrandingProvider
{
	public override String AppName => "Ezrie";
}
