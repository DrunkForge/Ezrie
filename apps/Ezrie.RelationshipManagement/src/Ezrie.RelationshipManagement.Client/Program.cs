﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ezrie.RelationshipManagement.Services;

namespace Ezrie.RelationshipManagement
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.Services.AddOptions();
			builder.Services.AddAuthorizationCore();
			builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
			builder.Services.TryAddSingleton(sp => (HostAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
			builder.Services.AddTransient<AuthorizedHandler>();

			builder.RootComponents.Add<App>("#app");

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

			await builder.Build().RunAsync();
		}
	}
}