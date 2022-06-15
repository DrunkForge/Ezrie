using Ezrie.CRM.EntityFrameworkCore.Migrations;
using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.Logging;
using Serilog;

namespace Ezrie.CRM;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is logged and it doesn't matter why it failed.")]
	public async static Task<Int32> Main(String[] args)
	{
		try
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.AddAppSettingsSecretsJson()
				.UseAutofac()
				.UseEzrieLogging<CRMHttpApiHostModule>();

			await builder.AddApplicationAsync<CRMHttpApiHostModule>();

			var app = builder.Build();

			await app.InitializeApplicationAsync();

			await app.MigrateAsync();

			await app.RunAsync();

			return 0;
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Host terminated unexpectedly!");
			return 1;
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}

	private static async Task MigrateAsync(this IHost host)
	{
		var configuration = host.Services.GetRequiredService<IConfiguration>();
		var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<CRMHttpApiHostModule>();

		await new MigrationHost<CRMEntityFrameworkCoreMigrationsModule>(logger).MigrateAndSeedAsync();
	}
}
