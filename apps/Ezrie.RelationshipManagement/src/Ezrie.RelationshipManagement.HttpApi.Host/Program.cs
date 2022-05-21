using System;
using System.Threading.Tasks;
using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.Hosting.AspNetCore;
using Ezrie.Logging;
using Ezrie.RelationshipManagement.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Ezrie.RelationshipManagement;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
	public async static Task<Int32> Main(String[] args)
	{
		try
		{
			var builder = CreateHostBuilder(args);

			var app = builder.Build();

			await app.MigrateAsync();

			await app.RunAsync().ConfigureAwait(false);

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

	public static IHostBuilder CreateHostBuilder(String[] args) => Host
		.CreateDefaultBuilder(args)
		.UseAutofac()
		.AddAppSettingsSecretsJson()
		.UseEzrieLogging<RelationshipManagementHttpApiHostModule>()
		.ConfigureWebHostDefaults(webBuilder =>
		{
			webBuilder.ConfigureKestrel(options => options.AddServerHeader = false);
			webBuilder.UseStartup<Startup<RelationshipManagementHttpApiHostModule>>();
		});

	private static async Task MigrateAsync(this IHost host)
	{
		var configuration = host.Services.GetRequiredService<IConfiguration>();
		var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<RelationshipManagementHttpApiHostModule>();

		await new MigrationHost<RelationshipManagementEntityFrameworkCoreMigrationsModule>(logger).MigrateAndSeedAsync();
	}
}
