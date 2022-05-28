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
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ezrie.Hosting.AspNetCore;

public static class ServiceConfigurationContextExtensions
{
	public static void ConfigureJwtAuthentication(this ServiceConfigurationContext context, String audience)
	{
		if (String.IsNullOrWhiteSpace(audience))
		{
			throw new ArgumentException($"'{nameof(audience)}' cannot be null or whitespace.", nameof(audience));
		}

		var hostoptions = context.Services.GetConfiguration().GetHostOptions();

		context.Services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.Authority = hostoptions.Authority;
				options.RequireHttpsMetadata = hostoptions.RequireHttpsMetadata;
				options.Audience = audience;
			});
	}
}
