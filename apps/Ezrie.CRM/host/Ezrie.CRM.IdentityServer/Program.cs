using System;
using System.Threading.Tasks;
using Ezrie.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Ezrie.CRM;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is logged and it doesn't matter why it failed.")]
	public async static Task<Int32> Main(String[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseEzrieLogging<CRMIdentityServerModule>();

            await builder.AddApplicationAsync<CRMIdentityServerModule>();

            var app = builder.Build();

            await app.InitializeApplicationAsync();

            await app.RunAsync();

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
