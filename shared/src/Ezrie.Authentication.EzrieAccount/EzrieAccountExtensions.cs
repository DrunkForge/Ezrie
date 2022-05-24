// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Ezrie.Authentication;

/// <summary>
/// Extension methods to configure Ezrie Account OAuth authentication.
/// </summary>
public static class EzrieAccountExtensions
{
	/// <summary>
	/// Adds Ezrie Account OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
	/// The default scheme is specified by <see cref="EzrieAccountDefaults.AuthenticationScheme"/>.
	/// <para>
	/// Ezrie Account authentication allows application users to sign in with their work, school, or personal Ezrie account.
	/// </para>
	/// </summary>
	/// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
	/// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
	public static AuthenticationBuilder AddEzrieAccount(this AuthenticationBuilder builder)
		=> builder.AddEzrieAccount(EzrieAccountDefaults.AuthenticationScheme, _ => { });

	/// <summary>
	/// Adds Ezrie Account OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
	/// The default scheme is specified by <see cref="EzrieAccountDefaults.AuthenticationScheme"/>.
	/// <para>
	/// Ezrie Account authentication allows application users to sign in with their work, school, or personal Ezrie account.
	/// </para>
	/// </summary>
	/// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
	/// <param name="configureOptions">A delegate to configure <see cref="EzrieAccountOptions"/>.</param>
	/// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
	public static AuthenticationBuilder AddEzrieAccount(this AuthenticationBuilder builder, Action<EzrieAccountOptions> configureOptions)
		=> builder.AddEzrieAccount(EzrieAccountDefaults.AuthenticationScheme, configureOptions);

	/// <summary>
	/// Adds Ezrie Account OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
	/// The default scheme is specified by <see cref="EzrieAccountDefaults.AuthenticationScheme"/>.
	/// <para>
	/// Ezrie Account authentication allows application users to sign in with their work, school, or personal Ezrie account.
	/// </para>
	/// </summary>
	/// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
	/// <param name="authenticationScheme">The authentication scheme.</param>
	/// <param name="configureOptions">A delegate to configure <see cref="EzrieAccountOptions"/>.</param>
	/// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
	public static AuthenticationBuilder AddEzrieAccount(this AuthenticationBuilder builder, String authenticationScheme, Action<EzrieAccountOptions> configureOptions)
		=> builder.AddEzrieAccount(authenticationScheme, EzrieAccountDefaults.DisplayName, configureOptions);

	/// <summary>
	/// Adds Ezrie Account OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
	/// The default scheme is specified by <see cref="EzrieAccountDefaults.AuthenticationScheme"/>.
	/// <para>
	/// Ezrie Account authentication allows application users to sign in with their work, school, or personal Ezrie account.
	/// </para>
	/// </summary>
	/// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
	/// <param name="authenticationScheme">The authentication scheme.</param>
	/// <param name="displayName">A display name for the authentication handler.</param>
	/// <param name="configureOptions">A delegate to configure <see cref="EzrieAccountOptions"/>.</param>
	/// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
	public static AuthenticationBuilder AddEzrieAccount(this AuthenticationBuilder builder, String authenticationScheme, String displayName, Action<EzrieAccountOptions> configureOptions)
		=> builder.AddOAuth<EzrieAccountOptions, EzrieAccountHandler>(authenticationScheme, displayName, configureOptions);
}
