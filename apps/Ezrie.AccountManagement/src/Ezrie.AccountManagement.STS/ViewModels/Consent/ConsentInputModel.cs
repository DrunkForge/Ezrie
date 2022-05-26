namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ConsentInputModel
{
	public String Button { get; set; } = String.Empty;
	public IEnumerable<String> ScopesConsented { get; set; } = Array.Empty<String>();
	public Boolean RememberConsent { get; set; }
	public String? ReturnUrl { get; set; }
	public String Description { get; set; } = String.Empty;
}

