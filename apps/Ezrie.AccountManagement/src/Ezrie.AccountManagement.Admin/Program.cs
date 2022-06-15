using Serilog;
using Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.Configuration;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;
using Ezrie.Logging;
using Ezrie.AccountManagement.Identity;
using Ezrie.AccountManagement.EntityFrameworkCore;
using Ezrie.Configuration;

namespace Ezrie.AccountManagement.Admin;

internal static class Program
{
	private const String SeedArgs = "/seed";
	private const String MigrateOnlyArgs = "/migrateonly";

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

			var migrationComplete = await ApplyDbMigrationsWithDataSeedAsync(args, configuration, host);

			if (args.Any(x => x == MigrateOnlyArgs))
			{
				await host.StopAsync();
				if (!migrationComplete)
				{
					Environment.ExitCode = -1;
				}

				return 0;
			}

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

	private static async Task<Boolean> ApplyDbMigrationsWithDataSeedAsync(String[] args, IConfiguration configuration, IHost host)
	{
		Log.Information("Applying DB Migrations");
		var applyDbMigrationWithDataSeedFromProgramArguments = args.Any(x => x == SeedArgs);
		if (applyDbMigrationWithDataSeedFromProgramArguments)
			args = args.Except(new[] { SeedArgs }).ToArray();

		var seedConfiguration = configuration.GetOptions<SeedConfiguration>();
		var databaseMigrationsConfiguration = configuration.GetOptions<DatabaseMigrationsConfiguration>();

		return await DbMigrationHelpers
			.ApplyDbMigrationsWithDataSeedAsync<IdentityServerConfigurationDbContext, AdminIdentityDbContext,
				IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext,
				IdentityServerDataProtectionDbContext, UserIdentity, UserIdentityRole>(host,
				applyDbMigrationWithDataSeedFromProgramArguments, seedConfiguration, databaseMigrationsConfiguration);
	}

	public static IHostBuilder CreateHostBuilder(String[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((hostContext, configApp) =>
			{
				var configurationRoot = configApp.Build();

				configApp.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);
				configApp.AddJsonFile("identitydata.json", optional: true, reloadOnChange: true);
				configApp.AddJsonFile("identityserverdata.json", optional: true, reloadOnChange: true);

				var env = hostContext.HostingEnvironment;

				configApp.AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
				configApp.AddJsonFile($"identitydata.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
				configApp.AddJsonFile($"identityserverdata.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

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
			})
			.UseEzrieLogging<Startup>();
}
