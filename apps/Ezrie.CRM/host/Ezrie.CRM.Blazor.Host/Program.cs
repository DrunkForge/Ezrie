using Ezrie.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ezrie.CRM.Blazor.Host;

internal static class Program
{
    public async static Task Main(String[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.UseEzrieLogging<CRMBlazorHostModule>();

        var application = await builder.AddApplicationAsync<CRMBlazorHostModule>(options =>
        {
            options.UseAutofac();
        });

        var host = builder.Build();

        await application.InitializeApplicationAsync(host.Services);

        await host.RunAsync();
    }

}
