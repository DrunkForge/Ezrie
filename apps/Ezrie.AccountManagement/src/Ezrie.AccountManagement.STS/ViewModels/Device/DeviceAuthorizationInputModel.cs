
using Ezrie.AccountManagement.STS.ViewModels.Consent;

namespace Ezrie.AccountManagement.STS.ViewModels.Device;

public class DeviceAuthorizationInputModel : ConsentInputModel
{
	public String UserCode { get; set; } = String.Empty;
}

