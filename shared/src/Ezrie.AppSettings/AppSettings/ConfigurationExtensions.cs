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
using Serilog.Events;
using Volo.Abp.Modularity;

namespace Ezrie.AppSettings;

public static class ConfigurationExtensions
{
	public static T GetOptions<T>(this IConfiguration configuration)
		where T : new()
	{
		ArgumentNullException.ThrowIfNull(configuration);

		return configuration.GetSection(typeof(T).Name).Get<T>() ?? new();
	}

	public static HostConfiguration GetHostConfiguration(this IConfiguration configuration)
		=> configuration.GetOptions<HostConfiguration>();

	public static RedisConfiguration GetRedis(this ServiceConfigurationContext context)
		=> context.Services.GetConfiguration().GetOptions<RedisConfiguration>();

	public static RedisConfiguration GetRedis(this IConfiguration configuration)
		=> configuration.GetOptions<RedisConfiguration>();

	public static ApiConfiguration GetApiConfiguration(this IConfiguration configuration)
		=> configuration.GetOptions<ApiConfiguration>();

	public static LogEventLevel GetSerilogMinimumLevel(this IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		var settings = configuration.GetSection(SerilogConfiguration.SectionName).Get<SerilogConfiguration>() ?? new();

		return settings.MinimumLevel.Default;
	}

	public static SeqSettings GetSeq(this IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		return configuration.GetSection(SeqSettings.SectionName).Get<SeqSettings>() ?? new();
	}

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
}
