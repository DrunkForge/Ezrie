namespace Ezrie.IdentityService;

public static class IdentityServiceDbProperties
{
	public const String ConnectionStringName = "IdentityService";
	public static String? DbSchema { get; set; }
	public static String DbTablePrefix { get; set; } = "IdentityService";
}
