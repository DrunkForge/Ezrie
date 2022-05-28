using Ezrie.IdentityService.Users;
using Ezrie.CRM.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Ezrie.CRM.Services;

// orig src https://github.com/berhir/BlazorWebAssemblyCookieAuth
public class HostAuthenticationStateProvider : AuthenticationStateProvider
{
	private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);

	private readonly NavigationManager _navigation;
	private readonly HttpClient _client;
	private readonly ILogger<HostAuthenticationStateProvider> _logger;

	private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
	private ClaimsPrincipal _cachedUser = new(new ClaimsIdentity());

	public HostAuthenticationStateProvider(NavigationManager navigation, HttpClient client, ILogger<HostAuthenticationStateProvider> logger)
	{
		_navigation = navigation;
		_client = client;
		_logger = logger;

		_logger.LogDebug("I'm alive!");
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		_logger.LogDebug("GetAuthenticationStateAsync()");
		return new AuthenticationState(await GetUser(useCache: true));
	}

	public void SignIn(String? customReturnUrl = null)
	{
		_logger.LogDebug("SignIn(customReturnUrl: {ReturnUrl})", customReturnUrl);
		var returnUrl = customReturnUrl != null ? _navigation.ToAbsoluteUri(customReturnUrl).ToString() : null;
		var encodedReturnUrl = Uri.EscapeDataString(returnUrl ?? _navigation.Uri);
		var logInUrl = _navigation.ToAbsoluteUri($"{AuthProperties.LogInPath}?returnUrl={encodedReturnUrl}");
		_navigation.NavigateTo(logInUrl.ToString(), true);
	}

	private async ValueTask<ClaimsPrincipal> GetUser(Boolean useCache = false)
	{
		_logger.LogDebug("GetUser(useCache: {UseCache})", useCache);
		var now = DateTimeOffset.Now;
		if (useCache && now < _userLastCheck + UserCacheRefreshInterval)
		{
			_logger.LogDebug("Taking user from cache");
			return _cachedUser;
		}

		_logger.LogDebug("Fetching user");
		_cachedUser = await FetchUser();
		_userLastCheck = now;

		return _cachedUser;
	}

	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is logged and it doesn't matter why it failed.")]
	private async Task<ClaimsPrincipal> FetchUser()
	{
		UserInfoDto? user = null;

		try
		{
			_logger.LogInformation("Base Address: {BaseUrl}", _client.BaseAddress);
			user = await _client.GetFromJsonAsync<UserInfoDto>("api/identity/user/current");
		}
		catch (HttpRequestException exc)
		{
			_logger.LogWarning(exc, "Fetching user failed.");
		}

		if (user == null || !user.IsAuthenticated)
		{
			return new ClaimsPrincipal(new ClaimsIdentity());
		}

		var identity = new ClaimsIdentity(
			nameof(HostAuthenticationStateProvider),
			user.NameClaimType,
			user.RoleClaimType);

		if (user.Claims != null)
		{
			foreach (var claim in user.Claims)
			{
				identity.AddClaim(new Claim(claim.Type, claim.Value));
			}
		}

		return new ClaimsPrincipal(identity);
	}
}
