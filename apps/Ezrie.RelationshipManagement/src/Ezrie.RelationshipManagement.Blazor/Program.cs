using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ezrie.RelationshipManagement.Blazor;

public static class Program
{
	public async static Task Main(String[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		var application = await builder.AddApplicationAsync<RelationshipManagementBlazorModule>(options =>
		{
			options.UseAutofac();
		});

		var host = builder.Build();

		await application.InitializeApplicationAsync(host.Services);

		await host.RunAsync();
	}
}
