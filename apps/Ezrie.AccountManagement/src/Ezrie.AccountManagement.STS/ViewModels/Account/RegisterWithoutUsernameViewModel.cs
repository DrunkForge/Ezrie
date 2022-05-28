﻿using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class RegisterWithoutUsernameViewModel
{
	[Required]
	[EmailAddress]
	public String Email { get; set; }

	[Required]
	[DataType(DataType.Password)]
	public String Password { get; set; }

	[DataType(DataType.Password)]
	[Compare("Password")]
	public String ConfirmPassword { get; set; }
}

