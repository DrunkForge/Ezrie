using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;

namespace Ezrie.AccountManagement.STS.Helpers.Localization;

public static class LoginPolicyResolutionLocalizer
{
	public static String GetUserNameLocalizationKey(LoginResolutionPolicy policy)
	{
		switch (policy)
		{
			case LoginResolutionPolicy.Username:
				return "Username";
			case LoginResolutionPolicy.Email:
				return "Email";
			default:
				return "Username";
		}
	}
}

