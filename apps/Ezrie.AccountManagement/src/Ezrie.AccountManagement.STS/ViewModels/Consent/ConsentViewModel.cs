// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ConsentViewModel : ConsentInputModel
{
	public String ClientName { get; set; }
	public String ClientUrl { get; set; }
	public String ClientLogoUrl { get; set; }
	public Boolean AllowRememberConsent { get; set; }

	public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
	public IEnumerable<ScopeViewModel> ApiScopes { get; set; }
}

