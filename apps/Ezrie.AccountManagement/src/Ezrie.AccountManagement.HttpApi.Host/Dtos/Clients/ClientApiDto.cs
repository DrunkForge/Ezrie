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

using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.Dtos.Clients;

public class ClientApiDto
{
	public ClientApiDto()
	{
		AllowedScopes = new List<String>();
		PostLogoutRedirectUris = new List<String>();
		RedirectUris = new List<String>();
		IdentityProviderRestrictions = new List<String>();
		AllowedCorsOrigins = new List<String>();
		AllowedGrantTypes = new List<String>();
		Claims = new List<ClientClaimApiDto>();
		Properties = new List<ClientPropertyApiDto>();
	}

	public Int32 AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
	public Int32 AccessTokenLifetime { get; set; } = 3600;

	public Int32? ConsentLifetime { get; set; }

	public Int32 AccessTokenType { get; set; }

	public Boolean AllowAccessTokensViaBrowser { get; set; }
	public Boolean AllowOfflineAccess { get; set; }
	public Boolean AllowPlainTextPkce { get; set; }
	public Boolean AllowRememberConsent { get; set; } = true;
	public Boolean AlwaysIncludeUserClaimsInIdToken { get; set; }
	public Boolean AlwaysSendClientClaims { get; set; }
	public Int32 AuthorizationCodeLifetime { get; set; } = 300;

	public String FrontChannelLogoutUri { get; set; }
	public Boolean FrontChannelLogoutSessionRequired { get; set; } = true;
	public String BackChannelLogoutUri { get; set; }
	public Boolean BackChannelLogoutSessionRequired { get; set; } = true;

	[Required]
	public String ClientId { get; set; }

	[Required]
	public String ClientName { get; set; }

	public String ClientUri { get; set; }

	public String Description { get; set; }

	public Boolean Enabled { get; set; } = true;
	public Boolean EnableLocalLogin { get; set; } = true;
	public Int32 Id { get; set; }
	public Int32 IdentityTokenLifetime { get; set; } = 300;
	public Boolean IncludeJwtId { get; set; }
	public String LogoUri { get; set; }

	public String ClientClaimsPrefix { get; set; } = "client_";

	public String PairWiseSubjectSalt { get; set; }

	public String ProtocolType { get; set; } = "oidc";

	public Int32 RefreshTokenExpiration { get; set; } = 1;

	public Int32 RefreshTokenUsage { get; set; } = 1;

	public Int32 SlidingRefreshTokenLifetime { get; set; } = 1296000;

	public Boolean RequireClientSecret { get; set; } = true;
	public Boolean RequireConsent { get; set; } = true;
	public Boolean RequirePkce { get; set; }
	public Boolean UpdateAccessTokenClaimsOnRefresh { get; set; }

	public List<String> PostLogoutRedirectUris { get; set; }

	public List<String> IdentityProviderRestrictions { get; set; }

	public List<String> RedirectUris { get; set; }

	public List<String> AllowedCorsOrigins { get; set; }

	public List<String> AllowedGrantTypes { get; set; }

	public List<String> AllowedScopes { get; set; }

	public List<ClientClaimApiDto> Claims { get; set; }
	public List<ClientPropertyApiDto> Properties { get; set; }

	public DateTime? Updated { get; set; }
	public DateTime? LastAccessed { get; set; }

	public Int32? UserSsoLifetime { get; set; }
	public String UserCodeType { get; set; }
	public Int32 DeviceCodeLifetime { get; set; } = 300;

	public Boolean RequireRequestObject { get; set; }

	public List<String> AllowedIdentityTokenSigningAlgorithms { get; set; }

	public Boolean NonEditable { get; set; }
}

