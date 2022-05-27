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
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Ezrie.EntityFrameworkCore;

[DependsOn(typeof(EzrieDomainSharedModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
public class EzrieEntityFrameworkCoreModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		// https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpDbContextOptions>(options =>
		{
			options.Configure(action =>
			{
				action.DbContextOptions
					.EnableSensitiveDataLogging(true)
					.EnableDetailedErrors(true);
			});

			options.UseNpgsql(o => o.UseNodaTime());
		});
	}
}
