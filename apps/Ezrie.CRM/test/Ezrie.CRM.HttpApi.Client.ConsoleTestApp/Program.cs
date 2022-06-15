using Ezrie.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Ezrie.CRM.HttpApi.Client.ConsoleTestApp;

class Program
{
	static async Task Main(System.String[] args)
	{
		await CreateHostBuilder(args).UseEzrieLogging<CRMConsoleApiClientModule>().RunConsoleAsync();
	}

	public static IHostBuilder CreateHostBuilder(System.String[] args) =>
		Host.CreateDefaultBuilder(args)
			.AddAppSettingsSecretsJson()
			.ConfigureServices((hostContext, services) =>
			{
				services.AddHostedService<ConsoleTestAppHostedService>();
			});
}
