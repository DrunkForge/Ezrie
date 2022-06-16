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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Ezrie.Seeding;

public class EzrieDataSeeder : IDataSeeder, ITransientDependency
{
	protected IServiceScopeFactory ServiceScopeFactory { get; }
	protected AbpDataSeedOptions Options { get; }

	public EzrieDataSeeder(IOptions<AbpDataSeedOptions> options, IServiceScopeFactory serviceScopeFactory)
	{
		Options = options.Value;
		ServiceScopeFactory = serviceScopeFactory;
	}

	[UnitOfWork]
	public virtual async Task SeedAsync(DataSeedContext context)
	{
		using (var scope = ServiceScopeFactory.CreateScope())
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<EzrieDataSeeder>>();
			foreach (var contributorType in Options.Contributors)
			{
				var contributor = (IDataSeedContributor)scope
					.ServiceProvider
					.GetRequiredService(contributorType);

				logger.LogInformation("Seeding with {DataSeedContributor}", contributorType.FullName);

				await contributor.SeedAsync(context);
			}
		}
	}
}
