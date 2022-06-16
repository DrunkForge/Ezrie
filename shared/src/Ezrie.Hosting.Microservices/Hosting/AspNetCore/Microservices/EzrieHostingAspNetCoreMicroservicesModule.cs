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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Ezrie.Hosting.AspNetCore.Microservices;

[DependsOn(typeof(EzrieHostingAspNetCoreModule))]
[DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
[DependsOn(typeof(AbpAspNetCoreAuthenticationJwtBearerModule))]
[DependsOn(typeof(AbpHttpClientIdentityModelWebModule))]
[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
[DependsOn(typeof(AbpAccountWebIdentityServerModule))]
public class EzrieHostingAspNetCoreMicroservicesModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		ConfigureAuthentication(context);
		ConfigureCors(context);
		ConfigureSwaggerWithAuth(context);
	}

	private static void ConfigureAuthentication(ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();
		context.Services.AddAuthentication()
			.AddJwtBearer(options =>
			{
				configuration.Bind(nameof(JwtBearerOptions), options);
                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
			});
			// .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, "Ezrie Account", options =>
			// {
			// 	configuration.Bind(nameof(OpenIdConnectOptions), options);

			// 	options.GetClaimsFromUserInfoEndpoint = true;
			// 	options.SaveTokens = true;

			// 	if (!options.Scope.Contains("email"))
			// 		options.Scope.Add("email");
			// 	if (!options.Scope.Contains("role"))
			// 		options.Scope.Add("role");
			// });

		context.Services.AddAuthorization();
	}

    private void ConfigureCors(ServiceConfigurationContext context)
    {
		var corsOptions = context.Services.GetCorsOptions();
		var origins = corsOptions.AllowOrigins.Select(o => o.RemovePostFix("/")).ToArray();

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
               {
                builder
                    .WithOrigins(origins)
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

	private static void ConfigureSwaggerWithAuth(ServiceConfigurationContext context, Dictionary<String, String>? scopes = null)
	{
		ArgumentNullException.ThrowIfNull(context);

		var swagger = context.Services.GetSwaggerOptions();
		if (scopes == null)
		{
			scopes = new();
		}

		foreach (var scope in swagger.Scopes)
		{
			scopes.Add(scope.Id, scope.Name);
		}

		context.Services.AddAbpSwaggerGenWithOAuth(
			swagger.Authority,
			scopes: scopes,
			options =>
			{
				options.SwaggerDoc(swagger.ApiVersion, new OpenApiInfo { Title = swagger.ApiName, Version = swagger.ApiVersion });
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

		app.UseAbpRequestLocalization();

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseJwtTokenMiddleware();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseSwagger();
		app.UseAbpSwaggerUI(options =>
		{
			var swaggerOptions = context.GetConfiguration().GetSwaggerOptions();
			options.SwaggerEndpoint(swaggerOptions.EndPointUrl, swaggerOptions.ApiName);
			options.OAuthClientId(swaggerOptions.ClientId);
			options.OAuthClientSecret(swaggerOptions.ClientSecret);
			options.OAuthScopes(swaggerOptions.Scopes.Select(s => s.Id).ToArray());
		});

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
	}
}
