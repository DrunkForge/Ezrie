// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

namespace Ezrie.AccountManagement.STS.ViewModels.Consent;

public class ScopeViewModel
{
	public String Value { get; set; }
	public String DisplayName { get; set; }
	public String Description { get; set; }
	public Boolean Emphasize { get; set; }
	public Boolean Required { get; set; }
	public Boolean Checked { get; set; }
}

