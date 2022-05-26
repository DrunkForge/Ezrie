using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ezrie.IdentityService;

public static class Program
{
	public async static Task Main(String[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		var application = await builder.AddApplicationAsync<IdentityServiceBlazorHostModule>(options =>
		{
			options.UseAutofac();
		});

		var host = builder.Build();

		await application.InitializeApplicationAsync(host.Services);

		await host.RunAsync();
	}
}
