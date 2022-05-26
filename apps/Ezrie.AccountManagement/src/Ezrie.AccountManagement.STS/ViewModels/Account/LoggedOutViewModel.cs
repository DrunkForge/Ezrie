namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoggedOutViewModel
{
	public String PostLogoutRedirectUri { get; set; } = String.Empty;
	public String ClientName { get; set; } = String.Empty;
	public String SignOutIframeUrl { get; set; } = String.Empty;

	public Boolean AutomaticRedirectAfterSignOut { get; set; }

	public String LogoutId { get; set; } = String.Empty;
	public Boolean TriggerExternalSignout => ExternalAuthenticationScheme != null;
	public String ExternalAuthenticationScheme { get; set; } = String.Empty;
}

