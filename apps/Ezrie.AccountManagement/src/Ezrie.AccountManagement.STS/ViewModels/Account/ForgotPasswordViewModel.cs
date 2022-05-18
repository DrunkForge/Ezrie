using System.ComponentModel.DataAnnotations;
using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class ForgotPasswordViewModel
{
	[Required]
	public LoginResolutionPolicy? Policy { get; set; }

	[EmailAddress]
	public String Email { get; set; }

	public String Username { get; set; }
}

