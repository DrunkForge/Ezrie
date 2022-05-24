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
using Serilog.Extensions.Hosting;
using Serilog.Extensions.Logging;

namespace Ezrie.Logging;

public static class LoggingConfigurationExtensions
{
	public static void AddEzrieLogging<T>(this IServiceCollection services, IConfiguration configuration)
		=> AddEzrieLogging(services, configuration, typeof(T).Assembly.GetName().Name ?? typeof(T).Name);

	public static void AddEzrieLogging(this IServiceCollection services, IConfiguration configuration, String applicationName)
	{
		if (Log.Logger is Logger)
		{
			AddChildLogger(services, Log.Logger);
			return;
		}

		ArgumentNullException.ThrowIfNull(services);
		if (applicationName.IsNullOrWhiteSpace())
			throw new ArgumentException($"The '{nameof(applicationName)}' parameter cannot be null or whitespace.", nameof(applicationName));

		var level = configuration.GetSerilogMinimumLevel();
		var seq = configuration.GetSeq();

		// Initially include all log entries with a filter of `true`
		var filterSwitch = new LoggingFilterSwitch("true");
		var levelSwitch = new LoggingLevelSwitch(level);
		seq.ControlLevelSwitch = levelSwitch;

		services.AddSingleton(filterSwitch);
		services.AddSingleton(levelSwitch);

		var loggerConfiguration = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Application", applicationName)
				.Filter.ControlledBy(filterSwitch)
				.MinimumLevel.ControlledBy(levelSwitch)
				.WriteTo.Seq(seq);

		Log.Logger = loggerConfiguration.CreateLogger();

		AddChildLogger(services, Log.Logger);

		Log.Logger.Write(LogEventLevel.Information, "Starting: {ApplicationName} in {EnvironmentName}",
			applicationName, HostEnvironment.EnvironmentName);
		Log.Logger.Debug("Debug Logging Enabled");
		Log.Logger.Verbose("Verbose Logging Enabled");
	}

	public static IServiceCollection AddChildLogger(this IServiceCollection services, Serilog.ILogger? logger = null)
	{
		services.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(logger, false));

		// Registered to provide two services...
		var diagnosticContext = new DiagnosticContext(logger ?? Log.Logger);

		// Consumed by e.g. middleware
		services.AddSingleton(diagnosticContext);

		// Consumed by user code
		services.AddSingleton<IDiagnosticContext>(diagnosticContext);

		return services;
	}

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
		var host = configuration.GetHostConfiguration();
		SelfLogProperties.SelfLogPath = host.SelfLogPath;
		Serilog.Debugging.SelfLog.Enable(SelfLog.WriteLine);

		builder.Services.AddEzrieLogging(configuration, applicationName);

		builder.AddSerilog();

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
