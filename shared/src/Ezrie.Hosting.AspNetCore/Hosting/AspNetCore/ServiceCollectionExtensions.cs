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
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezrie.Hosting.AspNetCore;

public static class ServiceCollectionExtensions
{
	public static void ConfigureCors(this IServiceCollection services, Action<CorsPolicyBuilder>? defaultPolicy = null)
	{
		var apiConfiguration = services.GetConfiguration().GetApiConfiguration();
		if (apiConfiguration.EnableCors)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					if (apiConfiguration.CorsAllowAnyOrigin)
					{
						builder
							.AllowAnyOrigin();
					}
					else
					{
						builder
							.WithOrigins(apiConfiguration.CorsAllowOrigins.Select(o => o.RemovePostFix("/")).ToArray())
							.SetIsOriginAllowedToAllowWildcardSubdomains();
					}

					builder
						.AllowAnyHeader()
						.AllowAnyMethod();

					defaultPolicy?.Invoke(builder);
				});
			});
		}
	}
}
