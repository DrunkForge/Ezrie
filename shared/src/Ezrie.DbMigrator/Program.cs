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

using Ezrie.Admin.EntityFrameworkCore;
using Ezrie.AdministrationService.EntityFrameworkCore;
using Ezrie.Configuration;
using Ezrie.EntityFrameworkCore;
using Ezrie.IdentityService.EntityFrameworkCore;
using Ezrie.Logging;
using Ezrie.Migrations;
using Ezrie.TenantService.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Ezrie.DbMigrator;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is logged and it doesn't matter why it failed.")]
	private static async Task<Int32> Main(String[] args)
	{
		try
		{
			await MigrateAndSeed.ExecuteAsync<AdminEntityFrameworkCoreMigrationsModule>();
			await MigrateAndSeed.ExecuteAsync<AdministrationServiceEntityFrameworkCoreMigrationsModule>();
			await MigrateAndSeed.ExecuteAsync<IdentityServiceEntityFrameworkCoreMigrationsModule>();
			await MigrateAndSeed.ExecuteAsync<TenantServiceEntityFrameworkCoreMigrationsModule>();

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
