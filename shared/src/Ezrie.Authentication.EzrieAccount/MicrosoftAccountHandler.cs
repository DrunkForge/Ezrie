// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Ezrie.Authentication;

/// <summary>
/// Authentication handler for Ezrie Account based authentication.
/// </summary>
public class EzrieAccountHandler : OAuthHandler<EzrieAccountOptions>
{
	/// <summary>
	/// Initializes a new instance of <see cref="EzrieAccountHandler"/>.
	/// </summary>
	/// <inheritdoc />
	public EzrieAccountHandler(IOptionsMonitor<EzrieAccountOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
		: base(options, logger, encoder, clock)
	{ }

	/// <inheritdoc />
	protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

		var response = await Backchannel.SendAsync(request, Context.RequestAborted);
		if (!response.IsSuccessStatusCode)
		{
			throw new HttpRequestException($"An error occurred when retrieving Ezrie user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Ezrie Account API is enabled.");
		}

		using (var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync(Context.RequestAborted)))
		{
			var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload.RootElement);
			context.RunClaimActions();
			await Events.CreatingTicket(context);
			return new AuthenticationTicket(context.Principal!, context.Properties, Scheme.Name);
		}
	}

	/// <inheritdoc />
	protected override String BuildChallengeUrl(AuthenticationProperties properties, String redirectUri)
	{
		var queryStrings = new Dictionary<String, String>
		{
			{ "client_id", Options.ClientId },
			{ "response_type", "code" },
			{ "redirect_uri", redirectUri }
		};

		AddQueryString(queryStrings, properties, OAuthChallengeProperties.ScopeKey, FormatScope, Options.Scope);
#pragma warning disable CS0618 // Type or member is obsolete
		AddQueryString(queryStrings, properties, EzrieChallengeProperties.ResponseModeKey);
#pragma warning restore CS0618 // Type or member is obsolete
		AddQueryString(queryStrings, properties, EzrieChallengeProperties.DomainHintKey);
		AddQueryString(queryStrings, properties, EzrieChallengeProperties.LoginHintKey);
		AddQueryString(queryStrings, properties, EzrieChallengeProperties.PromptKey);

		if (Options.UsePkce)
		{
			var bytes = new Byte[32];
			RandomNumberGenerator.Fill(bytes);
			var codeVerifier = Microsoft.AspNetCore.Authentication.Base64UrlTextEncoder.Encode(bytes);

			// Store this for use during the code redemption.
			properties.Items.Add(OAuthConstants.CodeVerifierKey, codeVerifier);

			var challengeBytes = SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier));
			var codeChallenge = WebEncoders.Base64UrlEncode(challengeBytes);

			queryStrings[OAuthConstants.CodeChallengeKey] = codeChallenge;
			queryStrings[OAuthConstants.CodeChallengeMethodKey] = OAuthConstants.CodeChallengeMethodS256;
		}

		var state = Options.StateDataFormat.Protect(properties);
		queryStrings.Add("state", state);

		return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryStrings!);
	}

	private static void AddQueryString<T>(
	   Dictionary<String, String> queryStrings,
	   AuthenticationProperties properties,
	   String name,
	   Func<T, String> formatter,
	   T defaultValue)
	{
		String? value;
		var parameterValue = properties.GetParameter<T>(name);
		if (parameterValue != null)
		{
			value = formatter(parameterValue);
		}
		else if (!properties.Items.TryGetValue(name, out value))
		{
			value = formatter(defaultValue);
		}

		// Remove the parameter from AuthenticationProperties so it won't be serialized into the state
		properties.Items.Remove(name);

		if (value != null)
		{
			queryStrings[name] = value;
		}
	}

	private static void AddQueryString(
		Dictionary<String, String> queryStrings,
		AuthenticationProperties properties,
		String name,
		String? defaultValue = null)
		=> AddQueryString(queryStrings, properties, name, x => x!, defaultValue);
}
