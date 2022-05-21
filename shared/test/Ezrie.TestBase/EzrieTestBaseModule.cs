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

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Ezrie;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpTestBaseModule))]
[DependsOn(typeof(AbpAuthorizationModule))]
public class EzrieTestBaseModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpBackgroundJobOptions>(options =>
		{
			options.IsJobExecutionEnabled = false;
		});

		context.Services.AddAlwaysAllowAuthorization();
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		SeedTestData(context);
	}

	private static void SeedTestData(ApplicationInitializationContext context)
	{
		AsyncHelper.RunSync(async () =>
		{
			using (var scope = context.ServiceProvider.CreateScope())
			{
				await scope.ServiceProvider
					.GetRequiredService<IDataSeeder>()
					.SeedAsync();
			}
		});
	}
}
