using Ezrie.Admin.Migrations;
using Ezrie.Logging;
using Serilog;

namespace Ezrie.Admin;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is logged and it doesn't matter why it failed.")]
	public async static Task<Int32> Main(String[] args)
	{
		try
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host
				.AddAppSettingsSecretsJson()
				.UseAutofac()
				.ConfigureLogging(b => b.ClearProviders())
				.UseEzrieLogging<AdminIdentityServerModule>();

			await builder.AddApplicationAsync<AdminIdentityServerModule>();

			var app = builder.Build();


			await app.InitializeApplicationAsync();

			await app.MigrateAsync();

			await app.RunAsync();

			return 0;
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Ezrie.Admin.IdentityServer terminated unexpectedly!");
			return 1;
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}

	private static async Task MigrateAsync(this IHost host)
	{
		var service = host.Services.GetRequiredService<IAdminMigrateAndSeedService>();

		await service.MigrateAndSeedAsync();
	}
}
