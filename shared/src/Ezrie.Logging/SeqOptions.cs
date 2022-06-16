using Serilog.Core;
using Serilog.Events;

namespace Ezrie.Logging;

public class SeqOptions
{
	public const String DefaultServerUrl = "http://localhost:5341";

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Doesn't work with IConfiguration binding.")]
	public String ServerUrl { get; set; } = DefaultServerUrl;
	public LogEventLevel RestrictedToMinimumLevel { get; set; } = LogEventLevel.Verbose;
	public Int32 BatchPostingLimit { get; set; } = 1000;
	public TimeSpan? Period { get; set; }
	public String? ApiKey { get; set; }
	public String? BufferBaseFilename { get; set; }
	public Int64? BufferSizeLimitBytes { get; set; }
	public Int64? EventBodyLimitBytes { get; set; } = 262144L;
	public LoggingLevelSwitch? ControlLevelSwitch { get; set; }
	public HttpMessageHandler? MessageHandler { get; set; }
	public Int64? RetainedInvalidPayloadsLimitBytes { get; set; }
	public Int32 QueueSizeLimit { get; set; } = 100000;
}
