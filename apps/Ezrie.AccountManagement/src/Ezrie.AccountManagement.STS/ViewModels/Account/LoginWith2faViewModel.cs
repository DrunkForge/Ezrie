using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoginWith2faViewModel
{
	[Required]
	[StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Text)]
	public String TwoFactorCode { get; set; }
	public Boolean RememberMachine { get; set; }
	public Boolean RememberMe { get; set; }
	public String ReturnUrl { get; set; }
}

