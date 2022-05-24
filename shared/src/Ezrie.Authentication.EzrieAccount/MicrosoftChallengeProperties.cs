// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.OAuth;

namespace Ezrie.Authentication;

/// <summary>
/// <see cref="AuthenticationProperties"/> for Ezrie OAuth challenge request.
/// See https://docs.microsoft.com/azure/active-directory/develop/v2-oauth2-auth-code-flow#request-an-authorization-code for reference
/// </summary>
public class EzrieChallengeProperties : OAuthChallengeProperties
{
	/// <summary>
	/// The parameter key for the "response_mode" argument being used for a challenge request.
	/// </summary>
	[Obsolete("This parameter is not supported in EzrieAccountHandler.")]
	public static readonly String ResponseModeKey = "response_mode";

	/// <summary>
	/// The parameter key for the "domain_hint" argument being used for a challenge request.
	/// </summary>
	public static readonly String DomainHintKey = "domain_hint";

	/// <summary>
	/// The parameter key for the "login_hint" argument being used for a challenge request.
	/// </summary>
	public static readonly String LoginHintKey = "login_hint";

	/// <summary>
	/// The parameter key for the "prompt" argument being used for a challenge request.
	/// </summary>
	public static readonly String PromptKey = "prompt";

	/// <summary>
	/// Initializes a new instance for <see cref="EzrieChallengeProperties"/>.
	/// </summary>
	public EzrieChallengeProperties()
	{ }

	/// <summary>
	/// Initializes a new instance for <see cref="EzrieChallengeProperties"/>.
	/// </summary>
	/// <inheritdoc />
	public EzrieChallengeProperties(IDictionary<String, String?> items)
		: base(items)
	{ }

	/// <summary>
	/// Initializes a new instance for <see cref="EzrieChallengeProperties"/>.
	/// </summary>
	/// <inheritdoc />
	public EzrieChallengeProperties(IDictionary<String, String?> items, IDictionary<String, Object?> parameters)
		: base(items, parameters)
	{ }

	/// <summary>
	/// Gets or sets the value for the <c>response_mode</c> parameter used for a challenge request. The response mode specifies the method
	/// that should be used to send the resulting token back to the app. Can be one of the following: <c>query</c>, <c>fragment</c>, <c>form_post</c>.
	/// </summary>
	[Obsolete("This parameter is not supported in EzrieAccountHandler.")]
	public String? ResponseMode
	{
		get => GetParameter<String>(ResponseModeKey);
		set => SetParameter(ResponseModeKey, value);
	}

	/// <summary>
	/// Gets or sets the value for the "domain_hint" parameter value being used for a challenge request.
	/// <para>
	/// If included, authentication will skip the email-based discovery process that user goes through on the sign-in page,
	/// leading to a slightly more streamlined user experience.
	/// </para>
	/// </summary>
	public String? DomainHint
	{
		get => GetParameter<String>(DomainHintKey);
		set => SetParameter(DomainHintKey, value);
	}

	/// <summary>
	/// Gets or sets the value for the "login_hint" parameter value being used for a challenge request.
	/// <para>
	/// Can be used to pre-fill the username/email address field of the sign-in page for the user, if their username is known ahead of time.
	/// </para>
	/// </summary>
	public String? LoginHint
	{
		get => GetParameter<String>(LoginHintKey);
		set => SetParameter(LoginHintKey, value);
	}

	/// <summary>
	/// Gets or sets the value for the "prompt" parameter value being used for a challenge request.
	/// <para>
	/// Indicates the type of user interaction that is required. The only valid values at this time are login, none, and consent.
	/// </para>
	/// </summary>
	public String? Prompt
	{
		get => GetParameter<String>(PromptKey);
		set => SetParameter(PromptKey, value);
	}
}
