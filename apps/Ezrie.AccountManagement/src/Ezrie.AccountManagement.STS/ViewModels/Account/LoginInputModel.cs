using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoginInputModel
{
	[Required]
	public String Username { get; set; }
	[Required]
	public String Password { get; set; }
	public Boolean RememberLogin { get; set; }
	public String ReturnUrl { get; set; }
}

