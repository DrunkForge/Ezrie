using Ezrie.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ezrie.CRM;

internal static class Program
{
	public static async Task Main(String[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.UseEzrieLogging<CrmBlazorModule>();

		var application = await builder.AddApplicationAsync<CrmBlazorModule>(options =>
		{
			options.UseAutofac();
		});

		var host = builder.Build();

		await application.InitializeApplicationAsync(host.Services);

		await host.RunAsync();
	}
}
