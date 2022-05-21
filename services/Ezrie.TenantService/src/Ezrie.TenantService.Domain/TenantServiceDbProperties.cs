namespace Ezrie.TenantService;

public static class TenantServiceDbProperties
{
    public static String DbTablePrefix { get; set; } = "TenantService";

    public static String DbSchema { get; set; } = null;

    public const String ConnectionStringName = "TenantService";
}
