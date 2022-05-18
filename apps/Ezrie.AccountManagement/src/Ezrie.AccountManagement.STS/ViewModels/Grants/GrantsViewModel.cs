namespace Ezrie.AccountManagement.STS.ViewModels.Grants;

public class GrantsViewModel
{
	public IEnumerable<GrantViewModel> Grants { get; set; }
}

public class GrantViewModel
{
	public String ClientId { get; set; }
	public String ClientName { get; set; }
	public String ClientUrl { get; set; }
	public String ClientLogoUrl { get; set; }
	public String Description { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Expires { get; set; }
	public IEnumerable<String> IdentityGrantNames { get; set; }
	public IEnumerable<String> ApiGrantNames { get; set; }
}

