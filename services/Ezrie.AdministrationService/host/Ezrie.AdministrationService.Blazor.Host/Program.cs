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

using Ezrie.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ezrie.AdministrationService;

public static class Program
{
	public async static Task Main(String[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.UseEzrieLogging<AdministrationServiceApplicationContractsModule>();

		var application = await builder.AddApplicationAsync<AdministrationServiceBlazorHostModule>(options =>
		{
			options.UseAutofac();
		});

		var host = builder.Build();

		await application.InitializeApplicationAsync(host.Services);

		await host.RunAsync();
	}
}
