using Ezrie.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ezrie.Admin.Blazor;

internal static class Program
{
    public async static Task Main(String[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.UseEzrieLogging<AdminBlazorModule>();

        var application = await builder.AddApplicationAsync<AdminBlazorModule>(options =>
        {
            options.UseAutofac();
        });

        var host = builder.Build();

        await application.InitializeApplicationAsync(host.Services);

        await host.RunAsync();
    }
}
