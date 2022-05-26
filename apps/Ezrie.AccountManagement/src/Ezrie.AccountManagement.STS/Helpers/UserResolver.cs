using Microsoft.AspNetCore.Identity;
using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;

namespace Ezrie.AccountManagement.STS.Helpers;

public class UserResolver<TUser> where TUser : class
{
	private readonly UserManager<TUser> _userManager;
	private readonly LoginResolutionPolicy _policy;

	public UserResolver(UserManager<TUser> userManager, LoginConfiguration configuration)
	{
		_userManager = userManager;
		_policy = configuration.ResolutionPolicy;
	}

	public async Task<TUser?> GetUserAsync(String login) => _policy switch
	{
		LoginResolutionPolicy.Username => await _userManager.FindByNameAsync(login),
		LoginResolutionPolicy.Email => await _userManager.FindByEmailAsync(login),
		_ => null,
	};
}

