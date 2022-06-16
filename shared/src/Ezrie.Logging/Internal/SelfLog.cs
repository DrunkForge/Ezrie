namespace Ezrie.Logging.Internal;

internal static class SelfLog
{
	private static readonly Object SyncRoot = new();
	private static Int32 FailCount;

	private static readonly FileStreamOptions Options = new()
	{
		Access = FileAccess.Write,
		Mode = FileMode.Append,
		Share = FileShare.Read,
		Options = FileOptions.WriteThrough
	};

	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "This is a last resort to log an error. If it fails, there's nothing that can be done but it's definitely not worth terminating the application because of it. -dw")]
	public static void WriteLine(String message)
	{
		Console.Error.WriteLine($"SELFLOG: {message}");

		if (String.IsNullOrWhiteSpace(SelfLogProperties.SelfLogPath))
			return;

		try
		{
			if (!String.IsNullOrWhiteSpace(SelfLogProperties.SelfLogPath))
			{
				lock (SyncRoot)
				{
					if (!String.IsNullOrWhiteSpace(SelfLogProperties.SelfLogPath))
					{
						using (var file = new StreamWriter(SelfLogProperties.SelfLogPath, System.Text.Encoding.UTF8, Options))
						{
							file.WriteLine(message);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			FailCount++;

			Console.Error.WriteLine($"SELFLOG: An error occured writing to `{SelfLogProperties.SelfLogPath}`. {ex.GetType().Name}: {ex.Message}");
			if (FailCount >= SelfLogProperties.MaximumRetries)
			{
				// It's not working so don't try anymore.
				Console.Error.WriteLine($"SELFLOG: After {FailCount} failures, no futher attempts will be made to write to `{SelfLogProperties.SelfLogPath}`.");

				SelfLogProperties.SelfLogPath = null!;
			}
		}
	}
}
