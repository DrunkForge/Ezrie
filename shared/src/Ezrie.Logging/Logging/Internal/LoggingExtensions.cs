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

using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Helpful extension methods on <see cref="ILogger"/>.
	/// </summary>
	internal static partial class LoggingExtensions
	{
		/// <summary>
		/// Returns a value stating whether the 'debug' log level is enabled.
		/// Returns false if the logger instance is null.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean IsDebugLevelEnabled([NotNullWhen(true)] this ILogger? logger)
		{
			return IsLogLevelEnabledCore(logger, LogLevel.Debug);
		}

		/// <summary>
		/// Returns a value stating whether the 'trace' log level is enabled.
		/// Returns false if the logger instance is null.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean IsTraceLevelEnabled([NotNullWhen(true)] this ILogger? logger)
		{
			return IsLogLevelEnabledCore(logger, LogLevel.Trace);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Boolean IsLogLevelEnabledCore([NotNullWhen(true)] ILogger? logger, LogLevel level)
		{
			return (logger != null && logger.IsEnabled(level));
		}

		[LoggerMessage(39, LogLevel.Information, "Writing data to file '{FileName}'.", EventName = "WritingDataToFile")]
		public static partial void WritingDataToFile(this ILogger logger, string fileName);
	}
}
