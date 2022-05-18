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

using Serilog.Core;
using Serilog.Events;

namespace Ezrie.AppSettings;

public class SeqSettings
{
	public const String SectionName = "Seq";
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
