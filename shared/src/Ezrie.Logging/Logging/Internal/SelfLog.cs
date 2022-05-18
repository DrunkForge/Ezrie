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

	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "This is a last resort to log an error. If it fails, there's not much that can be done but it's definitely not worth terminating the application because of it. -dw")]
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
