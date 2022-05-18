/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Doug Wilson (info@dougwilson.ca)
* 
* This program is free software: you can redistribute it and/or modify it under the terms of
* the GNU Affero General Public License as published by the Free Software Foundation, either
* version 3 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License along with this
* program. If not, see <https://www.gnu.org/licenses/>.
*********************************************************************************************/

using Ezrie.EntityFrameworkCore.Seeding;
using Ezrie.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Ezrie.EntityFrameworkCore.Migrations;

public class DbMigratorHostedService<T> : BackgroundService
	where T : IAbpModule
{
	private readonly IHostedServiceMonitor _hostedServiceMonitor;
	private readonly IConfiguration _configuration;
	private readonly ILogger<DbMigratorHostedService<T>> _logger;

	public DbMigratorHostedService(IHostedServiceMonitor hostedServiceMonitor, IConfiguration configuration, ILogger<DbMigratorHostedService<T>> logger)
	{
		_hostedServiceMonitor = hostedServiceMonitor;
		_configuration = configuration;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		try
		{
			_hostedServiceMonitor.ServiceStarted();

			using (var application = await AbpApplicationFactory.CreateAsync<T>(options =>
			{
				options.Services.ReplaceConfiguration(_configuration);
				options.UseAutofac();
			}))
			{
				try
				{
					await application.InitializeAsync();
				}
				catch (Exception ex)
				{
					_logger.LogCritical(ex, "Well... that sucks.");
					throw;
				}

				await ExecuteScehmaMigrations(application.ServiceProvider, stoppingToken);
				await ExecuteSeeding(application.ServiceProvider, stoppingToken);

				_logger.LogInformation("Migration and Seeding Completed.");

				await application.ShutdownAsync();
			}
		}
		finally
		{
			_hostedServiceMonitor.ServiceStopped();
		}
	}

	private async Task ExecuteScehmaMigrations(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		using (var scope = serviceProvider.CreateAsyncScope())
		{
			var services = scope.ServiceProvider.GetServices<IDbMigrationService>();

			if (services.Any())
			{
				_logger.LogInformation("Migration Service Started");

				foreach (var service in services.OrderBy(s => s.Priority))
				{
					await service.MigrateAsync(cancellationToken);
				}

				_logger.LogInformation("Migration Service Completed");
			}
			else
			{
				_logger.LogWarning("At least one IDbMigrationService must be registered.");
			}
		}
	}

	private async Task ExecuteSeeding(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		using (var scope = serviceProvider.CreateAsyncScope())
		{
			var seeder = scope.ServiceProvider.GetService<DbSeedingService>();
			if (seeder == null)
			{
				_logger.LogWarning("Could not get the DbSeedingService");
				return;
			}

			await seeder.SeedAsync(cancellationToken);
		}
	}
}
