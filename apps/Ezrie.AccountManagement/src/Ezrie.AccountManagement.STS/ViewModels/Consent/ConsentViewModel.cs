namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ConsentViewModel : ConsentInputModel
{
	public String ClientName { get; set; }
	public String ClientUrl { get; set; }
	public String ClientLogoUrl { get; set; }
	public Boolean AllowRememberConsent { get; set; }

	public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
	public IEnumerable<ScopeViewModel> ApiScopes { get; set; }
}

