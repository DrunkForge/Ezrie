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

using Ezrie.Configuration;
using Ezrie.EntityFrameworkCore.Seeding;
using Ezrie.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Ezrie.EntityFrameworkCore.Migrations;

public class MigrateAndSeedService<T>
	where T : MigrationsModuleBase
{
	private readonly ILogger _logger;
	private readonly IServiceProvider _serviceProvider;

	public MigrateAndSeedService(ILogger logger, IServiceProvider serviceProvider)
	{
		_logger = logger;
		_serviceProvider = serviceProvider;
	}

	public async Task MigrateAndSeedAsync(CancellationToken cancellationToken = default)
	{
			await ExecuteMigrations(_serviceProvider, cancellationToken);

			await ExecuteSeeding(_serviceProvider, cancellationToken);
	}

	private async Task ExecuteMigrations(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		using (var scope = serviceProvider.CreateAsyncScope())
		{
			var services = scope.ServiceProvider.GetServices<IDbSchemaMigrationService>();

			if (services.Any())
			{
				_logger.LogInformation("Migration Service Started");

				foreach (var service in services)
				{
					_logger.LogInformation("Executing {MigrationService}", service.GetType().Name);
					await service.MigrateAsync(cancellationToken);
				}

				_logger.LogInformation("Migration Service Completed");
			}
			else
			{
				_logger.LogWarning("At least one IDbContextMigrationRunner must be registered.");
			}
		}
	}

	private async Task ExecuteSeeding(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		using (var scope = serviceProvider.CreateAsyncScope())
		{
			var seeder = scope.ServiceProvider.GetService<SeedingService>();
			if (seeder == null)
			{
				_logger.LogWarning("Could not get the DbSeedingService");
				return;
			}

			await seeder.SeedAsync(cancellationToken);
		}
	}
}
