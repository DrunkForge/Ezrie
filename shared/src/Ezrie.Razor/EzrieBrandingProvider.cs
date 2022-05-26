using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Ui.Branding;

namespace Ezrie;

public class EzrieBrandingProvider : DefaultBrandingProvider
{
	public override String AppName => "EzrieCRM";
}
