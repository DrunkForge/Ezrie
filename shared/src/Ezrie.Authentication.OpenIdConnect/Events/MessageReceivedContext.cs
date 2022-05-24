// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Ezrie.Authentication.Events;

/// <summary>
/// A context for <see cref="OpenIdConnectEvents.OnMessageReceived"/>.
/// </summary>
public class MessageReceivedContext : RemoteAuthenticationContext<OpenIdConnectOptions>
{
	/// <summary>
	/// Initializes a new instance of <see cref="MessageReceivedContext"/>.
	/// </summary>
	/// <inheritdoc />
	public MessageReceivedContext(
		HttpContext context,
		AuthenticationScheme scheme,
		OpenIdConnectOptions options,
		AuthenticationProperties? properties)
		: base(context, scheme, options, properties) { }

	/// <summary>
	/// Gets or sets the <see cref="OpenIdConnectMessage"/>.
	/// </summary>
	public OpenIdConnectMessage ProtocolMessage { get; set; } = default!;

	/// <summary>
	/// Bearer Token. This will give the application an opportunity to retrieve a token from an alternative location.
	/// </summary>
	public String? Token { get; set; }
}
