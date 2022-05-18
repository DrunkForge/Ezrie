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
using Microsoft.Extensions.Hosting;

namespace Ezrie.EntityFrameworkCore;

public static class HostBuilderExtensions
{
	public const String IdentitySeedPath = "appsettings.identity.json";

	public static IHostBuilder AddIdentitySeedJson(
		this IHostBuilder hostBuilder,
		Boolean optional = true,
		Boolean reloadOnChange = true,
		String path = IdentitySeedPath)
	{
		return hostBuilder.ConfigureAppConfiguration((_, builder) =>
		{
			builder.AddJsonFile(
				path: path,
				optional: optional,
				reloadOnChange: reloadOnChange
			);
		});
	}
}
