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

using Ezrie.AppSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Modularity;

namespace Ezrie.Hosting.AspNetCore;

public static class ApplicationBuilderHelper
{
	public static async Task<WebApplication> BuildApplicationAsync<TStartupModule>(String[] args)
		where TStartupModule : IAbpModule
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Host
			.AddAppSettingsSecretsJson()
			.UseAutofac();

		await builder.AddApplicationAsync<TStartupModule>();
		return builder.Build();
	}

	public static void ConfigureJwtAuthentication(this ServiceConfigurationContext context)
	{
		var apiConfiguration = context.Services.GetConfiguration().GetApiConfiguration();
		context.Services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.Authority = apiConfiguration.IdentityServerBaseUrl;
				options.RequireHttpsMetadata = apiConfiguration.RequireHttpsMetadata;
				options.Audience = apiConfiguration.OidcApiName;
			});
	}
}
