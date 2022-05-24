// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ezrie.Authentication;

/// <summary>
/// Configuration options for <see cref="EzrieAccountHandler"/>.
/// </summary>
public class EzrieAccountOptions : OAuthOptions
{
	/// <summary>
	/// Initializes a new <see cref="EzrieAccountOptions"/>.
	/// </summary>
	public EzrieAccountOptions()
	{
		CallbackPath = new PathString("/signin-microsoft");
		AuthorizationEndpoint = EzrieAccountDefaults.AuthorizationEndpoint;
		TokenEndpoint = EzrieAccountDefaults.TokenEndpoint;
		UserInformationEndpoint = EzrieAccountDefaults.UserInformationEndpoint;
		UsePkce = true;
		Scope.Add("https://graph.microsoft.com/user.read");

		ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
		ClaimActions.MapJsonKey(ClaimTypes.Name, "displayName");
		ClaimActions.MapJsonKey(ClaimTypes.GivenName, "givenName");
		ClaimActions.MapJsonKey(ClaimTypes.Surname, "surname");
		ClaimActions.MapCustomJson(ClaimTypes.Email, user => user.GetString("mail") ?? user.GetString("userPrincipalName"));
	}

	public Boolean UsePkce { get; set; }
}
