#nullable disable

namespace Ezrie.AccountManagement.STS.Configuration;

[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't work nicely with IConfiguration")]
public class AdminConfiguration
{
	public String PageTitle { get; set; }

	public String HomePageLogoUri { get; set; }
	public String FaviconUri { get; set; }
	public String IdentityAdminBaseUrl { get; set; }
	public String AdministrationRole { get; set; }

	public String Theme { get; set; }

	public String CustomThemeCss { get; set; }
}

