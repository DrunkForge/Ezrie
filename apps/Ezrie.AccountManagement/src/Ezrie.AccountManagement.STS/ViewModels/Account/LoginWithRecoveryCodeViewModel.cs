using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoginWithRecoveryCodeViewModel
{
	[Required]
	[DataType(DataType.Text)]
	public String RecoveryCode { get; set; } = String.Empty;

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't work nicely with IConfiguration")]
	public String? ReturnUrl { get; set; }
}
