namespace Ezrie.Logging;

public static class SelfLogProperties
{
	public static String? SelfLogPath { get; set; }
	public static Int32 MaximumRetries { get; set; } = 10;
}

