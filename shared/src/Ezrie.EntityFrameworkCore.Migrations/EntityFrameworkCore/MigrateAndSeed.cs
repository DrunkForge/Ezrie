using Ezrie.Configuration;
using Ezrie.Logging;
using Ezrie.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Ezrie.EntityFrameworkCore;

public static class MigrateAndSeed
{
	public static async Task ExecuteAsync<T>(CancellationToken cancellationToken = default)
	where T : IAbpModule
	{
		using (var application = await AbpApplicationFactory.CreateAsync<T>(options =>
		{
			var configuration = EzrieConfiguration
				.CreateBuilder()
				.AddJsonFile($"appsettings.{typeof(T).Name}.json")
				.Build();

			options.Services.ReplaceConfiguration(configuration);
			options.UseAutofac();
			options.Services.AddEzrieLogging<T>();
		}))
		{
			await application.InitializeAsync();

			var services = application.ServiceProvider.GetServices<IMigrateAndSeedService>();
			foreach (var service in services)
			{
				await service.MigrateAndSeedAsync(cancellationToken);
			}

			await application.ShutdownAsync();
		}
	}
}
