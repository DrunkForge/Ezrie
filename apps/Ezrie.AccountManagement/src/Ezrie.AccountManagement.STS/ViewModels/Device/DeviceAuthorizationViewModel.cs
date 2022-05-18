
using Ezrie.AccountManagement.STS.ViewModels.Consent;

namespace Ezrie.AccountManagement.STS.ViewModels.Device;

public class DeviceAuthorizationViewModel : ConsentViewModel
{
	public String UserCode { get; set; }
	public Boolean ConfirmUserCode { get; set; }
}

