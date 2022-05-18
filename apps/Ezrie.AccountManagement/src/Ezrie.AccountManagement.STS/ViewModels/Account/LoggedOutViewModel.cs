namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoggedOutViewModel
{
	public String PostLogoutRedirectUri { get; set; }
	public String ClientName { get; set; }
	public String SignOutIframeUrl { get; set; }

	public Boolean AutomaticRedirectAfterSignOut { get; set; } = false;

	public String LogoutId { get; set; }
	public Boolean TriggerExternalSignout => ExternalAuthenticationScheme != null;
	public String ExternalAuthenticationScheme { get; set; }
}

