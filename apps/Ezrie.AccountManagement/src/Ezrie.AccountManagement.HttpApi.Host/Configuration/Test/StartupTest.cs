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

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Ezrie.AccountManagement.Identity;
using Ezrie.AccountManagement.Middlewares;
using Ezrie.AccountManagement.Helpers;
using Ezrie.AccountManagement.EntityFrameworkCore;

namespace Ezrie.AccountManagement.Configuration.Test;

public class StartupTest : Startup
{
	public StartupTest(IWebHostEnvironment env, IConfiguration configuration) : base(env, configuration)
	{
	}

	public override void RegisterDbContexts(IServiceCollection services)
	{
		services.RegisterDbContextsStaging<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext>();
	}

	public override void RegisterAuthentication(IServiceCollection services)
	{
		services
			.AddIdentity<UserIdentity, UserIdentityRole>(options => Configuration.GetSection(nameof(IdentityOptions)).Bind(options))
			.AddEntityFrameworkStores<AdminIdentityDbContext>()
			.AddDefaultTokenProviders();

		services.AddAuthentication(options =>
		{
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddCookie(JwtBearerDefaults.AuthenticationScheme);
	}

	public override void RegisterAuthorization(IServiceCollection services)
	{
		services.AddAuthorizationPolicies();
	}

	public override void UseAuthentication(IApplicationBuilder app)
	{
		app.UseAuthentication();
		app.UseMiddleware<AuthenticatedTestRequestMiddleware>();
	}
}

