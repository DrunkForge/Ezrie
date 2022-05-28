using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Ezrie.AccountManagement.STS.Helpers;

public class DelegationGrantValidator : IExtensionGrantValidator
{
	private readonly ITokenValidator _validator;

	public DelegationGrantValidator(ITokenValidator validator)
	{
		_validator = validator;
	}

	public String GrantType => "delegation";

	public async Task ValidateAsync(ExtensionGrantValidationContext context)
	{
		var userToken = context.Request.Raw.Get("token");

		if (String.IsNullOrEmpty(userToken))
		{
			context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
			return;
		}

		var result = await _validator.ValidateAccessTokenAsync(userToken);
		if (result.IsError)
		{
			context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
			return;
		}

		// get user's identity
		var sub = result.Claims.FirstOrDefault(c => c.Type == "sub").Value;

		context.Result = new GrantValidationResult(sub, GrantType);
		return;
	}
}
