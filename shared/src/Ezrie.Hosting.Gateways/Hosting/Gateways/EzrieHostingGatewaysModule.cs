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
using Ezrie.Hosting.AspNetCore;
using Ezrie.MultiTenancy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;

namespace Ezrie.Hosting.Gateways;

[DependsOn(typeof(EzrieHostingAspNetCoreModule))]
[DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
public class EzrieHostingGatewaysModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		ConfigureReverseProxy(context);
		//ConfigureSwaggerWithAuth(context);
	}

	private static void ConfigureCors(ServiceConfigurationContext context)
	{
		context.Services.AddCors(options =>
		{
			options.AddPolicy("ApiPolicy", builder =>
			{
				builder.AllowAnyOrigin();
			});
		});
	}

	private static void ConfigureAuthentication(ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();
		context.Services
			.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				configuration.Bind(nameof(JwtBearerOptions), options);
			})
			.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, "Ezrie Account", options =>
			{
				configuration.Bind(nameof(OpenIdConnectOptions), options);

				options.GetClaimsFromUserInfoEndpoint = true;
				options.SaveTokens = true;

				if (!options.Scope.Contains("email"))
					options.Scope.Add("email");
				if (!options.Scope.Contains("role"))
					options.Scope.Add("role");
			});
	}

	private static void ConfigureReverseProxy(ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();

		context.Services.AddReverseProxy()
			.LoadFromConfig(configuration.GetSection("ReverseProxy"));
	}

	private static void ConfigureSwaggerWithAuth(ServiceConfigurationContext context, Dictionary<String, String>? scopes = null)
	{
		ArgumentNullException.ThrowIfNull(context);

		var hostOptions = context.Services.GetSwaggerOptions();
		if (scopes == null)
		{
			scopes = new();
		}

		foreach (var scope in hostOptions.Scopes)
		{
			scopes.Add(scope.Id, scope.Name);
		}

		context.Services.AddAbpSwaggerGenWithOAuth(
			hostOptions.Authority,
			scopes: scopes,
			options =>
			{
				options.SwaggerDoc(hostOptions.ApiVersion, new OpenApiInfo { Title = hostOptions.ApiName, Version = hostOptions.ApiVersion });
				options.DocInclusionPredicate((_, __) => true);
				options.CustomSchemaIds(type => type.FullName);
			});
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
			app.UseStatusCodePagesWithReExecute("~/error");
		}

		app.UseCorrelationId();
		app.UseStaticFiles();
		app.UseAbpRequestLocalization();
		app.UseAuditing();
		app.UseRouting();
		app.UseEndpoints(endpoints => endpoints.MapReverseProxy());
	}
}
