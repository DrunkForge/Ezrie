using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;

namespace Ezrie.IdentityService.AspNetCore;
public class EzrieSignInManager : AbpSignInManager
{
	private const String LoginProviderKey = "LoginProvider";
	private const String XsrfKey = "XsrfId";

	public EzrieSignInManager(
		IdentityUserManager userManager,
		IHttpContextAccessor contextAccessor,
		IUserClaimsPrincipalFactory<Volo.Abp.Identity.IdentityUser> claimsFactory,
		IOptions<IdentityOptions> optionsAccessor,
		ILogger<SignInManager<Volo.Abp.Identity.IdentityUser>> logger,
		IAuthenticationSchemeProvider schemes,
		IUserConfirmation<Volo.Abp.Identity.IdentityUser> confirmation,
		IOptions<AbpIdentityOptions> options)
			: base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation, options)
	{
	}

	public async override Task<ExternalLoginInfo> GetExternalLoginInfoAsync(String expectedXsrf = null)
	{
		var auth = await Context.AuthenticateAsync(IdentityConstants.ExternalScheme);

		var items = auth?.Properties?.Items;

		if (auth?.Principal == null || items == null || !items.ContainsKey(LoginProviderKey))
		{
			return null;
		}

		if (expectedXsrf != null)
		{
			if (!items.ContainsKey(XsrfKey))
			{
				return null;
			}

			var userId = items[XsrfKey] as String;

			if (userId != expectedXsrf)
			{
				return null;
			}
		}

		var providerKey = auth.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

		if (providerKey == null || items[LoginProviderKey] is not String provider)
		{
			return null;
		}

		var autheSchemes = await GetExternalAuthenticationSchemesAsync();

		var providerDisplayName = autheSchemes.FirstOrDefault(p => p.Name == provider)?.DisplayName ?? provider;

		return new ExternalLoginInfo(auth.Principal, provider, providerKey, providerDisplayName)
		{
			AuthenticationTokens = auth.Properties.GetTokens(),
			AuthenticationProperties = auth.Properties
		};
	}
}
