using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezrie.Configuration;

// Client Options
public class OpenIdConnectClientOptions
{
	public String Authority { get; set; } = String.Empty;
	public String ClientId { get; set; } = String.Empty;
	public String? ClientName { get; set; }
	public String? ClientSecret { get; set; }

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] Scopes { get; set; } = Array.Empty<String>();

	public String? MetadatAddress { get; set; }
	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String PostLogoutRedirectUri { get; set; } = "/";
	public String RemoteSignOutPath { get; set; }
	public String ResponseType { get; set; } = "code";
	public String SignedOutCallbackPath { get; set; }
	public String SignedOutRedirectUri { get; set; }

	public Boolean RequireHttpsMetadata { get; set; } = true;
	public Boolean UsePkce { get; set; }
}
