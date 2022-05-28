using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoginWithRecoveryCodeViewModel
{
	[Required]
	[DataType(DataType.Text)]
	public String RecoveryCode { get; set; }

	public String ReturnUrl { get; set; }
}

