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

using Ezrie.Hosting.AspNetCore.Microservices;
using Ezrie.Hosting.Gateways;
using Microsoft.AspNetCore.Rewrite;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Ezrie.Crm.Gateway;

[DependsOn(typeof(EzrieHostingGatewaysModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
public class PublicGatewayModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		var configuration = context.Services.GetConfiguration();

		// Requested scopes for authorization code request and descriptions for swagger UI only
		context.ConfigureSwaggerWithAuth();
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		var env = context.GetEnvironment();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.ConfigureSwaggerUIWithYarp(context);

		app.UseRewriter(new RewriteOptions()
			// Regex for "", "/" and "" (whitespace)
			.AddRedirect("^(|\\|\\s+)$", "/swagger"));

		app.UseEndpoints(endpoints => endpoints.MapReverseProxy());
	}
}
