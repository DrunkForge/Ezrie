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
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace Ezrie.Hosting;

[DependsOn(typeof(EzrieDomainModule))]
[DependsOn(typeof(AbpBackgroundJobsRabbitMqModule))]
[DependsOn(typeof(AbpDistributedLockingModule))]
[DependsOn(typeof(AbpEventBusRabbitMqModule))]
public partial class EzrieHostingModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		var redis = context.GetRedis();

		context.Services.AddSingleton<IDistributedLockProvider>(_ =>
		{
			var connection = ConnectionMultiplexer.Connect(redis.Configuration);
			return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
		});
	}
}
