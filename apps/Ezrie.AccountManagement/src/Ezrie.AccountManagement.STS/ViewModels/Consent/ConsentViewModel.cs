namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ConsentViewModel : ConsentInputModel
{
	public String ClientName { get; set; } = String.Empty;
	public String ClientUrl { get; set; } = String.Empty;
	public String ClientLogoUrl { get; set; } = String.Empty;
	public Boolean AllowRememberConsent { get; set; }

	public IEnumerable<ScopeViewModel> IdentityScopes { get; set; } = Array.Empty<ScopeViewModel>();
	public IEnumerable<ScopeViewModel> ApiScopes { get; set; } = Array.Empty<ScopeViewModel>();
}

