/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Doug Wilson (info@dougwilson.ca)
* 
* This program is free software: you can redistribute it and/or modify it under the terms of
* the GNU Affero General Public License as published by the Free Software Foundation, either
* version 3 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License along with this
* program. If not, see <https://www.gnu.org/licenses/>.
*********************************************************************************************/

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore;

public class IdentityServerConfigurationDbContext : ConfigurationDbContext<IdentityServerConfigurationDbContext>, IAdminConfigurationDbContext
{
	public IdentityServerConfigurationDbContext(DbContextOptions<IdentityServerConfigurationDbContext> options, ConfigurationStoreOptions storeOptions)
		: base(options, storeOptions)
	{
	}

	public DbSet<ApiResourceProperty> ApiResourceProperties { get; set; }

	public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

	public DbSet<ApiResourceSecret> ApiSecrets { get; set; }

	public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

	public DbSet<IdentityResourceClaim> IdentityClaims { get; set; }

	public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

	public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

	public DbSet<ClientScope> ClientScopes { get; set; }

	public DbSet<ClientSecret> ClientSecrets { get; set; }

	public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

	public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

	public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

	public DbSet<ClientClaim> ClientClaims { get; set; }

	public DbSet<ClientProperty> ClientProperties { get; set; }

	public DbSet<ApiScopeProperty> ApiScopeProperties { get; set; }

	public DbSet<ApiResourceScope> ApiResourceScopes { get; set; }
}

