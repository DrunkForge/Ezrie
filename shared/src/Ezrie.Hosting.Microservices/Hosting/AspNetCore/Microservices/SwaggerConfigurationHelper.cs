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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp.Modularity;

namespace Ezrie.Hosting.AspNetCore.Microservices;

public static class SwaggerConfigurationHelper
{
	public static void ConfigureSwaggerWithAuth(this ServiceConfigurationContext context, Dictionary<String, String>? scopes = null)
	{
		ArgumentNullException.ThrowIfNull(context);

		var apiConfiguration = context.Services.GetConfiguration().GetAppConfiguration();

		context.Services.AddAbpSwaggerGenWithOAuth(
			apiConfiguration.IdentityServerBaseUrl,
			scopes: scopes,
			options =>
			{
				options.SwaggerDoc(apiConfiguration.ApiVersion, new OpenApiInfo { Title = apiConfiguration.ApiName, Version = apiConfiguration.ApiVersion });
				options.DocInclusionPredicate((_, __) => true);
				options.CustomSchemaIds(type => type.FullName);
			});
	}
}
