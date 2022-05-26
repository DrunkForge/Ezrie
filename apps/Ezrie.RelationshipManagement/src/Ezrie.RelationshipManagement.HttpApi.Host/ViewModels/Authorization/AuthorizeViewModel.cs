using System.ComponentModel.DataAnnotations;

namespace Ezrie.RelationshipManagement.ViewModels.Authorization;

public class AuthorizeViewModel
{
	[Display(Name = "Application")]
	public String ApplicationName { get; set; } = String.Empty;

	[Display(Name = "Scope")]
	public String Scope { get; set; } = String.Empty;
}
