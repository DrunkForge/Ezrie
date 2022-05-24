// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ezrie.Authentication;

/// <summary>
/// Default values for Ezrie account authentication
/// </summary>
public static class EzrieAccountDefaults
{
	/// <summary>
	/// The default scheme for Ezrie account authentication. Defaults to <c>Ezrie</c>.
	/// </summary>
	public const String AuthenticationScheme = "Ezrie";

	/// <summary>
	/// The default display name for Ezrie account authentication. Defaults to <c>Ezrie</c>.
	/// </summary>
	public static readonly String DisplayName = "Ezrie";

	/// <summary>
	/// The default endpoint used to perform Ezrie account authentication.
	/// </summary>
	/// <remarks>
	/// For more details about this endpoint, see https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user
	/// </remarks>
	public static readonly String AuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";

	/// <summary>
	/// The OAuth endpoint used to exchange access tokens.
	/// </summary>
	public static readonly String TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

	/// <summary>
	/// The Ezrie Graph API endpoint that is used to gather additional user information.
	/// </summary>
	public static readonly String UserInformationEndpoint = "https://graph.microsoft.com/v1.0/me";
}
