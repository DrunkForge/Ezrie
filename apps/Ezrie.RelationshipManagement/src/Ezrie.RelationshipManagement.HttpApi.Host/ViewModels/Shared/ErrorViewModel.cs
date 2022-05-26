using System.ComponentModel.DataAnnotations;

namespace Ezrie.RelationshipManagement.ViewModels.Shared;

public class ErrorViewModel
{
	[Display(Name = "Error")]
	public String Error { get; set; } = String.Empty;

	[Display(Name = "Description")]
	public String ErrorDescription { get; set; } = String.Empty;
}
