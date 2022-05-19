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

public class AppConfiguration
{

	// Swagger
	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String ApiBaseUrl { get; set; } = String.Empty;
	public String ApiName { get; set; } = String.Empty;
	public String ApiVersion { get; set; } = "v1";

	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String BaseUrl { get; set; } = String.Empty;

	public String OidcApiName { get; set; } = String.Empty;
	public String ClientId { get; set; } = String.Empty;
	public String? ClientName { get; set; }
	public String ClientSecret { get; set; } = String.Empty;

	// Cors
	public Boolean CorsAllowAnyOrigin { get; set; }
	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] CorsAllowOrigins { get; set; } = Array.Empty<String>();

	// Identity
	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String IdentityServerBaseUrl { get; set; } = String.Empty;
	public String IdentityAdminCookieName { get; set; } = String.Empty;
	public Double IdentityAdminCookieExpiresUtcHours { get; set; }
	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String IdentityAdminRedirectUri { get; set; } = String.Empty;
	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] Scopes { get; set; } = Array.Empty<String>();
	public Boolean RequireHttpsMetadata { get; set; } = true;
	public String OidcResponseType { get; set; } = "code";

	// Authorization
	public String AdministrationRole { get; set; } = String.Empty;
	public String TokenValidationClaimName { get; set; } = String.Empty;
	public String TokenValidationClaimRole { get; set; } = String.Empty;

	public Boolean HideUIForMSSqlErrorLogging { get; set; }

	// Theme
	public String PageTitle { get; set; } = String.Empty;
	[SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't play nice with IConfiguration")]
	public String FaviconUri { get; set; } = String.Empty;
	public String Theme { get; set; } = String.Empty;
	public String CustomThemeCss { get; set; } = String.Empty;
}

