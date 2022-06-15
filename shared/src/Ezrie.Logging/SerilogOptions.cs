using Serilog.Events;
using System.Diagnostics.CodeAnalysis;

namespace Ezrie.Logging;

internal class SerilogOptions
{
	public const String SectionName = "Serilog";
	public MinimumLevel MinimumLevel { get; set; } = new();
}

internal class MinimumLevel
{
	public LogEventLevel Default { get; set; } = LogEventLevel.Information;

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] Override { get; set; } = Array.Empty<String>();
}
