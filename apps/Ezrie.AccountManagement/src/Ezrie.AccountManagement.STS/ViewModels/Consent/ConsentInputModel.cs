// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

using System.Collections.Generic;

namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ConsentInputModel
{
	public String Button { get; set; }
	public IEnumerable<String> ScopesConsented { get; set; }
	public Boolean RememberConsent { get; set; }
	public String ReturnUrl { get; set; }
	public String Description { get; set; }
}

