// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

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

