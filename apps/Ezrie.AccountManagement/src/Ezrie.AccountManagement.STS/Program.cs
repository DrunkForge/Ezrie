using Ezrie.Logging;
using Serilog;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;

namespace Ezrie.AccountManagement.STS;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
	public static async Task<Int32> Main(String[] args)
	{
		try
		{
			var builder = CreateHostBuilder(args);

			var host = builder.Build();

			DockerHelpers.ApplyDockerConfiguration(host.Services.GetRequiredService<IConfiguration>());

			await host.RunAsync();

			return 0;
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Host terminated unexpectedly");
			return 1;
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}

	public static IHostBuilder CreateHostBuilder(String[] args) => Host
		.CreateDefaultBuilder(args)
		.UseEzrieLogging<Startup>()
		.ConfigureAppConfiguration((hostContext, configApp) =>
		{
			var configurationRoot = configApp.Build();
			configApp.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);

			var env = hostContext.HostingEnvironment;

			configApp.AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

			if (env.IsDevelopment())
			{
				configApp.AddUserSecrets<Startup>(true);
			}

			configurationRoot.AddAzureKeyVaultConfiguration(configApp);

			configApp.AddEnvironmentVariables();
			configApp.AddCommandLine(args);
		})
		.ConfigureWebHostDefaults(webBuilder => webBuilder.ConfigureKestrel(options => options.AddServerHeader = false).UseStartup<Startup>());
}
