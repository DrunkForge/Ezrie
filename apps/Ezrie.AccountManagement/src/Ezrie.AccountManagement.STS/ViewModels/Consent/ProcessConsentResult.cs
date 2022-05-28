// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan �koruba

using IdentityServer4.Models;

namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ProcessConsentResult
{
	public Boolean IsRedirect => RedirectUri != null;
	public String RedirectUri { get; set; }
	public Client Client { get; set; }

	public Boolean ShowView => ViewModel != null;
	public ConsentViewModel ViewModel { get; set; }

	public Boolean HasValidationError => ValidationError != null;
	public String ValidationError { get; set; }
}

