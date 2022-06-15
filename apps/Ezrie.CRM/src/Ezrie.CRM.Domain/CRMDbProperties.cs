namespace Ezrie.CRM;

public static class CRMDbProperties
{
    public const String ConnectionStringName = "CRM";
    public static String? DbSchema { get; set; }
    public static String DbTablePrefix { get; set; } = "CRM";
}
