namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ScopeViewModel
{
	public String Value { get; set; } = String.Empty;
	public String DisplayName { get; set; } = String.Empty;
	public String Description { get; set; } = String.Empty;
	public Boolean Emphasize { get; set; }
	public Boolean Required { get; set; }
	public Boolean Checked { get; set; }
}

