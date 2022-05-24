// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ezrie.Authentication;

/// <summary>
/// Default values related to OpenIdConnect authentication handler
/// </summary>
public static class OpenIdConnectDefaults
{
	/// <summary>
	/// Constant used to identify state in openIdConnect protocol message.
	/// </summary>
	public static readonly String AuthenticationPropertiesKey = "OpenIdConnect.AuthenticationProperties";

	/// <summary>
	/// The default value used for OpenIdConnectOptions.AuthenticationScheme.
	/// </summary>
	public const String AuthenticationScheme = "OpenIdConnect";

	/// <summary>
	/// The default value for the display name.
	/// </summary>
	public static readonly String DisplayName = "OpenIdConnect";

	/// <summary>
	/// The prefix used to for the nonce in the cookie.
	/// </summary>
	public static readonly String CookieNoncePrefix = ".AspNetCore.OpenIdConnect.Nonce.";

	/// <summary>
	/// The property for the RedirectUri that was used when asking for a 'authorizationCode'.
	/// </summary>
	public static readonly String RedirectUriForCodePropertiesKey = "OpenIdConnect.Code.RedirectUri";

	/// <summary>
	/// Constant used to identify userstate inside AuthenticationProperties that have been serialized in the 'state' parameter.
	/// </summary>
	public static readonly String UserstatePropertiesKey = "OpenIdConnect.Userstate";
}
