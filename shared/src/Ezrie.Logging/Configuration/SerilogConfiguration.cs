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

using Serilog.Events;

namespace Ezrie.Configuration;

public class SerilogConfiguration
{
	public const String SectionName = "Serilog";
	public MinimumLevel MinimumLevel { get; set; } = new();
}

public class MinimumLevel
{
	public LogEventLevel Default { get; set; } = LogEventLevel.Information;

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] Override { get; set; } = Array.Empty<String>();
}
