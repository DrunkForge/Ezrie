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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ezrie.EntityFrameworkCore.Migrations;

public abstract class MigrationDbContextFactoryBase<T> : IDesignTimeDbContextFactory<T>
	where T : DbContext
{
	public abstract String ConnectionStringName { get; }
	public abstract T CreateContext(DbContextOptions<T> options);

	public T CreateDbContext(String[] args)
	{
		var builder = new DbContextOptionsBuilder<T>()
			.UseNpgsql(GetConnectionStringFromConfiguration(), o => o.UseNodaTime());

		return CreateContext(builder.Options);
	}

	private String GetConnectionStringFromConfiguration()
	{
		var configuration = BuildConfiguration();
		return configuration.GetConnectionString(ConnectionStringName);
	}

	private IConfigurationRoot BuildConfiguration()
	{
		var environment = RuntimeEnvironment.GetEnvironmentName();
		var basePath = Directory.GetCurrentDirectory();
		Console.WriteLine($"BasePath: {basePath} Environment: {environment} ConnectionStringName: {ConnectionStringName}");
		var builder = new ConfigurationBuilder()
			.SetBasePath(basePath)
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile($"appsettings.{environment}.json", true);

		return builder.Build();
	}
}
