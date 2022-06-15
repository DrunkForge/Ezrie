using Ezrie.Configuration;
using Ezrie.Logging;
using Serilog;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;

namespace Ezrie.AccountManagement.STS;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is logged and it doesn't matter why it failed.")]
	public static async Task<Int32> Main(String[] args)
	{
		// https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		try
		{
			var host = CreateHostBuilder(args).Build();

			var configuration = host.Services.GetRequiredService<IConfiguration>();

			DockerHelpers.ApplyDockerConfiguration(configuration);

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

	private static IConfiguration GetConfiguration(String[] args)
	{
		var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		var isDevelopment = environment == Environments.Development;

		var configurationBuilder = EzrieConfiguration.CreateBuilder();

		if (isDevelopment)
		{
			configurationBuilder.AddUserSecrets<Startup>(true);
		}

		var configuration = configurationBuilder.Build();

		configuration.AddAzureKeyVaultConfiguration(configurationBuilder);

		configurationBuilder.AddCommandLine(args);
		configurationBuilder.AddEnvironmentVariables();

		return configurationBuilder.Build();
	}

	public static IHostBuilder CreateHostBuilder(String[] args) =>
		Host.CreateDefaultBuilder(args)
			.UseEzrieLogging<Startup>()
			.ConfigureAppConfiguration((hostContext, configApp) =>
			{
				var env = hostContext.HostingEnvironment;
				var configurationRoot = configApp.Build();

				configApp.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);
				configApp.AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

				if (env.IsDevelopment())
				{
					configApp.AddUserSecrets<Startup>(true);
				}

				configurationRoot.AddAzureKeyVaultConfiguration(configApp);

				configApp.AddEnvironmentVariables();
				configApp.AddCommandLine(args);
			})
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.ConfigureKestrel(options => options.AddServerHeader = false);
				webBuilder.UseStartup<Startup>();
			});
}
