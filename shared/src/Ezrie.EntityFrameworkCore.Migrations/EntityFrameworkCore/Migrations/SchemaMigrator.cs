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
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Ezrie.EntityFrameworkCore.Migrations;

public class DbContextMigrator<TDbContext> : IDbContextMigrator
	where TDbContext : IEfCoreDbContext
{
	private readonly IServiceProvider _serviceProvider;

	public DbContextMigrator(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public Boolean CanMigrate<T>() => typeof(TDbContext).IsAssignableFrom(typeof(T));

	public async Task MigrateAsync(CancellationToken cancellationToken = default)
	{
		/* We intentionally resolving the TDbContext
		 * from IServiceProvider (instead of directly injecting it)
		 * to properly get the connection string of the current tenant in the
		 * current scope.
		 */

		var dbContext = _serviceProvider.GetRequiredService<TDbContext>();

		await dbContext.Database.MigrateAsync(cancellationToken);
	}
}
