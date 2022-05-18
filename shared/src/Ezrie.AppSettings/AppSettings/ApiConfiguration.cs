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

namespace Ezrie.AppSettings;

public class ApiConfiguration
{
	public String AdministrationRole { get; set; } = String.Empty;

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String ApiBaseUrl { get; set; } = String.Empty;

	public String ApiName { get; set; } = String.Empty;

	public String ApiVersion { get; set; } = "v1";

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String IdentityServerBaseUrl { get; set; } = String.Empty;

	public String OidcApiName { get; set; } = String.Empty;

	public String OidcSwaggerUIClientId { get; set; } = String.Empty;
	public String? OidcSwaggerUIClientName { get; set; }

	public String OidcSwaggerUIClientSecret { get; set; } = String.Empty;

	public Boolean RequireHttpsMetadata { get; set; } = true;

	public Boolean CorsAllowAnyOrigin { get; set; }

	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] CorsAllowOrigins { get; } = Array.Empty<String>();
}
