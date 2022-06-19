using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezrie.Configuration;

public class IdentityServerSeed
{
	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public IdentityResourceSeed[] IdentityResources { get; set; } = Array.Empty<IdentityResourceSeed>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public ApiResourceSeed[] ApiResources { get; set; } = Array.Empty<ApiResourceSeed>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public ApiScopeSeed[] ApiScopes { get; set; } = Array.Empty<ApiScopeSeed>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public ClientSeed[] Clients { get; set; } = Array.Empty<ClientSeed>();
}

public class ApiResourceSeed
{
	public String Name { get; set; } = String.Empty;
	public String? DisplayName { get; set; }

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] Scopes { get; set; } = Array.Empty<String>();
}

public class ApiScopeSeed
{
	public String Name { get; set; } = String.Empty;
	public String? DisplayName { get; set; }
	public Boolean Required { get; set; }

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] UserClaims { get; set; } = Array.Empty<String>();
}

public class ClientSeed
{
	public String ClientId { get; set; } = String.Empty;
	public String? ClientName { get; set; }
	public String? Description { get; set; }

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String ClientUri { get; set; } = String.Empty;

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String? BackChannelLogoutUri { get; set; }

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String? FrontChannelLogoutUri { get; set; }
	public Boolean RequireClientSecret { get; set; }
	public Boolean RequirePkce { get; set; }

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] AllowedGrantTypes { get; set; } = Array.Empty<String>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] AllowedScopes { get; set; } = Array.Empty<String>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] AllowedCorsOrigins { get; set; } = Array.Empty<String>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public ClientSecretSeed[] ClientSecrets { get; set; } = Array.Empty<ClientSecretSeed>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] Permissions { get; set; } = Array.Empty<String>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] PostLogoutRedirectUris { get; set; } = Array.Empty<String>();

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] RedirectUris { get; set; } = Array.Empty<String>();
}

public class ClientSecretSeed
{
	public String? Description { get; set; }
	public String Value { get; set; } = String.Empty;
}

public class IdentityResourceSeed
{
	public String Name { get; set; } = String.Empty;
	public String? DisplayName { get; set; }
	public String? Description { get; set; }
	public Boolean Enabled { get; set; }
	public Boolean Required { get; set; }
	public Boolean? Emphasize { get; set; }

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] UserClaims { get; set; } = Array.Empty<String>();
}
