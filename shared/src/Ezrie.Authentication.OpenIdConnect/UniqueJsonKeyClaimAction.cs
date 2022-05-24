// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Ezrie.Authentication;

/// <summary>
/// A ClaimAction that selects a top level value from the json user data with the given key name and adds it as a Claim.
/// This no-ops if the ClaimsIdentity already contains a Claim with the given ClaimType.
/// This no-ops if the key is not found or the value is empty.
/// </summary>
public class UniqueJsonKeyClaimAction : JsonKeyClaimAction
{
	/// <summary>
	/// Creates a new UniqueJsonKeyClaimAction.
	/// </summary>
	/// <param name="claimType">The value to use for Claim.Type when creating a Claim.</param>
	/// <param name="valueType">The value to use for Claim.ValueType when creating a Claim.</param>
	/// <param name="jsonKey">The top level key to look for in the json user data.</param>
	public UniqueJsonKeyClaimAction(String claimType, String valueType, String jsonKey)
		: base(claimType, valueType, jsonKey)
	{
	}

	/// <inheritdoc />
	public override void Run(JsonElement userData, ClaimsIdentity identity, String issuer)
	{
		var value = userData.GetString(JsonKey);
		if (String.IsNullOrEmpty(value))
		{
			// Not found
			return;
		}

		var claim = identity.FindFirst(c => String.Equals(c.Type, JsonKey, StringComparison.OrdinalIgnoreCase));
		if (claim != null && String.Equals(claim.Value, value, StringComparison.Ordinal))
		{
			// Duplicate
			return;
		}

		claim = identity.FindFirst(c =>
		{
			// If this claimType is mapped by the JwtSeurityTokenHandler, then this property will be set
			return c.Properties.TryGetValue(JwtSecurityTokenHandler.ShortClaimTypeProperty, out var shortType)
				&& String.Equals(shortType, JsonKey, StringComparison.OrdinalIgnoreCase);
		});
		if (claim != null && String.Equals(claim.Value, value, StringComparison.Ordinal))
		{
			// Duplicate with an alternate name.
			return;
		}

		identity.AddClaim(new Claim(ClaimType, value, ValueType, issuer));
	}
}
