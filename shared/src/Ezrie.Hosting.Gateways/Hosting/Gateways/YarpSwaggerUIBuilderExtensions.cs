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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Yarp.ReverseProxy.Configuration;

namespace Ezrie;

public static partial class YarpSwaggerUIBuilderExtensions
{
	public static IApplicationBuilder ConfigureSwaggerUIWithYarp(this IApplicationBuilder app, ApplicationInitializationContext context)
	{
		app.UseSwagger();
		app.UseSwaggerUI(options =>
		{
			var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
			var hostOptions = configuration.GetHostOptions();
			var logger = context.ServiceProvider.GetRequiredService<ILogger<ApplicationInitializationContext>>();
			var proxyConfigProvider = context.ServiceProvider.GetRequiredService<IProxyConfigProvider>();
			var yarpConfig = proxyConfigProvider.GetConfig();

#pragma warning disable CS8603 // Possible null reference return. - Not my code. -dw
			var routedClusters = yarpConfig.Clusters
				.SelectMany(t => t.Destinations, (clusterId, destination) => new { clusterId.ClusterId, destination.Value });
#pragma warning restore CS8603 // Possible null reference return.

			var groupedClusters = routedClusters
				.GroupBy(q => q.Value.Address)
				.Select(t => t.First())
				.Distinct()
				.ToList();

			foreach (var clusterGroup in groupedClusters)
			{
				var routeConfig = yarpConfig.Routes.FirstOrDefault(q =>
					q.ClusterId == clusterGroup.ClusterId);
				if (routeConfig == null)
				{
					logger.RouteMissing(clusterGroup.ClusterId);
					continue;
				}

				options.SwaggerEndpoint($"{clusterGroup.Value.Address}{SwaggerProperties.EndPointUrl}", $"{routeConfig.RouteId} API");
				options.OAuthClientId(hostOptions.ClientId);
				options.OAuthClientSecret(hostOptions.ClientSecret);
			}
		});

		return app;
	}

	[LoggerMessage(0, LogLevel.Warning, "Swagger UI: Couldn't find route configuration for {ClusterId}")]
	static partial void RouteMissing(this ILogger logger, String clusterId);
}
