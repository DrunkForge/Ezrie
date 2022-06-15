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

using Microsoft.Extensions.Configuration;

namespace Ezrie.Configuration;

public static class EzrieConfiguration
{
	public static IConfiguration CreateDefault() => CreateBuilder().Build();

	public static IConfigurationBuilder CreateBuilder()
	{
		var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

		return new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
			.AddJsonFile("serilog.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"serilog.{environment}.json", optional: true, reloadOnChange: true);
	}
}
