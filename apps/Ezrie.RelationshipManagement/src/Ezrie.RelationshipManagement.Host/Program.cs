namespace Ezrie.RelationshipManagement;

internal static class Program
{
	public static void Main(String[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(String[] args) =>
		 Host.CreateDefaultBuilder(args)
			 .ConfigureWebHostDefaults(webBuilder =>
			 {
				 webBuilder
					 .ConfigureKestrel(options => options.AddServerHeader = false)
					 .UseStartup<Startup>();
			 });
}
