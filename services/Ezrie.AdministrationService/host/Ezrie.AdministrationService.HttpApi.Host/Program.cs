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
using Serilog;

namespace Ezrie.AdministrationService;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
	public async static Task<Int32> Main(String[] args)
	{
		try
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.AddAppSettingsSecretsJson()
				.UseAutofac()
				.UseEzrieLogging< AdministrationServiceHttpApiHostModule>();

			await builder.AddApplicationAsync<AdministrationServiceHttpApiHostModule>();

			var app = builder.Build();

			await app.InitializeApplicationAsync();

			await app.RunAsync();

			return 0;
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Host terminated unexpectedly!");
			return 1;
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}
}
