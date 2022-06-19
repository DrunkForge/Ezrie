using Ezrie.Configuration;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;
using IdentityResource = Volo.Abp.IdentityServer.IdentityResources.IdentityResource;

namespace Ezrie.Admin.Seeding;

public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
	private static readonly IReadOnlyCollection<String> CommonApiUserClaims = new[]
	{
		"email",
		"email_verified",
		"name",
		"phone_number",
		"phone_number_verified",
		"role"
	};

	private static readonly IReadOnlyCollection<String> CommonScopes = new[]
	{
		"email",
		"openid",
		"profile",
		"role",
		"phone",
		"address",
		"Admin"
	};

	private readonly IApiResourceRepository _apiResourceRepository;
	private readonly IApiScopeRepository _apiScopeRepository;
	private readonly IIdentityResourceRepository _identityResourceRepository;
	private readonly IClientRepository _clientRepository;
	private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
	private readonly IGuidGenerator _guidGenerator;
	private readonly IPermissionDataSeeder _permissionDataSeeder;
	private readonly IConfiguration _configuration;
	private readonly ICurrentTenant _currentTenant;
	private readonly ILogger<IdentityServerDataSeedContributor> _logger;
	private readonly IdentityServerSeed _data = new();

	public IdentityServerDataSeedContributor(
		IConfiguration configuration,
		ICurrentTenant currentTenant,
		IGuidGenerator guidGenerator,
		IIdentityResourceDataSeeder identityResourceDataSeeder,
		IPermissionDataSeeder permissionDataSeeder,
		IApiResourceRepository apiResourceRepository,
		IApiScopeRepository apiScopeRepository,
		IClientRepository clientRepository,
		IIdentityResourceRepository identityResourceRepository,
		ILogger<IdentityServerDataSeedContributor> logger)
	{
		_configuration = configuration;
		_configuration.Bind(nameof(IdentityServerSeed), _data);

		_currentTenant = currentTenant;
		_guidGenerator = guidGenerator;
		_identityResourceDataSeeder = identityResourceDataSeeder;
		_permissionDataSeeder = permissionDataSeeder;
		_apiResourceRepository = apiResourceRepository;
		_apiScopeRepository = apiScopeRepository;
		_clientRepository = clientRepository;
		_identityResourceRepository = identityResourceRepository;
		_logger = logger;

	}

	/// <summary>
	/// Generate default clients, identity and api resources
	/// </summary>
	[UnitOfWork]
	public virtual async Task SeedAsync(DataSeedContext context)
	{
		using (_currentTenant.Change(context?.TenantId))
		{
			await EnsureIdentityResources();

			await EnsureApiScopes();

			await EnsureApiResources();

			await EnsureClients();
		}
	}

	private async Task EnsureIdentityResources()
	{
		await _identityResourceDataSeeder.CreateStandardResourcesAsync();

		foreach (var entry in _data.IdentityResources)
		{
			var identityResource = await _identityResourceRepository.FindByNameAsync(entry.Name);
			if (identityResource == null)
			{
				identityResource = await _identityResourceRepository.InsertAsync(
					new(
						_guidGenerator.Create(),
						entry.Name,
						entry.DisplayName
					),
					autoSave: true
				);
				_logger.LogInformation("Added IdentityResource: {Name} ({DisplayName})", identityResource.Name, identityResource.DisplayName);
			}
		}
	}

	private async Task EnsureApiScopes()
	{
		foreach (var entry in _data.ApiScopes)
		{
			var apiScope = await _apiScopeRepository.FindByNameAsync(entry.Name);
			if (apiScope == null)
			{
				apiScope = await _apiScopeRepository.InsertAsync(
					new ApiScope(
						_guidGenerator.Create(),
						entry.Name,
						entry.DisplayName ?? entry.Name + " API"
					),
					autoSave: true
				);
				_logger.LogInformation("Added ApiScope: {ApiScope} ({DisplayName})", apiScope.Name, apiScope.DisplayName);
			}
		}
	}

	private async Task EnsureApiResources()
	{
		foreach (var entry in _data.ApiResources)
		{
			var apiResource = await _apiResourceRepository.FindByNameAsync(entry.Name);
			if (apiResource == null)
			{
				apiResource = await _apiResourceRepository.InsertAsync(
					new ApiResource(
						_guidGenerator.Create(),
						entry.Name,
						entry.DisplayName
					),
					autoSave: true
				);
			}

			foreach (var userClaim in CommonApiUserClaims.Concat(apiResource.UserClaims.Select(c => c.Type)))
			{
				if (apiResource.FindClaim(userClaim) == null)
				{
					apiResource.AddUserClaim(userClaim);
					_logger.LogInformation("Added UserClaim to {ApiResource} API resource: {UserClaim}", apiResource.Name, userClaim);
				}
			}

			await _apiResourceRepository.UpdateAsync(apiResource);
		}
	}

	private async Task EnsureClients()
	{
		foreach (var entry in _data.Clients)
		{
			await EnsureClientAsync(entry);
		}
	}

	private async Task<Client> EnsureClientAsync(ClientSeed entry)
	{
		var client = await _clientRepository.FindByClientIdAsync(entry.ClientId);
		if (client == null)
		{
			client = await _clientRepository.InsertAsync(
				new Client(
					_guidGenerator.Create(),
					entry.ClientId
				)
				{
					ClientName = entry.ClientName,
					ProtocolType = "oidc",
					Description = entry.Description ?? entry.ClientName ?? entry.ClientId,
					AlwaysIncludeUserClaimsInIdToken = true,
					AllowOfflineAccess = true,
					AbsoluteRefreshTokenLifetime = 31536000, //365 days
					AccessTokenLifetime = 31536000, //365 days
					AuthorizationCodeLifetime = 300,
					IdentityTokenLifetime = 300,
					RequireConsent = false,
					FrontChannelLogoutUri = entry.FrontChannelLogoutUri,
					RequireClientSecret = entry.RequireClientSecret,
					RequirePkce = entry.RequirePkce
				},
				autoSave: true
			);
			_logger.LogInformation("Added Client: {ClientId} ({ClientName})", client.ClientId, client.ClientName);
		}

		foreach (var scope in entry.AllowedScopes.Concat(CommonScopes))
		{
			if (client.FindScope(scope) == null)
			{
				client.AddScope(scope);
				_logger.LogInformation("Added Scope to {ClientId}: {Scope}", client.ClientId, scope);
			}
		}

		foreach (var grantType in entry.AllowedGrantTypes)
		{
			if (client.FindGrantType(grantType) == null)
			{
				client.AddGrantType(grantType);
				_logger.LogInformation("Added GrantType to {ClientId}: {GrantType}", client.ClientId, grantType);
			}
		}

		foreach (var secret in entry.ClientSecrets)
		{
			if (client.FindSecret(secret.Value) == null)
			{
				client.AddSecret(secret.Value);
			}
		}

		if (entry.FrontChannelLogoutUri != null)
		{
			client.FrontChannelLogoutUri = entry.FrontChannelLogoutUri;
			_logger.LogInformation("Set FrontChannelLogoutUri for {ClientId}: {FrontChannelLogoutUri}", client.ClientId, entry.FrontChannelLogoutUri);
		}

		if (entry.BackChannelLogoutUri != null)
		{
			client.BackChannelLogoutUri = entry.BackChannelLogoutUri;
			_logger.LogInformation("Set BackChannelLogoutUri for {ClientId}: {BackChannelLogoutUri}", client.ClientId, entry.BackChannelLogoutUri);
		}

		foreach (var redirectUri in entry.RedirectUris)
		{
			if (client.FindRedirectUri(redirectUri) == null)
			{
				client.AddRedirectUri(redirectUri);
				_logger.LogInformation("Added RedirectUri to {ClientId}: {RedirectUri}", client.ClientId, redirectUri);
			}
		}

		foreach (var postLogoutRedirectUri in entry.PostLogoutRedirectUris)
		{
			if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
			{
				client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
				_logger.LogInformation("Added PostLogoutRedirectUri to {ClientId}: {PostLogoutRedirectUri}", client.ClientId, postLogoutRedirectUri);
			}
		}

		if (entry.Permissions.Any())
		{
			await _permissionDataSeeder.SeedAsync(
				ClientPermissionValueProvider.ProviderName,
				entry.ClientId,
				entry.Permissions,
				null
			);
			_logger.LogInformation("Added Permissions to {ClientId}: {Permissions}", client.ClientId, entry.Permissions.ToArray());
		}

		foreach (var corsOrigin in entry.AllowedCorsOrigins.Select(o => o.RemovePostFix("/")))
		{
			if (client.FindCorsOrigin(corsOrigin) == null)
			{
				client.AddCorsOrigin(corsOrigin);
				_logger.LogInformation("Added CorsOrigin to {ClientId}: {CorsOrigin}", client.ClientId, corsOrigin);
			}
		}

		return await _clientRepository.UpdateAsync(client);
	}
}
