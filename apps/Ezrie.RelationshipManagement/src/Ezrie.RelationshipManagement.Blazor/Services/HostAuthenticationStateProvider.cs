using System.Net.Http.Json;
using System.Security.Claims;
using Ezrie.RelationshipManagement.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Ezrie.RelationshipManagement.Services
{
	// orig src https://github.com/berhir/BlazorWebAssemblyCookieAuth
	public class HostAuthenticationStateProvider : AuthenticationStateProvider
	{
		private static readonly TimeSpan _userCacheRefreshInterval = TimeSpan.FromSeconds(60);

		private const String LogInPath = "api/Account/Login";

		private readonly NavigationManager _navigation;
		private readonly HttpClient _client;
		private readonly ILogger<HostAuthenticationStateProvider> _logger;

		private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
		private ClaimsPrincipal _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());

		public HostAuthenticationStateProvider(NavigationManager navigation, HttpClient client, ILogger<HostAuthenticationStateProvider> logger)
		{
			_navigation = navigation;
			_client = client;
			_logger = logger;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			return new AuthenticationState(await GetUser(useCache: true));
		}

		public void SignIn(String? customReturnUrl = null)
		{
			var returnUrl = customReturnUrl != null ? _navigation.ToAbsoluteUri(customReturnUrl).ToString() : null;
			var encodedReturnUrl = Uri.EscapeDataString(returnUrl ?? _navigation.Uri);
			var logInUrl = _navigation.ToAbsoluteUri($"{LogInPath}?returnUrl={encodedReturnUrl}");
			_navigation.NavigateTo(logInUrl.ToString(), true);
		}

		private async ValueTask<ClaimsPrincipal> GetUser(Boolean useCache = false)
		{
			var now = DateTimeOffset.Now;
			if (useCache && now < _userLastCheck + _userCacheRefreshInterval)
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
			UserInfo? user = null;

			try
			{
				_logger.LogInformation("Base Address: {BaseAddress}", _client.BaseAddress);
				user = await _client.GetFromJsonAsync<UserInfo>("api/User");
			}
			catch (Exception exc)
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
}
