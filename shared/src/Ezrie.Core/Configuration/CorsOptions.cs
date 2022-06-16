namespace Ezrie.Configuration;
public class CorsOptions
{
	public Boolean AllowAnyOrigin { get; set; }
	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] AllowOrigins { get; set; } = Array.Empty<String>();
}
