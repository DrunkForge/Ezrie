
using IdentityServer4.Models;

namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ProcessConsentResult
{
	public Boolean IsRedirect => RedirectUri != null;

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't work nicely with IConfiguration")]
	public String? RedirectUri { get; set; }

	public Client? Client { get; set; }

	public Boolean ShowView => ViewModel != null;
	public ConsentViewModel? ViewModel { get; set; }

	public Boolean HasValidationError => ValidationError != null;
	public String? ValidationError { get; set; }
}

