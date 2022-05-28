namespace Ezrie.Configuration;

public class AuthenticationOptions
{
	public String Authority { get; set; } = String.Empty;
	public String? Audience { get; set; }
	public String ClientId { get; set; } = String.Empty;
	public String? MetadatUrl { get; set; }
	public String RedirectUri { get; set; }
	public String PostLogoutRedirectUri { get; set; }
	public String ResponseType { get; set; } = "code";
	public String ResponseMode { get; set; }

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] Scopes { get; set; } = Array.Empty<String>();
	public Boolean RequireHttpsMetadata { get; set; } = true;
}
