using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Ezrie.AccountManagement.STS.ViewModels.Manage;

public class ExternalLoginsViewModel
{
	public IEnumerable<UserLoginInfo> CurrentLogins { get; set; } = Array.Empty<UserLoginInfo>();

	public IEnumerable<AuthenticationScheme> OtherLogins { get; set; } = Array.Empty<AuthenticationScheme>();

	public Boolean ShowRemoveButton { get; set; }

	public String StatusMessage { get; set; } = String.Empty;
}

