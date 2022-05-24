using System;

namespace CPCA;

/// <summary>
/// Marker class for DI/IoC
/// </summary>
public class CpcaCmsModule : CpcaModule<CpcaCmsModule>
{
	public override String Endpoint => CpcaConsts.Endpoints.CMS;
}
