// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

namespace Ezrie.AccountManagement.STS.ViewModels.Account;

public class LoggedOutViewModel
{
	public String PostLogoutRedirectUri { get; set; }
	public String ClientName { get; set; }
	public String SignOutIframeUrl { get; set; }

	public Boolean AutomaticRedirectAfterSignOut { get; set; } = false;

	public String LogoutId { get; set; }
	public Boolean TriggerExternalSignout => ExternalAuthenticationScheme != null;
	public String ExternalAuthenticationScheme { get; set; }
}

