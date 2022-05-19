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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace Ezrie.Hosting.AspNetCore;

[DependsOn(typeof(EzrieHostingModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
public class EzrieHostingAspNetCoreModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		ConfigureDistributedCache(context);
		context.ConfigureCors();
	}

	private void ConfigureDistributedCache(ServiceConfigurationContext context)
	{
		var redis = context.GetRedis();

		Configure<AbpDistributedCacheOptions>(options => options.KeyPrefix = EzrieConstants.DistributedCachePrefix);

		var connectionMultiplexer = ConnectionMultiplexer.Connect(redis.Configuration);
		context.Services
			.AddDataProtection()
			.SetApplicationName(EzrieConstants.DataProtectionApplicationName)
			.PersistKeysToStackExchangeRedis(connectionMultiplexer, EzrieConstants.DataProtectionKey);
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		var env = context.GetEnvironment();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseHsts();
		}

		app.UseSerilogRequestLogging();
		app.UseHttpsRedirection();
		app.UseCorrelationId();
		app.UseStaticFiles();
		app.UseAbpRequestLocalization();
		app.UseRouting();

		if (app.ApplicationServices.GetApiConfiguration().EnableCors)
		{
			app.UseCors();
		}

		app.UseAuthentication();
		app.UseAuthorization();
		app.UseAuditing();
		app.UseAbpSerilogEnrichers();
	}
}
