namespace Ezrie.AccountManagement.STS.Configuration;

public class ExternalProvidersConfiguration
{
	public Boolean UseGitHubProvider { get; set; }
	public String? GitHubClientId { get; set; }
	public String? GitHubClientSecret { get; set; }
	public String? GitHubCallbackPath { get; set; }

	public Boolean UseAzureAdProvider { get; set; }
	public String? AzureAdClientId { get; set; }
	public String? AzureAdSecret { get; set; }
	public String? AzureAdTenantId { get; set; }
	public String? AzureInstance { get; set; }
	public String? AzureAdCallbackPath { get; set; }
	public String? AzureDomain { get; set; }
}
