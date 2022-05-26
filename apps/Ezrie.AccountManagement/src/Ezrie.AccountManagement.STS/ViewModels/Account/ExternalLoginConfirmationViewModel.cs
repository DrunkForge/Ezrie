using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class ExternalLoginConfirmationViewModel
{
	[Required]
	[RegularExpression(@"^[a-zA-Z0-9_@\-\.\+]+$")]
	public String UserName { get; set; } = String.Empty;

	[Required]
	[EmailAddress]
	public String Email { get; set; } = String.Empty;
}

