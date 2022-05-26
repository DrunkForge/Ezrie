namespace Ezrie.RelationshipManagement;

public static class Program
{
	public static void Main(String[] args) =>
		CreateHostBuilder(args).Build().Run();

	public static IHostBuilder CreateHostBuilder(String[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(options => options.UseStartup<Startup>());
}
