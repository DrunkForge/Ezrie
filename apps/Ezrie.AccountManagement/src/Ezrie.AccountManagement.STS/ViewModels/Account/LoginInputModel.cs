using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoginInputModel
{
	[Required]
	public String Username { get; set; } = String.Empty;
	[Required]
	public String Password { get; set; } = String.Empty;
	public Boolean RememberLogin { get; set; }
	public String ReturnUrl { get; set; } = String.Empty;
}

