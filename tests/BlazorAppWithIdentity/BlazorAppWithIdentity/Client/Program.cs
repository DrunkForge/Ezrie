using Ezrie.Logging;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorAppWithIdentity.Client;

public class Program
{
	public static async Task Main(String[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.UseEzrieLogging<Program>();

		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		builder.Services.AddHttpClient("BlazorAppWithIdentity.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
			.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

		// Supply HttpClient instances that include access tokens when making requests to the server project
		builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorAppWithIdentity.ServerAPI"));

		builder.Services.AddApiAuthorization();

		await builder.Build().RunAsync();
	}
}
