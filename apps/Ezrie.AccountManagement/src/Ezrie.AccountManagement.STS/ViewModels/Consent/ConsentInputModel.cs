namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ConsentInputModel
{
	public String Button { get; set; }
	public IEnumerable<String> ScopesConsented { get; set; }
	public Boolean RememberConsent { get; set; }
	public String ReturnUrl { get; set; }
	public String Description { get; set; }
}

