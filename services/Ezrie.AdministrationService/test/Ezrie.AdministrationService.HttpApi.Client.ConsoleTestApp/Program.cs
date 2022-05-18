using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ezrie.AdministrationService.HttpApi.Client.ConsoleTestApp;

class Program
{
	static async Task Main(System.String[] args)
	{
		await CreateHostBuilder(args).RunConsoleAsync();
	}

	public static IHostBuilder CreateHostBuilder(System.String[] args) =>
		Host.CreateDefaultBuilder(args)
			.AddAppSettingsSecretsJson()
			.ConfigureServices((hostContext, services) =>
			{
				services.AddHostedService<ConsoleTestAppHostedService>();
			});
}
