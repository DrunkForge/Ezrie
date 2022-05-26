using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class ResetPasswordViewModel
{
	[Required]
	[EmailAddress]
	public String Email { get; set; } = String.Empty;

	[Required]
	[DataType(DataType.Password)]
	public String Password { get; set; } = String.Empty;

	[DataType(DataType.Password)]
	[Compare("Password")]
	public String ConfirmPassword { get; set; } = String.Empty;

	public String Code { get; set; } = String.Empty;
}

