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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Modularity;

namespace Ezrie.Configuration;

public static class ConfigurationExtensions
{
	public const String AppYarpJsonPath = "yarp.json";

	public static IHostBuilder AddYarpJson(this IHostBuilder hostBuilder, Boolean optional = true, Boolean reloadOnChange = true, String? path = null)
	{
		ArgumentNullException.ThrowIfNull(hostBuilder);

		return hostBuilder.ConfigureAppConfiguration((_, builder) =>
		{
			builder.AddJsonFile(
				path: path ?? AppYarpJsonPath,
				optional: optional,
				reloadOnChange: reloadOnChange
			);
		});
	}

	public static HostConfiguration GetHostConfiguration(this IConfiguration configuration)
		=> configuration.GetOptions<HostConfiguration>()
		?? new();

	public static RedisConfiguration GetRedis(this ServiceConfigurationContext context)
		=> context.Services.GetConfiguration().GetOptions<RedisConfiguration>()
		?? new();

	public static RedisConfiguration GetRedis(this IConfiguration configuration)
		=> configuration.GetOptions<RedisConfiguration>()
		?? new();

	public static AppConfiguration GetAppConfiguration(this IServiceCollection services)
		=> services.GetOptions<AppConfiguration>()
		?? throw new ConfigurationException($"The AppConfiguration section is missing or invalid.");

	public static AppConfiguration GetAppConfiguration(this IServiceProvider serviceProvider)
		=> serviceProvider.GetOptions<AppConfiguration>()
		?? throw new ConfigurationException($"The AppConfiguration section is missing or invalid.");

	public static AppConfiguration GetAppConfiguration(this IConfiguration configuration)
		=> configuration.GetOptions<AppConfiguration>()
		?? throw new ConfigurationException($"The AppConfiguration section is missing or invalid.");

	public static RemoteServices GetRemoteServices(this ServiceConfigurationContext context)
		=> context.Services.GetConfiguration().GetOptions<RemoteServices>()
		?? new();

	private static T? GetOptions<T>(this IServiceProvider serviceProvider) where T : class
		=> serviceProvider.GetRequiredService<IConfiguration>().GetOptions<T>();

	private static T? GetOptions<T>(this IServiceCollection services) where T : class
		=> services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetOptions<T>();

	public static T? GetOptions<T>(this IConfiguration configuration, T? defaultValue = null) where T : class
		=> configuration.GetSection(typeof(T).Name).Get<T>() ?? defaultValue;
}
