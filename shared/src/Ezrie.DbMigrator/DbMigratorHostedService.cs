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

using Ezrie.AdministrationService.EntityFrameworkCore.Migrations;
using Ezrie.CRM.EntityFrameworkCore.Migrations;
using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.IdentityService.EntityFrameworkCore.Migrations;
using Ezrie.TenantService.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ezrie.DbMigrator;

public class DbMigratorHostedService : BackgroundService
{
	private readonly IHostApplicationLifetime _hostApplicationLifetime;
	private readonly ILogger<DbMigratorHostedService> _logger;

	public DbMigratorHostedService(IHostApplicationLifetime applicationLifetime, ILogger<DbMigratorHostedService> logger)
	{
		_hostApplicationLifetime = applicationLifetime;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await new MigrationHost<CRMEntityFrameworkCoreMigrationsModule>(_logger).MigrateAndSeedAsync(stoppingToken);

		await new MigrationHost<IdentityServiceEntityFrameworkCoreMigrationsModule>(_logger).MigrateAndSeedAsync(stoppingToken);
		
		await new MigrationHost<TenantServiceEntityFrameworkCoreMigrationsModule>(_logger).MigrateAndSeedAsync(stoppingToken);

		await new MigrationHost<AdministrationServiceEntityFrameworkCoreMigrationsModule>(_logger).MigrateAndSeedAsync(stoppingToken);

		_hostApplicationLifetime.StopApplication();
	}
}
