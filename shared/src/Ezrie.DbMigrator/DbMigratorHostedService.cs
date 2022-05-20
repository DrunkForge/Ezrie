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

using Ezrie.AdministrationService.EntityFrameworkCore.Migration;
using Ezrie.EntityFrameworkCore.Seeding;
using Ezrie.Migrations;
using Ezrie.TenantService.EntityFrameworkCore.Migration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Ezrie.EntityFrameworkCore.Migrations;

public class DbMigratorHostedService : BackgroundService
{
	private readonly IConfiguration _configuration;
	private readonly IHostApplicationLifetime _hostApplicationLifetime;
	private readonly ILogger<DbMigratorHostedService> _logger;

	public DbMigratorHostedService(IConfiguration configuration, IHostApplicationLifetime applicationLifetime, ILogger<DbMigratorHostedService> logger)
	{
		_hostApplicationLifetime = applicationLifetime;
		_configuration = configuration;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await new MigrationHost<TenantServiceEntityFrameworkCoreMigrationModule>(_configuration, _logger).MigrateAndSeedAsync(stoppingToken);

		await new MigrationHost<AdministrationServiceEntityFrameworkCoreMigrationModule>(_configuration, _logger).MigrateAndSeedAsync(stoppingToken);

		_hostApplicationLifetime.StopApplication();
	}
}
