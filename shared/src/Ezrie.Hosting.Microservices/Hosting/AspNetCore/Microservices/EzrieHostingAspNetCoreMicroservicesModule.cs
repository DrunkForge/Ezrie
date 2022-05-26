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
using Ezrie.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Ezrie.Hosting.AspNetCore.Microservices;

[DependsOn(typeof(EzrieHostingAspNetCoreModule))]
[DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
[DependsOn(typeof(AbpHttpClientIdentityModelWebModule))]
[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
public class EzrieHostingAspNetCoreMicroservicesModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		var configuration = context.Services.GetConfiguration();

		context.ConfigureSwaggerWithAuth();
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		var configuration = context.GetConfiguration();
		var apiConfiguration = configuration.GetAppConfiguration();

		if (MultiTenancyConsts.IsEnabled)
		{
			app.UseMultiTenancy();
		}

		app.UseUnitOfWork();
		app.UseSwagger();
		app.UseAbpSwaggerUI(options =>
		{
			options.SwaggerEndpoint(SwaggerProperties.EndPointUrl, apiConfiguration.ApiName);
			options.OAuthClientId(apiConfiguration.ClientId);
			options.OAuthClientSecret(apiConfiguration.ClientSecret);
			options.OAuthScopes(apiConfiguration.Scopes);
		});

		app.UseConfiguredEndpoints(endpoints =>
		{
			endpoints.MapControllers();
			endpoints.MapDefaultControllerRoute();
		});
	}
}
