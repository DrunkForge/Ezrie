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

using System.IdentityModel.Tokens.Jwt;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Skoruba.AuditLogging.EntityFramework.Entities;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;
using Serilog;
using Ezrie.AccountManagement.Identity;
using Ezrie.AccountManagement.EntityFrameworkCore.EntityFrameworkCore;
using Ezrie.AccountManagement.Configuration;
using Ezrie.AccountManagement.Resources;
using Ezrie.AccountManagement.Mappers;
using Ezrie.AccountManagement.ExceptionHandling;
using Ezrie.AccountManagement.Helpers;
using Ezrie.AccountManagement.Configuration.Authorization;

namespace Ezrie.AccountManagement;

public class Startup
{
	public Startup(IWebHostEnvironment env, IConfiguration configuration)
	{
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
		HostingEnvironment = env;
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public IWebHostEnvironment HostingEnvironment { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		var adminApiConfiguration = Configuration.GetSection(nameof(AdminApiConfiguration)).Get<AdminApiConfiguration>();
		services.AddSingleton(adminApiConfiguration);

		// Add DbContexts
		RegisterDbContexts(services);

		services.AddDataProtection<IdentityServerDataProtectionDbContext>(Configuration);

		// Add email senders which is currently setup for SendGrid and SMTP
		services.AddEmailSenders(Configuration);

		services.AddScoped<ControllerExceptionFilterAttribute>();
		services.AddScoped<IApiErrorResources, ApiErrorResources>();

		// Add authentication services
		RegisterAuthentication(services);

		// Add authorization services
		RegisterAuthorization(services);

		var profileTypes = new HashSet<Type>
			{
				typeof(IdentityMapperProfile<IdentityRoleDto, IdentityUserRolesDto, String, IdentityUserClaimsDto, IdentityUserClaimDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto, IdentityRoleClaimDto, IdentityRoleClaimsDto>)
			};

		services.AddAdminAspNetIdentityServices<AdminIdentityDbContext, IdentityServerPersistedGrantDbContext,
			IdentityUserDto, IdentityRoleDto, UserIdentity, UserIdentityRole, String, UserIdentityUserClaim, UserIdentityUserRole,
			UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
			IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
			IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
			IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>(profileTypes);

		services.AddAdminServices<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>();

		services.AddAdminApiCors(adminApiConfiguration);

		services.AddMvcServices<IdentityUserDto, IdentityRoleDto,
			UserIdentity, UserIdentityRole, String, UserIdentityUserClaim, UserIdentityUserRole,
			UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
			IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
			IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
			IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>();

		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc(adminApiConfiguration.ApiVersion, new OpenApiInfo { Title = adminApiConfiguration.ApiName, Version = adminApiConfiguration.ApiVersion });

			options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.OAuth2,
				Flows = new OpenApiOAuthFlows
				{
					AuthorizationCode = new OpenApiOAuthFlow
					{
						AuthorizationUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/authorize"),
						TokenUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/token"),
						Scopes = new Dictionary<String, String> {
								{ adminApiConfiguration.OidcApiName, adminApiConfiguration.ApiName }
						}
					}
				}
			});
			options.OperationFilter<AuthorizeCheckOperationFilter>();
		});

		services.AddAuditEventLogging<AdminAuditLogDbContext, AuditLog>(Configuration);

		services.AddIdSHealthChecks<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminIdentityDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext>(Configuration, adminApiConfiguration);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AdminApiConfiguration adminApiConfiguration)
	{
		app.UseSerilogRequestLogging();
		app.AddForwardHeaders();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint($"{adminApiConfiguration.ApiBaseUrl}/swagger/v1/swagger.json", adminApiConfiguration.ApiName);

			c.OAuthClientId(adminApiConfiguration.OidcSwaggerUIClientId);
			c.OAuthAppName(adminApiConfiguration.ApiName);
			c.OAuthUsePkce();
		});

		app.UseRouting();
		UseAuthentication(app);
		app.UseCors();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();

			endpoints.MapHealthChecks("/health", new HealthCheckOptions
			{
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
		});
	}

	public virtual void RegisterDbContexts(IServiceCollection services)
	{
		services.AddDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext, AuditLog>(Configuration);
	}

	public virtual void RegisterAuthentication(IServiceCollection services)
	{
		services.AddApiAuthentication<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(Configuration);
	}

	public virtual void RegisterAuthorization(IServiceCollection services)
	{
		services.AddAuthorizationPolicies();
	}

	// This gets overriden by unit tests.
	public virtual void UseAuthentication(IApplicationBuilder app)
	{
		app.UseAuthentication();
	}
}

