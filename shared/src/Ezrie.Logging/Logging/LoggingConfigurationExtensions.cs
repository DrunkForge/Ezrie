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
using Ezrie.Logging.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Expressions;

namespace Ezrie.Logging;

public static class LoggingConfigurationExtensions
{
	public static IHostBuilder UseEzrieLogging<T>(this IHostBuilder hostBuilder)
		=> hostBuilder.UseEzrieLogging(typeof(T).Assembly.GetName().Name ?? typeof(T).Name);

	public static IHostBuilder UseEzrieLogging(this IHostBuilder hostBuilder, String applicationName)
		=> hostBuilder
			.ConfigureLogging((context, logging) => logging
				.ClearProviders()
				.AddEzrieLogging(context.Configuration, applicationName))
			.UseSerilog();

	public static ILoggingBuilder AddEzrieLogging<T>(this ILoggingBuilder builder, IConfiguration configuration)
		=> builder.AddEzrieLogging(configuration, typeof(T).Assembly.GetName().Name ?? typeof(T).Name);

	public static ILoggingBuilder AddEzrieLogging(this ILoggingBuilder builder, IConfiguration configuration, String applicationName)
	{
		if (String.IsNullOrWhiteSpace(applicationName))
		{
			throw new ArgumentException($"'{nameof(applicationName)}' cannot be null or whitespace.", nameof(applicationName));
		}

		var level = configuration.GetSerilogMinimumLevel();
		var host = configuration.GetHostConfiguration();
		var seq = configuration.GetSeq();

		SelfLogProperties.SelfLogPath = host.SelfLogPath;
		Serilog.Debugging.SelfLog.Enable(SelfLog.WriteLine);

		// Initially include all log entries with a filter of `true`
		var filterSwitch = new LoggingFilterSwitch("true");
		var levelSwitch = new LoggingLevelSwitch(level);
		seq.ControlLevelSwitch = levelSwitch;

		builder.Services.AddSingleton(filterSwitch);
		builder.Services.AddSingleton(levelSwitch);

		var logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Application", applicationName)
				.Filter.ControlledBy(filterSwitch)
				.MinimumLevel.ControlledBy(levelSwitch)
				.WriteTo.Seq(seq);

		Log.Logger = logger.CreateLogger();

		builder.AddSerilog();

		Log.Write(LogEventLevel.Information, "Starting: {ApplicationName} in {EnvironmentName}",
			applicationName, RuntimeEnvironment.GetEnvironmentName());
		Log.Debug("Debug Logging Enabled");
		Log.Verbose("Verbose Logging Enabled");

		return builder;
	}

	internal static LoggerConfiguration Seq(this LoggerSinkConfiguration loggerConfig, SeqSettings seq)
	{
		ArgumentNullException.ThrowIfNull(loggerConfig);
		ArgumentNullException.ThrowIfNull(seq);

		return loggerConfig.Seq(
			serverUrl: seq.ServerUrl,
			restrictedToMinimumLevel: seq.RestrictedToMinimumLevel,
			batchPostingLimit: seq.BatchPostingLimit,
			period: seq.Period,
			apiKey: seq.ApiKey,
			bufferBaseFilename: seq.BufferBaseFilename,
			bufferSizeLimitBytes: seq.BufferSizeLimitBytes,
			eventBodyLimitBytes: seq.EventBodyLimitBytes,
			controlLevelSwitch: seq.ControlLevelSwitch,
			messageHandler: seq.MessageHandler,
			retainedInvalidPayloadsLimitBytes: seq.RetainedInvalidPayloadsLimitBytes,
			queueSizeLimit: seq.QueueSizeLimit
		);
	}
}
