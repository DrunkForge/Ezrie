// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan Škoruba

namespace Ezrie.AccountManagement.STS.ViewModels.Grants;

public class GrantsViewModel
{
	public IEnumerable<GrantViewModel> Grants { get; set; }
}

public class GrantViewModel
{
	public String ClientId { get; set; }
	public String ClientName { get; set; }
	public String ClientUrl { get; set; }
	public String ClientLogoUrl { get; set; }
	public String Description { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Expires { get; set; }
	public IEnumerable<String> IdentityGrantNames { get; set; }
	public IEnumerable<String> ApiGrantNames { get; set; }
}

