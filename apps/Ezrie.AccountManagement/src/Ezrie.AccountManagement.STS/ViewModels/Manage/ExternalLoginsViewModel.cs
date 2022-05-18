using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Ezrie.AccountManagement.STS.ViewModels.Manage;

public class ExternalLoginsViewModel
{
	public IList<UserLoginInfo> CurrentLogins { get; set; }

	public IList<AuthenticationScheme> OtherLogins { get; set; }

	public Boolean ShowRemoveButton { get; set; }

	public String StatusMessage { get; set; }
}

