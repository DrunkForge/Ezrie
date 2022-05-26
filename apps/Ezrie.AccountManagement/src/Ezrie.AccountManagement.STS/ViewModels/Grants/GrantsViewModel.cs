namespace Ezrie.AccountManagement.STS.ViewModels.Grants;

public class GrantsViewModel
{
	public IEnumerable<GrantViewModel> Grants { get; set; } = Array.Empty<GrantViewModel>();
}

public class GrantViewModel
{
	public String ClientId { get; set; } = String.Empty;
	public String ClientName { get; set; } = String.Empty;
	public String ClientUrl { get; set; } = String.Empty;
	public String ClientLogoUrl { get; set; } = String.Empty;
	public String Description { get; set; } = String.Empty;
	public DateTime Created { get; set; }
	public DateTime? Expires { get; set; }
	public IEnumerable<String> IdentityGrantNames { get; set; } = Array.Empty<String>();
	public IEnumerable<String> ApiGrantNames { get; set; } = Array.Empty<String>();
}
