using Ezrie.Configuration;
using Ezrie.Logging;
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

public static class EzrieSerilogExtensions
{
	public static IHostBuilder UseEzrieLogging<T>(this IHostBuilder hostBuilder)
		=> hostBuilder.UseEzrieLogging(typeof(T).Assembly.GetName().Name ?? typeof(T).Name);

	public static IHostBuilder UseEzrieLogging(this IHostBuilder hostBuilder, String applicationName)
		=> hostBuilder.ConfigureServices((context, services) => services.AddEzrieLogging(context.Configuration, applicationName));

	public static IServiceCollection AddEzrieLogging<T>(this IServiceCollection services)
		=> AddEzrieLogging(services, services.GetConfiguration(), typeof(T).Assembly.GetName().Name ?? typeof(T).Name);

	public static IServiceCollection AddEzrieLogging(this IServiceCollection services, IConfiguration configuration, String applicationName)
	{
		ArgumentNullException.ThrowIfNull(services);
		if (applicationName.IsNullOrWhiteSpace())
			throw new ArgumentException($"The '{nameof(applicationName)}' parameter cannot be null or whitespace.", nameof(applicationName));

		var level = configuration.GetSerilogMinimumLevel();
		var seqOptions = configuration.GetSeq();

		// Initially include all log entries with a filter of `true`
		var filterSwitch = new LoggingFilterSwitch("true");
		var levelSwitch = new LoggingLevelSwitch(level);
		seqOptions.ControlLevelSwitch = levelSwitch;

		Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Application", applicationName)
				.Filter.ControlledBy(filterSwitch)
				.MinimumLevel.ControlledBy(levelSwitch)
				.WriteTo.Seq(seqOptions).CreateLogger();

		services.AddSingleton(filterSwitch);
		services.AddSingleton(levelSwitch);
		services.AddDiagnosticContext();

		Log.Logger.Write(LogEventLevel.Information, "Starting: {ApplicationName} in {EnvironmentName}",
			applicationName, HostEnvironment.EnvironmentName);
		Log.Logger.Debug("Debug Logging Enabled");
		Log.Logger.Verbose("Verbose Logging Enabled");

		services.AddLogging(builder => builder.AddSerilog(dispose: true));

		return services;
	}

	public static IServiceCollection AddDiagnosticContext(this IServiceCollection services, Serilog.ILogger? logger = null)
	{
		// Registered to provide two services...
		var diagnosticContext = new DiagnosticContext(logger ?? Log.Logger);

		// Consumed by e.g. middleware
		services.AddSingleton(diagnosticContext);

		// Consumed by user code
		services.AddSingleton<IDiagnosticContext>(diagnosticContext);

		return services;
	}

	public static LoggerConfiguration Seq(this LoggerSinkConfiguration loggerConfig, SeqOptions options)
	{
		ArgumentNullException.ThrowIfNull(loggerConfig);

		return loggerConfig.Seq(
			serverUrl: options.ServerUrl,
			restrictedToMinimumLevel: options.RestrictedToMinimumLevel,
			batchPostingLimit: options.BatchPostingLimit,
			period: options.Period,
			apiKey: options.ApiKey,
			bufferBaseFilename: options.BufferBaseFilename,
			bufferSizeLimitBytes: options.BufferSizeLimitBytes,
			eventBodyLimitBytes: options.EventBodyLimitBytes,
			controlLevelSwitch: options.ControlLevelSwitch,
			messageHandler: options.MessageHandler,
			retainedInvalidPayloadsLimitBytes: options.RetainedInvalidPayloadsLimitBytes,
			queueSizeLimit: options.QueueSizeLimit
		);
	}

	private static LogEventLevel GetSerilogMinimumLevel(this IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		var settings = configuration.GetSection(SerilogOptions.SectionName).Get<SerilogOptions>() ?? new();

		return settings.MinimumLevel.Default;
	}

	private static SeqOptions GetSeq(this IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);
		var options = new SeqOptions();

		configuration.Bind(nameof(SeqOptions), options);

		return options;
	}
}
