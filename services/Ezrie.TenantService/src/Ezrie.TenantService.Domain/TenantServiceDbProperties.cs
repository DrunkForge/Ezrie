namespace Ezrie.TenantService;

public static class TenantServiceDbProperties
{
	public const String ConnectionStringName = "TenantService";
	public static String? DbSchema { get; set; }
	public static String DbTablePrefix { get; set; } = "TenantService";
}
