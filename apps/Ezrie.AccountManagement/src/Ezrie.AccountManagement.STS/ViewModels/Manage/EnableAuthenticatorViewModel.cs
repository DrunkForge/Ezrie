using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ezrie.AccountManagement.STS.ViewModels.Manage;

public class EnableAuthenticatorViewModel
{
	[Required]
	[StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Text)]
	[Display(Name = "Verification Code")]
	public String Code { get; set; } = String.Empty;

	[BindNever]
	public String SharedKey { get; set; } = String.Empty;

	[BindNever]
	public String AuthenticatorUri { get; set; } = String.Empty;
}

