using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ezrie.RelationshipManagement.Services;
using Microsoft.AspNetCore.Components.Web;
using IdentityModel;

namespace Ezrie.RelationshipManagement;

internal static class Program
{
	public static async Task Main(String[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.Services.AddOptions();
		builder.Services.AddAuthorizationCore();
		builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
		builder.Services.TryAddSingleton(sp => (HostAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
		builder.Services.AddTransient<AuthorizedHandler>();

		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		builder.Services.AddHttpClient("default", client =>
		{
			client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		});

		builder.Services.AddHttpClient("authorizedClient", client =>
		{
			client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}).AddHttpMessageHandler<AuthorizedHandler>();

		builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("default"));

		builder.Services.AddOidcAuthentication(options =>
		{
			options.ProviderOptions.Authority = "https://localhost:5500/";
			options.ProviderOptions.ClientId = "ezrie_relationship_management_blazor";
			options.ProviderOptions.ResponseType = "code";

			options.UserOptions.NameClaim = JwtClaimTypes.Name;
			options.UserOptions.RoleClaim = JwtClaimTypes.Role;

			options.ProviderOptions.DefaultScopes.Add("roles");
			options.ProviderOptions.DefaultScopes.Add("email");
			options.ProviderOptions.DefaultScopes.Add("phone");
		});

		await builder.Build().RunAsync();
	}
}
