// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

using System;
using System.Collections.Generic;
using System.Linq;
using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoginViewModel : LoginInputModel
{
	public Boolean AllowRememberLogin { get; set; } = true;
	public Boolean EnableLocalLogin { get; set; } = true;
	public LoginResolutionPolicy LoginResolutionPolicy { get; set; } = LoginResolutionPolicy.Username;

	public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
	public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

	public Boolean IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
	public String ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
}

