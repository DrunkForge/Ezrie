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

namespace Ezrie.Configuration;

public class SwaggerOptions
{
	public String ApiName { get; set; } = String.Empty;
	public String ApiVersion { get; set; } = "v1";
	public String Authority { get; set; } = String.Empty;
	public String ClientId { get; set; } = String.Empty;
	public String? ClientSecret { get; set; }

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String EndPointUrl { get; set; } = "/swagger/v1/swagger.json";

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public Scope[] Scopes { get; set; } = Array.Empty<Scope>();
}

public class Scope
{
	public String Id { get; set; } = null!;
	public String Name { get; set; } = null!;
}